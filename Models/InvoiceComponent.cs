using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class InvoiceComponent
    {
        [Key]
        public short InvoiceComponentId { get; set; }
        public int CustomerInvoiceId { get; set; }
        public int ComponentId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
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

        [ForeignKey(nameof(ComponentId))]
        [InverseProperty(nameof(MasterItem.InvoiceComponents))]
        public virtual MasterItem Component { get; set; }
        [ForeignKey(nameof(CustomerInvoiceId))]
        [InverseProperty("InvoiceComponents")]
        public virtual CustomerInvoice CustomerInvoice { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.InvoiceComponents))]
        public virtual Organization Org { get; set; }
    }
}
