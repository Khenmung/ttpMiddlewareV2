using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EvaluationResultMark")]
    public partial class EvaluationResultMark
    {
        [Key]
        public int EvaluationResultMarkId { get; set; }
        public int StudentClassId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
        public int EvaluationExamMapId { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TotalMark { get; set; }
        public short Rank { get; set; }
        public bool Active { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [StringLength(256)]
        public string Comments { get; set; }
        public int TestStatusId { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Pc { get; set; }
    }
}
