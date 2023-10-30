using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("ApplicationFeatureRolesPerm")]
    public class RawApplicationFeatureRolesPerm
    {
        [Key]
        public int ApplicationFeatureRoleId { get; set; }
        public short PlanFeatureId { get; set; }
        public int RoleId { get; set; }
        public short? PermissionId { get; set; }
        public short? OrgId { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

       
        
    }
}
