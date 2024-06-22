﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("Config_table")]
    public partial class Config_table
    {
        [Key]
        public int ConfigTableId { get; set; }
        [Required]
        [StringLength(200)]
        public string TableName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastUpdatedColumn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdatedValue { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Required]
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public bool History { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
