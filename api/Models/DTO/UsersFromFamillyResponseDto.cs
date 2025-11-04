using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.DTO
{
    public class UsersFromFamillyResponseDto
    {
        public string Email { get; set; }
        public decimal MonthlyBuget { get; set; }
        public decimal Expenses { get; set; }
        public decimal Savings { get; set; }

    }
}