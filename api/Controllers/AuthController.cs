using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Models.DTO;
using api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserData> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenRepository _token;

        public AuthController(UserManager<UserData> userManager, ITokenRepository token, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _token = token;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            // var user = new IdentityUser
            // {
            //     UserName = requestDto.Email?.Trim(),
            //     Email = requestDto.Email?.Trim()
            // };

            var user = new UserData
            {
                UserName = requestDto.Email?.Trim(),
                Email = requestDto.Email?.Trim(),
            };

            //Create user
            var identityResult = await _userManager.CreateAsync(user, requestDto.Password);

            //Check is user correct created
            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRoleAsync(user, "FamillyMember");

                if (identityResult.Succeeded)
                {
                    await _context.UserDatas.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return Ok("Account created"); //there sould be returned token, email
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        { 
            var identityUser = await _userManager.FindByEmailAsync(requestDto.Email);

            if (identityUser is not null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(identityUser, requestDto.Password);

                if (checkPassword)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);
                    var token = _token.CreateJwtToken(identityUser, roles.ToList());
                    //return user token 
                    return Ok(
                        new LoginResponseDto
                        {
                            Email = requestDto.Email,
                            Roles = roles.ToList(),
                            Token = token
                        });
                }
            }

            ModelState.AddModelError("", "Email or password is incorrect");

            return ValidationProblem(ModelState);
        }

    }
}