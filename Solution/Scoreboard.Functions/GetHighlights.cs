using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Scoreboard.Functions.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Scoreboard.Common.Entities;
using Scoreboard.Common.Data;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Scoreboard.Functions
{
    public static class GetHighlights
    {
        private static string conn = ConfigurationManager.ConnectionStrings["HighlightContext"].ConnectionString;       

        [FunctionName("GetHighlights")]
        public async static Task Run([TimerTrigger("0 0 9 * * 1")]TimerInfo myTimer, 
            [Queue("sedash-mailqueue", Connection = "AzureWebJobsStorage")] IAsyncCollector<string> outputQueue,
            TraceWriter log)
        {
            log.Info($"GetHighlights Triggered at: {DateTime.Now}");
            string highlights = await GetUserHighlights();
            var usersToSend = await GetUsers();

            foreach (UserProfile d in usersToSend)
            {
                HighlightMessage hm = new HighlightMessage();
                hm.email = d.EmailAddress;
                hm.messagebody = highlights;                
                await outputQueue.AddAsync(JsonConvert.SerializeObject(hm));                
            }            
            log.Info("Function Complete");
            return;

        }

        private async static Task<string> GetUserHighlights()
        {

            string body = "<!DOCTYPE html>";
            body += "<html><head><style type=\"text/css\">body{font-family:Arial;font-size:13px;}</style></head><body>";

            using (var db = new ApplicationDbContext(conn))
            {
                // Runs on Monday to set for any items 
                // entered previous Mon -> Sun
                DateTime currDate = DateTime.Today;
                DateTime startDate = currDate.AddDays(-7);
                DateTime endDate = currDate.AddDays(-1);

                body += "<h3>Highlights for Week Ending " + currDate.AddDays(-3).ToShortDateString() + "</h3>";
                body += "<hr size=\"1\">";

                var myQuery = await db.Highlight
                .Include(h => h.UserProfile)
                .Where(h => h.DateStart >= startDate && h.DateEnd <= endDate)
                .Select(h => new HighlightSearchResult
                {
                    HighlightId = h.HighlightId,
                    FullName = h.UserProfile.FirstName + " " + h.UserProfile.LastName,
                    Body = h.Body,
                    DateStart = h.DateStart,
                    DateEnd = h.DateEnd,
                    DateCreated = h.DateCreated
                })
                .OrderBy(h => h.DateCreated)
                .OrderBy(h => h.FullName)
                .ToListAsync();

                foreach (HighlightSearchResult h in myQuery)
                {
                    body += "<strong><h4>" + h.FullName + " - " + h.DateStart.ToShortDateString() + "->" + h.DateEnd.ToShortDateString() + "</h4></strong>";
                    body += h.Body;
                    body += "<hr size=\"1\">";
                }

            }
            body += "</body></html>";
            return body;
        }

        static async Task<List<UserProfile>> GetUsers()
        {
            using (var db = new ApplicationDbContext(conn))
            {
                List<UserProfile> myUsersToSend = await db.UserProfile
                    .Where(u => u.IsActiveTeamMember == true).ToListAsync();

                return myUsersToSend;
            }
        }
    }
}
