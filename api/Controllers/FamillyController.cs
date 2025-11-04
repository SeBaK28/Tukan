using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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

        [HttpGet]
        [Route("get-all-members")]
        [Authorize]
        public async Task<IActionResult> GetAllMembersFromFamilly() //poprawić ten kontroller zeby byl zrobiony zgodnie ze sztuką, dodać Interface, mappery itd
        {

            var getId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var getUser = await _context.UserDatas.Include(x => x.FamillyGroups).FirstOrDefaultAsync(x => x.Id == getId);

            var getFamillyList = getUser.FamillyGroups.ToList();

            List<FamillyListForUserResponseDto> lista = new List<FamillyListForUserResponseDto>();

            foreach (var user in getFamillyList)
            {
                var usersList = user.Members.ToList();
                var getFamilly = await _context.FamillyDatas.Include(x => x.Members).FirstOrDefaultAsync(x => x.GroupId == user.GroupId);
                var newFam = new FamillyListForUserResponseDto
                {
                    FamillyId = getFamilly.GroupId,
                    UsersListDto = new List<UsersFromFamillyResponseDto>()
                };

                foreach (var item in getFamilly.Members)
                {
                    var findUser = await _context.UserDatas.FirstOrDefaultAsync(x => x.Id == item.Id);

                    var newUserToList = new UsersFromFamillyResponseDto
                    {
                        Email = findUser.Email,
                        MonthlyBuget = findUser.MonthlyBuget,
                        Expenses = findUser.Expenses,
                        Savings = findUser.Savings
                    };
                    newFam.UsersListDto.Add(newUserToList);
                }

                lista.Add(newFam);
            }


            return Ok(lista);

        }

        
    }
}