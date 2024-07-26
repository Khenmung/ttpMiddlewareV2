using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class InventoryItem
    {
        public InventoryItem()
        {
            CustomerInvoiceItems = new HashSet<CustomerInvoiceItem>();
        }

        [Key]
        public int InventoryItemId { get; set; }
        [Required]
        [StringLength(100)]
        public string ShortName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public int? CategoryId { get; set; }
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
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(MasterItem.InventoryItems))]
        public virtual MasterItem Category { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.InventoryItems))]
        public virtual Organization Org { get; set; }
        [InverseProperty("InventoryNavigation")]
        public virtual Inventory Inventory { get; set; }
        [InverseProperty(nameof(CustomerInvoiceItem.InventoryItem))]
        public virtual ICollection<CustomerInvoiceItem> CustomerInvoiceItems { get; set; }
    }
}
