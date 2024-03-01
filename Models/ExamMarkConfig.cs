using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ExamMarkConfig")]
    public partial class ExamMarkConfig
    {
        [Key]
        public int ExamMarkConfigId { get; set; }
        public short ExamId { get; set; }
        public int ClassId { get; set; }
        public int ClassSubjectId { get; set; }
        [Required]
        [StringLength(1000)]
        public string Formula { get; set; }
        [Required]
        public bool? Active { get; set; }
        public short? BatchId { get; set; }
        public bool Deleted { get; set; }
        public short? OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.ExamMarkConfigs))]
        public virtual ClassMaster Class { get; set; }
        [ForeignKey(nameof(ClassSubjectId))]
        [InverseProperty("ExamMarkConfigs")]
        public virtual ClassSubject ClassSubject { get; set; }
        [ForeignKey(nameof(ExamId))]
        [InverseProperty("ExamMarkConfigs")]
        public virtual Exam Exam { get; set; }
    }
}
