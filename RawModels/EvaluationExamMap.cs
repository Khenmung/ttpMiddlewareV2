using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("EvaluationExamMap")]
    public class RawEvaluationExamMap
    {
      

        [Key]
        public int EvaluationExamMapId { get; set; }
        public int EvaluationMasterId { get; set; }
        public short? ExamId { get; set; }
        [Required]
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public short? OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }

     
    }
}
