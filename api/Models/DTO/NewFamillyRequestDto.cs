using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.DTO
{
    public class NewFamillyRequestDto
    {
        public string NameGroup { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal GroupBudget { get; set; }

    }
}