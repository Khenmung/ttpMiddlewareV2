using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ConfigTable")]
    public partial class ConfigTable
    {
        public ConfigTable()
        {
            PackageDetails = new HashSet<PackageDetail>();
        }

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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public bool History { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }

        [InverseProperty(nameof(PackageDetail.ConfigTable))]
        public virtual ICollection<PackageDetail> PackageDetails { get; set; }
    }
}
