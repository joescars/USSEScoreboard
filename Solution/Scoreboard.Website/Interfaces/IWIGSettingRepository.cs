using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scoreboard.Website.Models;

namespace Scoreboard.Website.Interfaces
{
    public interface IWIGSettingRepository
    {
        WIGSetting GetSettings();    
    }
}
