using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Index(nameof(NormalizedEmail), Name = "EmailIndex")]
    public class RawAspNetUser
    {
        public RawAspNetUser()
        {
           
        }

        [Key]
        public string Id { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ValidFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ValidTo { get; set; }
        public int OrgId { get; set; }
        public int Active { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string NormalizedUserName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        [StringLength(500)]
        public string PasswordHash { get; set; }
        [StringLength(500)]
        public string SecurityStamp { get; set; }
        [StringLength(2000)]
        public string ConcurrencyStamp { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int UserTypeId { get; set; }
        public int SubOrgId { get; set; }

      
    }
}
