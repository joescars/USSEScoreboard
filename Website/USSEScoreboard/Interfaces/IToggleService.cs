using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USSEScoreboard.Interfaces
{
    public interface IToggleService
    {
        Task ToggleUserCRM(int userId);
        Task ToggleUserExpense(int userId);
        Task ToggleUserFRI(int userId);
        Task ToggleUserAscendNotes(int userId);
    }
}
