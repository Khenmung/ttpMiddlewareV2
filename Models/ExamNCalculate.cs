using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ExamNCalculate")]
    public partial class ExamNCalculate
    {
        [Key]
        public int ExamNCalculateId { get; set; }
        public short ExamId { get; set; }
        public int CalculateResultPropertyId { get; set; }
        public short OrgId { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        [Required]
        [StringLength(255)]
        public string Formula { get; set; }
        public int CalculateCategoryId { get; set; }
        public byte Sequence { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(ExamId))]
        [InverseProperty("ExamNCalculates")]
        public virtual Exam Exam { get; set; }
    }
}
