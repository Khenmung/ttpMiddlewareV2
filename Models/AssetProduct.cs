using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("AssetProduct")]
    public partial class AssetProduct
    {
        [Key]
        public int AssetProductId { get; set; }
        [Required]
        [StringLength(30)]
        public string AssetProductName { get; set; }
        public int? AssetProductCategoryId { get; set; }
        public int? AssetProductTypeId { get; set; }
        public int? AssetVendorId { get; set; }
        public bool Active { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }
    }
}
