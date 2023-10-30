using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Active), nameof(Deleted), Name = "NonClusteredIndex-20230830-212839")]
    public partial class ClassEvaluationOption
    {
        public ClassEvaluationOption()
        {
            ClassEvaluations = new HashSet<ClassEvaluation>();
            StudentEvaluationAnswers = new HashSet<StudentEvaluationAnswer>();
        }

        [Key]
        public int ClassEvaluationAnswerOptionsId { get; set; }
        public int? ClassEvaluationId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Point { get; set; }
        public byte? Correct { get; set; }
        public byte Active { get; set; }
        public int? ParentId { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(ClassEvaluationId))]
        [InverseProperty("ClassEvaluationOptions")]
        public virtual ClassEvaluation ClassEvaluation { get; set; }
        [InverseProperty("ClassEvaluationAnswerOptionParent")]
        public virtual ICollection<ClassEvaluation> ClassEvaluations { get; set; }
        [InverseProperty(nameof(StudentEvaluationAnswer.ClassEvaluationAnswerOptions))]
        public virtual ICollection<StudentEvaluationAnswer> StudentEvaluationAnswers { get; set; }
    }
}
