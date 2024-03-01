using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("CustomFeatureRolePermission")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(ApplicationId), nameof(RoleId), nameof(Active), nameof(Deleted), Name = "NonClusteredIndex-20230923-194737")]
    public partial class CustomFeatureRolePermission
    {
        [Key]
        public int CustomFeatureRolePermissionId { get; set; }
        public int CustomFeatureId { get; set; }
        public int RoleId { get; set; }
        public byte PermissionId { get; set; }
        public int ApplicationId { get; set; }
        public int TableNameId { get; set; }
        public short OrgId { get; set; }
        [Required]
        public bool? Active { get; set; }
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

        [ForeignKey(nameof(CustomFeatureId))]
        [InverseProperty("CustomFeatureRolePermissions")]
        public virtual CustomFeature CustomFeature { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.CustomFeatureRolePermissions))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(MasterItem.CustomFeatureRolePermissions))]
        public virtual MasterItem Role { get; set; }
    }
}
