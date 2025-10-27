using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(UserData user, List<string> roles);
        
    }
}