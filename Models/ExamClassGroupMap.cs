using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ExamClassGroupMap")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Active), nameof(Deleted), Name = "NonClusteredIndex-20230830-205200")]
    public partial class ExamClassGroupMap
    {
        [Key]
        public short ExamClassGroupMapId { get; set; }
        public short ExamId { get; set; }
        public short ClassGroupId { get; set; }
        public bool Active { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal MarkConvertTo { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(ClassGroupId))]
        [InverseProperty("ExamClassGroupMaps")]
        public virtual ClassGroup ClassGroup { get; set; }
        [ForeignKey(nameof(ExamId))]
        [InverseProperty("ExamClassGroupMaps")]
        public virtual Exam Exam { get; set; }
    }
}
