using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public class UserScore
    {
        public int UserProfileId { get; set; }
        public string FullName { get; set; }
        public bool IsCRM { get; set; }
        public bool IsExpenses { get; set; }
        public bool IsFRI { get; set; }
        public bool IsAscendNotes { get; set; }
        public int TotalPresentations { get; set; }
        public int TotalAscend { get; set; }
        public int CommitTotal { get; set; }
        public int CommitCompleted { get; set; }
    }
}
