using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("TeacherPeriod")]
    public partial class TeacherPeriod
    {
        [Key]
        public int TeacherPeriodId { get; set; }
        public int EmployeeId { get; set; }
        public int SchoolClassPeriodId { get; set; }
        public int? TeacherSubjectId { get; set; }
        public bool? OffPeriod { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.TeacherPeriods))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(SchoolClassPeriodId))]
        [InverseProperty("TeacherPeriods")]
        public virtual SchoolClassPeriod SchoolClassPeriod { get; set; }
    }
}
