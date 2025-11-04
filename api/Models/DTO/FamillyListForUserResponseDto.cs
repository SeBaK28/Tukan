using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.DTO
{
    public class FamillyListForUserResponseDto
    {
        public int FamillyId { get; set; }
        public List<UsersFromFamillyResponseDto> UsersListDto { get; set; }
        //public List<UserDataResponseForFamillyResponseDto>? User { get; set; } = new List<UserDataResponseForFamillyResponseDto>();
    }
}