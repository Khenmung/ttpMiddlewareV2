using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EvaluationMaster")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Active), nameof(Deleted), nameof(History), Name = "NonClusteredIndex-20240606-135947")]
    public partial class EvaluationMaster
    {
        public EvaluationMaster()
        {
            ClassEvaluations = new HashSet<ClassEvaluation>();
            EvaluationExamMaps = new HashSet<EvaluationExamMap>();
        }

        [Key]
        public int EvaluationMasterId { get; set; }
        [Required]
        [StringLength(50)]
        public string EvaluationName { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        public int ClassGroupId { get; set; }
        public short? Duration { get; set; }
        public bool? DisplayResult { get; set; }
        public bool? AppendAnswer { get; set; }
        public bool? ProvideCertificate { get; set; }
        public byte? FullMark { get; set; }
        public byte? PassMark { get; set; }
        public bool Confidential { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [StringLength(8)]
        public string StartTime { get; set; }
        public int ETypeId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [InverseProperty(nameof(ClassEvaluation.EvaluationMaster))]
        public virtual ICollection<ClassEvaluation> ClassEvaluations { get; set; }
        [InverseProperty(nameof(EvaluationExamMap.EvaluationMaster))]
        public virtual ICollection<EvaluationExamMap> EvaluationExamMaps { get; set; }
    }
}
