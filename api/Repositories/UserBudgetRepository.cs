using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserBudgetRepository : IUserBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public UserBudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ChangeBudget(string userId, decimal value)
        {
            var user = await _context.UserDatas.FirstOrDefaultAsync(x => x.Id == userId);

            if (value > 0)
            {
                user.MonthlyBuget = value;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }

        public async Task<bool> ChangeSavings(string userId, decimal value)
        {
            var user = await _context.UserDatas.FirstOrDefaultAsync(x => x.Id == userId);

            if (value > 0)
            {
                user.Savings = value;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

                public async Task<bool> ChangeExpenses(string userId, decimal value)
        {
            var user = await _context.UserDatas.FirstOrDefaultAsync(x => x.Id == userId);

            if (value > 0)
            {
                user.Expenses = value;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}