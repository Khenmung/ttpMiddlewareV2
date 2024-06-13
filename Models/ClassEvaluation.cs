using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ClassEvaluation")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Active), nameof(Deleted), nameof(History), Name = "NonClusteredIndex-20230830-205432")]
    public partial class ClassEvaluation
    {
        public ClassEvaluation()
        {
            ClassEvaluationOptions = new HashSet<ClassEvaluationOption>();
            StudentEvaluationResults = new HashSet<StudentEvaluationResult>();
        }

        [Key]
        public int ClassEvaluationId { get; set; }
        public int QuestionnaireTypeId { get; set; }
        public int EvaluationMasterId { get; set; }
        public short? ExamId { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        public byte MultipleAnswer { get; set; }
        public int? ClassEvaluationAnswerOptionParentId { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal DisplayOrder { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Marks { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(ClassEvaluationAnswerOptionParentId))]
        [InverseProperty(nameof(ClassEvaluationOption.ClassEvaluations))]
        public virtual ClassEvaluationOption ClassEvaluationAnswerOptionParent { get; set; }
        [ForeignKey(nameof(EvaluationMasterId))]
        [InverseProperty("ClassEvaluations")]
        public virtual EvaluationMaster EvaluationMaster { get; set; }
        [ForeignKey(nameof(ExamId))]
        [InverseProperty("ClassEvaluations")]
        public virtual Exam Exam { get; set; }
        [InverseProperty(nameof(ClassEvaluationOption.ClassEvaluation))]
        public virtual ICollection<ClassEvaluationOption> ClassEvaluationOptions { get; set; }
        [InverseProperty(nameof(StudentEvaluationResult.ClassEvaluation))]
        public virtual ICollection<StudentEvaluationResult> StudentEvaluationResults { get; set; }
    }
}
