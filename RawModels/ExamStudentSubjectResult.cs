using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("ExamStudentSubjectResult")]
    public class RawExamStudentSubjectResult
    {
        [Key]
        public int ExamStudentSubjectResultId { get; set; }
        public short ExamId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int SemesterId { get; set; }
        public int StudentClassId { get; set; }
        public int StudentClassSubjectId { get; set; }
        public short ClassSubjectMarkComponentId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Marks { get; set; }
        [StringLength(10)]
        public string Grade { get; set; }
        public int ExamStatus { get; set; }
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
        public byte? Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
