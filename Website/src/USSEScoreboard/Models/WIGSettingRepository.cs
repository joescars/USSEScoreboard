using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USSEScoreboard.Data;
using USSEScoreboard.Interfaces;

namespace USSEScoreboard.Models
{
    public class WIGSettingRepository : IWIGSettingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public WIGSettingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public WIGSetting GetSettings()
        {
            return _dbContext.WigSetting.FirstOrDefault();
        }
    }
}
