using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.DTO
{
    public class AddUserToMemberDTO
    {
        public int GroupId { get; set; }
        public string Email { get; set; }
    }
}