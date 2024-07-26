using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("Inventory")]
    public partial class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int InventoryItemId { get; set; }
        [Required]
        [StringLength(50)]
        public string SKU { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? QtyPerUnit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PPU { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PPP { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? CPU { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? CPP { get; set; }
        public int PaymentTypeId { get; set; }
        public int? ModelId { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public int StatusId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StatusDate { get; set; }
        public int? CustomerId { get; set; }
        public int? VendorId { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public bool History { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Required]
        [StringLength(150)]
        public string PhotoPath { get; set; }
        [Required]
        [StringLength(150)]
        public string QrcodePath { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MfgDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        public int? LocationId { get; set; }

        [ForeignKey(nameof(InventoryId))]
        [InverseProperty(nameof(InventoryItem.Inventory))]
        public virtual InventoryItem InventoryNavigation { get; set; }
    }
}
