using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models.CommitmentViewModels
{
    public class EditCommitmentViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public CommitmentStatus Status { get; set; }
        public DateTime DateCreated { get; set; }

        // Used for assigning to users
        public IList<UserProfile> Users { get; set; }

        [Description("Assigned To")]
        public int SelectedUserID { get; set; }

        // Assigned Wig
        public IList<Wig> Wigs { get; set; }
        public int SelectedWigId { get; set; }
    }
}
