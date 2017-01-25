using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models.WigViewModels
{
    public class EditWigViewModel
    {
        public int WigId { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public WigStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
        public IList<UserProfile> UserProfiles { get; set; }
        public List<int> SelectedUserProfiles { get; set; }

    }
}
