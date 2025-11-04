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
        public int? FamillyId { get; set; } = null;
        public List<FamillyData> FamillyGroups { get; set; } = new List<FamillyData>();
        public List<TransactionData> TransactionList { get; set; } = new List<TransactionData>();
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyBuget { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Expenses { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Savings { get; set; } = 0;
    }
}   