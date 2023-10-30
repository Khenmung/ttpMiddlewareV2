using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("CustomFeatureRolePermission")]
    public class RawCustomFeatureRolePermission
    {
        [Key]
        public int CustomFeatureRolePermissionId { get; set; }
        public int CustomFeatureId { get; set; }
        public int RoleId { get; set; }
        public byte PermissionId { get; set; }
        public int ApplicationId { get; set; }
        public int TableNameId { get; set; }
        public int SubOrgId { get; set; }
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

       
    }
}
