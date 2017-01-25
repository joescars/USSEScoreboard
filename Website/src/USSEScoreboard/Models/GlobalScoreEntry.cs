using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Models
{
    public enum GlobalScoreEntryType
    {
        [Description("Total Community Presentations")]
        Presentations,
        [Description("Ascend Active")]
        AscendActive,
        [Description("Ascend Code Completed")]
        AscendCodeCompleted,
        [Description("Ascend Proposed")]
        AscendProposed,
        [Description("Ascend Win")]
        AscendWins
    }

    public class GlobalScoreEntry
    {
        public int GlobalScoreEntryId { get; set; }
        public int TimeFrameTotal { get; set; }
        public GlobalScoreEntryType GlobalScoreType { get; set; }
        public DateTime WeekEnding { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public GlobalScoreEntry()
        {
            this.TimeFrameTotal = 0;
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
        }
    }
}
