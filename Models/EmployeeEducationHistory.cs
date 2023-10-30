using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EmployeeEducationHistory")]
    public partial class EmployeeEducationHistory
    {
        [Key]
        public int EmployeeEducationHistoryId { get; set; }
        [Required]
        [StringLength(30)]
        public string CourseName { get; set; }
        public short FromYear { get; set; }
        public short ToYear { get; set; }
        public short PercentageObtained { get; set; }
        [Required]
        [StringLength(30)]
        public string BoardName { get; set; }
        public int EmployeeId { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
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
        [InverseProperty(nameof(EmpEmployee.EmployeeEducationHistories))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.EmployeeEducationHistories))]
        public virtual Organization Org { get; set; }
    }
}
