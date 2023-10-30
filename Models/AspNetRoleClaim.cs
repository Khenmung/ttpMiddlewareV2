using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(RoleId), Name = "IX_AspNetRoleClaims_RoleId")]
    public partial class AspNetRoleClaim
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

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(AspNetRole.AspNetRoleClaims))]
        public virtual AspNetRole Role { get; set; }
    }
}
