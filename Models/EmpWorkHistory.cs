using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EmpWorkHistory")]
    public partial class EmpWorkHistory
    {
        [Key]
        public int EmpWorkHistoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string OrganizationName { get; set; }
        [StringLength(100)]
        public string Designation { get; set; }
        [StringLength(256)]
        public string Responsibility { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        public int? EmployeeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
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
        [InverseProperty(nameof(EmpEmployee.EmpWorkHistories))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.EmpWorkHistories))]
        public virtual Organization Org { get; set; }
    }
}
