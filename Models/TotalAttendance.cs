using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("TotalAttendance")]
    public partial class TotalAttendance
    {
        [Key]
        public int TotalAttendanceId { get; set; }
        public int ClassId { get; set; }
        public short ExamId { get; set; }
        public byte TotalNoOfAttendance { get; set; }
        public bool Active { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.TotalAttendances))]
        public virtual ClassMaster Class { get; set; }
        [ForeignKey(nameof(ExamId))]
        [InverseProperty("TotalAttendances")]
        public virtual Exam Exam { get; set; }
    }
}
