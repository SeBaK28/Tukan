using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Models;
using api.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamillyController : ControllerBase
    {
        private readonly UserManager<UserData> _userManager;

        public FamillyController(UserManager<UserData> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("createFamilly")]
        [Authorize]
        public async Task<IActionResult> CreateFamilly()
        {
            //var getId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok("Success");
        }
    }
}