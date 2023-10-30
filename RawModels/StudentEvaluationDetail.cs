using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("StudentEvaluationDetail")]
    public class RawStudentEvaluationDetail
    {
        [Key]
        public int StudentEvaluationDetailId { get; set; }
        public int? ClassEvaluationId { get; set; }
        public int? Rating { get; set; }
        [StringLength(1000)]
        public string Detail { get; set; }
        public int? StudentClassId { get; set; }
        public short? OrgId { get; set; }
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
