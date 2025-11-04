using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            var familly = await this.FindFamillyAsync(requestDto.GroupId);

            if (familly is null || familly.AdminGroupId != creatorId || userToAdd is null)
            {
                return null;
            }

            familly.Members.Add(userToAdd);
            await this.RefreshFamillyDataAsync(familly.GroupId);
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

        public async Task<FamillyData?> FindFamillyAsync(int groupId)
        {
            return await _context.FamillyDatas.Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.GroupId == groupId);
        }


        public async Task<bool> IsAlreadyInFamillyAsync(int groupId, UserData? user)
        {
            var familly = await this.FindFamillyAsync(groupId);

            return familly.Members.Any(x => x.Id == user.Id);
        }

        public async Task<FamillyData> RefreshFamillyDataAsync(int groupId)
        {
            var getFamilly = await this.FindFamillyAsync(groupId);

            var howMany = getFamilly.Members.Count();

            var budgetPerPerson = getFamilly.GroupBudget / howMany;

            foreach (var member in getFamilly.Members)
            {
                member.Expenses += budgetPerPerson;
            }

            //await _context.SaveChangesAsync();
            return getFamilly;
        }
    }
}