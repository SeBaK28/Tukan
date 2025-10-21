using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IUserBudgetRepository
    {
        Task<bool> ChangeBudget(string userId, decimal value); //dozmiany, jako typ dać UserData
        Task<bool> ChangeSavings(string userId, decimal value);
        Task<bool> ChangeExpenses(string userId, decimal value);
    }
}