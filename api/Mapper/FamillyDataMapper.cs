using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.DTO;

namespace api.Mapper
{
    public static class FamillyDataMapper
    {
        
        public static FamillyData NewFamillyDto(this NewFamillyRequestDto requestDto, string AdminId)
        {
            return new FamillyData
            {
                Members = new List<UserData>(),
                GroupBudget = requestDto.GroupBudget,
                NameGroup = requestDto.NameGroup,
                AdminGroupId = AdminId
            };
        }


    }
}