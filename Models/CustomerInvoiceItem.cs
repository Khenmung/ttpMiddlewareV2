using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class CustomerInvoiceItem
    {
        [Key]
        public int CustomerInvoiceItemId { get; set; }
        public int CustomerInvoiceId { get; set; }
        public int? InventoryItemId { get; set; }
        public short? ClassFeeId { get; set; }
        public short? CustomerAppItemId { get; set; }
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

        [ForeignKey(nameof(CustomerAppItemId))]
        [InverseProperty(nameof(CustomerPlan.CustomerInvoiceItems))]
        public virtual CustomerPlan CustomerAppItem { get; set; }
        [ForeignKey(nameof(CustomerInvoiceId))]
        [InverseProperty("CustomerInvoiceItems")]
        public virtual CustomerInvoice CustomerInvoice { get; set; }
        [ForeignKey(nameof(InventoryItemId))]
        [InverseProperty("CustomerInvoiceItems")]
        public virtual InventoryItem InventoryItem { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.CustomerInvoiceItems))]
        public virtual Organization Org { get; set; }
    }
}
