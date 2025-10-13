using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class UserData : IdentityUser
    {
        public int FamillyId { get; set; }
        public List<FamillyData> FamillyGroups { get; set; } = new List<FamillyData>();
        public List<TransactionData> TransactionList { get; set; } = new List<TransactionData>();
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyBuget { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Expenses { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Savings { get; set; }
    }
}