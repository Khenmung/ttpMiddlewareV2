using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Index(nameof(RoleId), Name = "IX_AspNetRoleClaims_RoleId")]
    public class RawAspNetRoleClaim
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoleId { get; set; }
        [StringLength(256)]
        public string ClaimType { get; set; }
        [StringLength(256)]
        public string ClaimValue { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
