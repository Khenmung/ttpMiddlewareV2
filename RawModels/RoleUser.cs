using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("RoleUser")]
    public class RawRoleUser
    {
        [Key]
        public short RoleUserId { get; set; }
        public int RoleId { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        public short OrgId { get; set; }
        public short? DepartmentId { get; set; }
        public short? LocationId { get; set; }
        public short? BatchId { get; set; }
        public byte Active { get; set; }
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

      
    }
}
