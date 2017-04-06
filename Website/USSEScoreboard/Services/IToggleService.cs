using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Services
{
    public interface IToggleService
    {
        Task ToggleUserCRM(int userId);
    }
}
