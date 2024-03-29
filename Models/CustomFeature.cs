﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("CustomFeature")]
    public partial class CustomFeature
    {
        public CustomFeature()
        {
            CustomFeatureRolePermissions = new HashSet<CustomFeatureRolePermission>();
        }

        [Key]
        public int CustomFeatureId { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomFeatureName { get; set; }
        public int ApplicationId { get; set; }
        [Required]
        [StringLength(30)]
        public string TableName { get; set; }
        public int TableRowId { get; set; }
        public int? TableNameId { get; set; }
        public short OrgId { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public bool? Deleted { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [InverseProperty(nameof(CustomFeatureRolePermission.CustomFeature))]
        public virtual ICollection<CustomFeatureRolePermission> CustomFeatureRolePermissions { get; set; }
    }
}
