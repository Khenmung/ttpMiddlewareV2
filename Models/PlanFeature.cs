using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(PlanId), nameof(ApplicationId), nameof(Active), nameof(Deleted), Name = "NonClusteredIndex-20230923-191338")]
    public partial class PlanFeature
    {
        public PlanFeature()
        {
            ApplicationFeatureRolesPerms = new HashSet<ApplicationFeatureRolesPerm>();
        }

        [Key]
        public short PlanFeatureId { get; set; }
        public short PlanId { get; set; }
        public short PageId { get; set; }
        public int? ApplicationId { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(PageId))]
        [InverseProperty("PlanFeatures")]
        public virtual Page Page { get; set; }
        [ForeignKey(nameof(PlanId))]
        [InverseProperty("PlanFeatures")]
        public virtual Plan Plan { get; set; }
        [InverseProperty(nameof(ApplicationFeatureRolesPerm.PlanFeature))]
        public virtual ICollection<ApplicationFeatureRolesPerm> ApplicationFeatureRolesPerms { get; set; }
    }
}
