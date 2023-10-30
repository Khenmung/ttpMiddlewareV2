using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EvaluationMark")]
    public partial class EvaluationMark
    {
        [Key]
        public int EvaluationMarkId { get; set; }
        public int EvaluationMasterId { get; set; }
        public int StudentClassId { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal TotalMarks { get; set; }
        public int StatusId { get; set; }
        public short? BatchId { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public bool Active { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
