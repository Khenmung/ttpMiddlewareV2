using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("CustomerInvoice")]
    public partial class CustomerInvoice
    {
        public CustomerInvoice()
        {
            CustomerInvoiceItems = new HashSet<CustomerInvoiceItem>();
            InvoiceComponents = new HashSet<InvoiceComponent>();
        }

        [Key]
        public int CustomerInvoiceId { get; set; }
        public short? CustomerId { get; set; }
        public int? StudentClassId { get; set; }
        public short? DueForMonth { get; set; }
        [Column(TypeName = "date")]
        public DateTime InvoiceDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DueDate { get; set; }
        public int PaymentStatusId { get; set; }
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

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(Organization.CustomerInvoiceCustomers))]
        public virtual Organization Customer { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.CustomerInvoiceOrgs))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(PaymentStatusId))]
        [InverseProperty(nameof(MasterItem.CustomerInvoices))]
        public virtual MasterItem PaymentStatus { get; set; }
        [InverseProperty(nameof(CustomerInvoiceItem.CustomerInvoice))]
        public virtual ICollection<CustomerInvoiceItem> CustomerInvoiceItems { get; set; }
        [InverseProperty(nameof(InvoiceComponent.CustomerInvoice))]
        public virtual ICollection<InvoiceComponent> InvoiceComponents { get; set; }
    }
}
