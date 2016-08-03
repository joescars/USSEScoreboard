using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;

namespace USSEScoreboard.Models
{
    public class SeedDataLive
    {
        private class SeedUser
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            List<SeedUser> initialUsers = new List<SeedUser>();

            // User Data
            initialUsers.Add(
                new SeedUser { FirstName = "Homer", LastName = "Simpson", Email = "Home.Simpson@TheSimpsons.com", Password = "DuffBeer2015:)" }
            );

            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Look for any users.
            if (context.UserProfile.Any())
            {
                return;   // DB has been seeded
            }

            // Create users
            foreach (SeedUser su in initialUsers)
            {
                //if (context.UserProfile.Any(aa => aa.FirstName == su.FirstName))
                //    continue;
                var u = new ApplicationUser { UserName = su.Email, Email = su.Email };
                var result = await userManager.CreateAsync(u, su.Password);
                if (result.Succeeded)
                {
                    var up = new UserProfile()
                    {
                        UserId = u.Id,
                        FirstName = su.FirstName,
                        LastName = su.LastName
                    };
                    context.UserProfile.Add(up);
                    await context.SaveChangesAsync();
                }
                await userManager.AddToRoleAsync(u, "TE");
            }

        }

    }

}
