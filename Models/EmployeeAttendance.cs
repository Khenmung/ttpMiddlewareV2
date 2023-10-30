using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EmployeeAttendance")]
    public partial class EmployeeAttendance
    {
        [Key]
        public int EmployeeAttendanceId { get; set; }
        public short EmployeeId { get; set; }
        public int AttendanceStatusId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AttendanceDate { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public short? ReportedTo { get; set; }
        public bool? Approved { get; set; }
        [StringLength(450)]
        public string ApprovedBy { get; set; }
        public bool Active { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public short? BatchId { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("EmployeeAttendances")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.EmployeeAttendances))]
        public virtual Organization Org { get; set; }
    }
}
