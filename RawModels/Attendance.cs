using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("Attendance")]
    [Index(nameof(OrgId), nameof(AttendanceDate), nameof(StudentClassId), nameof(ClassSubjectId), Name = "attendanceindex")]
    public class RawAttendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public int StudentClassId { get; set; }
        public int SectionId { get; set; }
        public int ClassId { get; set; }
        public int? SemesterId { get; set; }
        public short? TeacherId { get; set; }
        public byte AttendanceStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AttendanceDate { get; set; }
        public int? ClassSubjectId { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public short? ReportedTo { get; set; }
        public bool? Approved { get; set; }
        [StringLength(450)]
        public string ApprovedBy { get; set; }
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

       
    }
}
