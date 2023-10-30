using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ExamStudentResult")]
    public partial class ExamStudentResult
    {
        [Key]
        public int ExamStudentResultId { get; set; }
        public short ExamId { get; set; }
        public int? EvaluationExamMapId { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int StudentId { get; set; }
        public int StudentClassId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? TotalMarks { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? MarkPercent { get; set; }
        [StringLength(50)]
        public string Division { get; set; }
        public short? ClassStrength { get; set; }
        [StringLength(10)]
        public string Attendance { get; set; }
        public short? Rank { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public byte? PassCount { get; set; }
        public byte? FailCount { get; set; }
        public int? ExamStatusId { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("ExamStudentResults")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ExamId))]
        [InverseProperty("ExamStudentResults")]
        public virtual Exam Exam { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.ExamStudentResults))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("ExamStudentResults")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
