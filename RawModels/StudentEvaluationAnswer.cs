using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudentEvaluationAnswer")]
    public class RawStudentEvaluationAnswer
    {
        [Key]
        public int StudentEvaluationAnswerId { get; set; }
        public int StudentEvaluationResultId { get; set; }
        public int ClassEvaluationAnswerOptionsId { get; set; }
        public byte Active { get; set; }
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
        public int SubOrgId { get; set; }

      
    }
}
