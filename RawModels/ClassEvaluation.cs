using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("ClassEvaluation")]
    public class RawClassEvaluation
    {
        public RawClassEvaluation()
        {
            
        }

        [Key]
        public int ClassEvaluationId { get; set; }
        public int QuestionnaireTypeId { get; set; }
        public int EvaluationMasterId { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        public byte MultipleAnswer { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal? Marks { get; set; }
        public short? ExamId { get; set; }
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

      
    }
}
