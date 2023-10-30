using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(UserId), Name = "IX_RefreshTokens_UserId")]
    public partial class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [StringLength(450)]
        public string Token { get; set; }
        [StringLength(450)]
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUser.RefreshTokens))]
        public virtual AspNetUser User { get; set; }
    }
}
