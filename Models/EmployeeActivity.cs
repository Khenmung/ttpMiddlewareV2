using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EmployeeActivity")]
    public partial class EmployeeActivity
    {
        [Key]
        public int EmployeeActivityId { get; set; }
        public int ActivityNameId { get; set; }
        public int EmployeeActivityCategoryId { get; set; }
        public int? EmployeeActivitySubCategoryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ActivityDate { get; set; }
        public int EmployeeId { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        [StringLength(256)]
        public string Remarks { get; set; }
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
        [StringLength(1000)]
        public string Description { get; set; }
        public int? SessionId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.EmployeeActivities))]
        public virtual EmpEmployee Employee { get; set; }
    }
}
