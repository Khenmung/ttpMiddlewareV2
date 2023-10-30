using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudentEvaluation")]
    public class RawStudentEvaluation
    {
        [Key]
        public int StudentEvaluationId { get; set; }
        public int? ClassEvaluationId { get; set; }
        public int? RatingId { get; set; }
        [StringLength(1000)]
        public string Detail { get; set; }
        public int? StudentClassId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActivityDate { get; set; }
        public int? EvaluationTypeId { get; set; }
        public short? ExamId { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? Active { get; set; }

      
    }
}
