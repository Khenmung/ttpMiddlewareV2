using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ExamResultSubjectMark")]
    public partial class ExamResultSubjectMark
    {
        [Key]
        public int ExamResultSubjectMarkId { get; set; }
        public int StudentClassId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public short ExamId { get; set; }
        public int StudentClassSubjectId { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Marks { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal? ActualMarks { get; set; }
        [StringLength(20)]
        public string Grade { get; set; }
        public short OrgId { get; set; }
        public bool Active { get; set; }
        public short? BatchId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool? Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }

        [ForeignKey(nameof(ExamId))]
        [InverseProperty("ExamResultSubjectMarks")]
        public virtual Exam Exam { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("ExamResultSubjectMarks")]
        public virtual StudentClass StudentClass { get; set; }
        [ForeignKey(nameof(StudentClassSubjectId))]
        [InverseProperty("ExamResultSubjectMarks")]
        public virtual StudentClassSubject StudentClassSubject { get; set; }
    }
}
