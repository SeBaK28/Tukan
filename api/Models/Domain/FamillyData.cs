using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class FamillyData
    {
        [Key]
        public int GroupId { get; set; }
        public List<UserData> Members { get; set; } //Dla fronta czy lepiej będzie mieć całego usera czy przekazywać rodzienie jedynie id danego usera 
        [Column(TypeName = "decimal(18,2)")]
        public decimal GroupBudget { get; set; }
        public string AdminGroupId { get; set; }
        public string NameGroup { get; set; } 
    }
}
