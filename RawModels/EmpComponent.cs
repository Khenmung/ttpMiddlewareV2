using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawEmpComponent
    {
        public RawEmpComponent()
        {
            
        }

        [Key]
        public int EmpSalaryComponentId { get; set; }
        [Required]
        [StringLength(50)]
        public string SalaryComponent { get; set; }
        [Required]
        [StringLength(250)]
        public string FormulaOrAmount { get; set; }
        public int ComponentTypeId { get; set; }
        public short DisplayOrder { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

      
    }
}
