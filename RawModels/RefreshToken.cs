﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Index(nameof(UserId), Name = "IX_RefreshTokens_UserId")]
    public class RawRefreshToken
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

      
    }
}
