using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("PackageDetail")]
    public partial class PackageDetail
    {
        [Key]
        public Guid SyncId { get; set; }
        [Required]
        [StringLength(30)]
        public string TableName { get; set; }
        [Required]
        [StringLength(30)]
        public string PackageName { get; set; }
        [Required]
        [StringLength(50)]
        public string PackagePath { get; set; }
        public short Sequence { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Required]
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool? History { get; set; }
    }
}
