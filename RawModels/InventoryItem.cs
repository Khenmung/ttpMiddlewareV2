using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawInventoryItem
    {
      

        [Key]
        public int InventoryItemId { get; set; }
        [StringLength(20)]
        public string SKU { get; set; }
        [Required]
        [StringLength(20)]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(100)]
        public string ShortName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int? UnitId { get; set; }
        public short? QtyPerUnit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PPP { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PPU { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MinPrice { get; set; }
        public short? MinCount { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

       
    }
}
