using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mapper;
using api.Models;
using api.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamillyController : ControllerBase
    {
        private readonly UserManager<UserData> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IFamillyDataRepository _famillyRepo;

        public FamillyController(UserManager<UserData> userManager, ApplicationDbContext context,
        IFamillyDataRepository famillyRepo)
        {
            _famillyRepo = famillyRepo;
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

            var newFamilly = await _famillyRepo.CreateFamillyAsync(getUser, requestDto);

            if (newFamilly is null)
                return NotFound();

            await _userManager.AddToRoleAsync(getUser, "FamillyCreator");

            return Ok($"Familly {requestDto.NameGroup} is created");

        }

        [HttpPost]
        [Route("add-member")]
        [Authorize(Roles = "FamillyCreator")]
        public async Task<IActionResult> AddUserToMembers([FromBody] AddUserToMemberDTO requestDto)
        {
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userToAdd = await _userManager.FindByEmailAsync(requestDto.Email);

            var newAction = await _famillyRepo.AddToFamillyAsync(userToAdd, requestDto, creatorId);

            if (newAction is null || userToAdd is null)
            {
                return NotFound();
            }

            return Ok($"Succesfully added {requestDto.Email} to familly"); //dodać sprawdzenie czy podany
            //  user jest juz w tej rodzinie. Jak zostanie dodany user to musi być podzielony 
            // budzet na wysztkich members. Dodać pobieranie wszystkich z rodziny.
        }
    }
}