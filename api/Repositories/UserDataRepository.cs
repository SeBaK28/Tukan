using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        public UserManager<UserData> _userManager;
        public UserDataRepository(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserData?> GetUserAsync(string id)
        {
            var getUser = await _userManager.FindByIdAsync(id);
            return getUser;
        }
    }
}