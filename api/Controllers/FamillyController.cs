using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamillyController : ControllerBase
    {
        private readonly UserManager<UserData> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IFamillyRepository _famillyRepo;

        public FamillyController(UserManager<UserData> userManager, ApplicationDbContext context, IFamillyRepository famillyRepo)
        {
            _famillyRepo = famillyRepo;
            _userManager = userManager;
            _context = context;
        }


        [HttpPost]
        [Route("newFamilly")]
        [Authorize]
        public async Task<IActionResult> CreateNewFamilly()
        {
            var getId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var getUser = await _context.UserDatas.FirstOrDefaultAsync(x => x.Id == getId);

            if(getUser is null)
            {
                return NotFound();
            }

            var familly = new FamillyData
            {
                Members = new List<UserData>(),
                GroupBudget = 0,
                AdminGroupId = getId,
            };

            await _famillyRepo.CreateAsync(familly);
            familly.Members.Add(getUser);
            await _userManager.AddToRoleAsync(getUser, "FamillyCreator"); 


            return Ok("New familly is created");
        }
    }
}