﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Models;

namespace USSEScoreboard.Interfaces
{
    public interface IWIGSettingRepository
    {
        WIGSetting GetSettings();    
    }
}
