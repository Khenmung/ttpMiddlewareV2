using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class EmpEmployeeSkill
    {
        [Key]
        public int EmpEmployeeSkillId { get; set; }
        public short SkillId { get; set; }
        public int EmployeeId { get; set; }
        public byte Active { get; set; }
        public short ExperienceInMonths { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.EmpEmployeeSkills))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.EmpEmployeeSkills))]
        public virtual Organization Org { get; set; }
    }
}
