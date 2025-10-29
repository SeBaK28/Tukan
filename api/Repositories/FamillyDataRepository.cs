using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mapper;
using api.Models;
using api.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class FamillyDataRepository : IFamillyDataRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserData> _userManager;
        public FamillyDataRepository(ApplicationDbContext context, UserManager<UserData> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<FamillyData?> AddToFamillyAsync(UserData userToAdd, AddUserToMemberDTO requestDto, string creatorId)
        {
            var familly = await _context.FamillyDatas.Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.GroupId == requestDto.GroupId);

            if (familly is null || familly.AdminGroupId != creatorId || userToAdd is null)
            {
                return null;
            }
            
            familly.Members.Add(userToAdd);
            await _context.SaveChangesAsync();
            return familly;
        }

        public async Task<FamillyData?> CreateFamillyAsync(UserData user, NewFamillyRequestDto requestDto)
        {

            var familly = requestDto.NewFamillyDto(user.Id);

            familly.Members.Add(user);

            var addOperation = await _context.FamillyDatas.AddAsync(familly);
            if (addOperation is not null)
            {
                await _context.SaveChangesAsync();
                return familly;
            }
            return null;
        }
        
        
    }
}