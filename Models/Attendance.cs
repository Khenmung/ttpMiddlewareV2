using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("Attendance")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(BatchId), nameof(ClassId), nameof(SectionId), nameof(AttendanceDate), nameof(ClassSubjectId), Name = "attendanceindex")]
    public partial class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public int? StudentClassId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public short? TeacherId { get; set; }
        public int AttendanceStatusId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AttendanceDate { get; set; }
        public int? ClassSubjectId { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public bool? Approved { get; set; }
        [StringLength(450)]
        public string ApprovedBy { get; set; }
        public short? ReportedTo { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public short BatchId { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("Attendances")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ClassSubjectId))]
        [InverseProperty("Attendances")]
        public virtual ClassSubject ClassSubject { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.Attendances))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("Attendances")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
