using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("EvaluationExamMap")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Active), nameof(Deleted), Name = "NonClusteredIndex-20230830-212636")]
    public partial class EvaluationExamMap
    {
        public EvaluationExamMap()
        {
            StudentEvaluationResults = new HashSet<StudentEvaluationResult>();
        }

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
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(EvaluationMasterId))]
        [InverseProperty("EvaluationExamMaps")]
        public virtual EvaluationMaster EvaluationMaster { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.EvaluationExamMaps))]
        public virtual Organization Org { get; set; }
        [InverseProperty(nameof(StudentEvaluationResult.EvaluationExamMap))]
        public virtual ICollection<StudentEvaluationResult> StudentEvaluationResults { get; set; }
    }
}
