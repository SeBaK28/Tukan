using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.DTO;

namespace api.Interfaces
{
    public interface IFamillyDataRepository
    {
        Task<FamillyData?> CreateFamillyAsync(UserData user, NewFamillyRequestDto requestDto);
        Task<FamillyData?> AddToFamillyAsync(UserData user, AddUserToMemberDTO requestDto, string creatorId);
        Task<FamillyData> RefreshFamillyDataAsync(int groupId);
        Task<bool> IsAlreadyInFamillyAsync(int groupId, UserData? user);
        Task<FamillyData?> FindFamillyAsync(int groupId);

    }
}