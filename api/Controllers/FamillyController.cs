using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamillyController : ControllerBase
    {
        private readonly UserManager<UserData> _userManager;
        private readonly ApplicationDbContext _context;

        public FamillyController(UserManager<UserData> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("create-familly")]
        [Authorize]
        public async Task<IActionResult> CreateFamilly([FromBody] NewFamillyRequestDto requestDto)
        {
            var getId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var getUser = await _userManager.FindByIdAsync(getId);

            var familly = new FamillyData
            {
                NameGroup = requestDto.NameGroup,
                GroupBudget = 0,
                AdminGroupId = getUser.Id,
                Members = new List<UserData>()
            };

            familly.Members.Add(getUser);
            var addOperation = await _context.FamillyDatas.AddAsync(familly);
            if (addOperation is not null)
            {
                await _userManager.AddToRoleAsync(getUser, "FamillyCreator");
                await _context.SaveChangesAsync();

                return Ok($"Familly {requestDto.NameGroup} is create");
                // return CreatedAtAction(nameof(), new{id = })
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("add-member")]
        [Authorize(Roles = "FamillyCreator")]
        public async Task<IActionResult> AddUserToMemers([FromBody] AddUserToMemberDTO requestDto)
        {
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userToAdd = await _userManager.FindByEmailAsync(requestDto.Email);

            var familly = await _context.FamillyDatas.FirstOrDefaultAsync(x => x.GroupId == requestDto.GroupId);

            if(familly is null || userToAdd is null)
            {
                return NotFound();
            }

            if (familly.AdminGroupId == creatorId)
            {
                familly.Members.Add(userToAdd);
                await _context.SaveChangesAsync();
            }

            return BadRequest();
        }
    }
}