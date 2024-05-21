using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentEvaluationResult")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(StudentClassId), nameof(Deleted), nameof(History), Name = "NonClusteredIndex-20240513-221337")]
    public partial class StudentEvaluationResult
    {
        public StudentEvaluationResult()
        {
            StudentEvaluationAnswers = new HashSet<StudentEvaluationAnswer>();
        }

        [Key]
        public int StudentEvaluationResultId { get; set; }
        public int StudentClassId { get; set; }
        public int? StudentId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int EvaluationExamMapId { get; set; }
        public int ClassEvaluationId { get; set; }
        public string HistoryText { get; set; }
        [StringLength(1000)]
        public string AnswerText { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Points { get; set; }
        public int? AnswerOptionsId { get; set; }
        public bool Submitted { get; set; }
        public short? BatchId { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        [StringLength(450)]
        public string Updatedby { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }
        public Guid SyncId { get; set; }
        public bool History { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.StudentEvaluationResults))]
        public virtual ClassMaster Class { get; set; }
        [ForeignKey(nameof(ClassEvaluationId))]
        [InverseProperty("StudentEvaluationResults")]
        public virtual ClassEvaluation ClassEvaluation { get; set; }
        [ForeignKey(nameof(EvaluationExamMapId))]
        [InverseProperty("StudentEvaluationResults")]
        public virtual EvaluationExamMap EvaluationExamMap { get; set; }
        [InverseProperty(nameof(StudentEvaluationAnswer.StudentEvaluationResult))]
        public virtual ICollection<StudentEvaluationAnswer> StudentEvaluationAnswers { get; set; }
    }
}
