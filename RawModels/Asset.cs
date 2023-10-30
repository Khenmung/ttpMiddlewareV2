using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawAsset
    {
        [Key]
        public int AssetId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [StringLength(80)]
        public string AssetName { get; set; }
        [StringLength(20)]
        public string SerialNo { get; set; }
        public short? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        public int? DepreciationTypeId { get; set; }
        public int? AssetLocationId { get; set; }
        [StringLength(250)]
        public string LocationDetail { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PurchaseDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        public int? PurchaseTypeId { get; set; }
        [StringLength(15)]
        public string UsefulLife { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? ResidualValue { get; set; }
        public int? ConditionId { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public short? OrgId { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public int SubOrgId { get; set; }
    }
}
