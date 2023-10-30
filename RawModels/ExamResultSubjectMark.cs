using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("ExamResultSubjectMark")]
    public class RawExamResultSubjectMark
    {
        [Key]
        public int ExamResultSubjectMarkId { get; set; }
        public int StudentClassId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
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

     
    }
}
