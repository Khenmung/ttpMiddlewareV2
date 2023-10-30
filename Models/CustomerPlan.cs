using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class CustomerPlan
    {
        public CustomerPlan()
        {
            CustomerInvoiceItems = new HashSet<CustomerInvoiceItem>();
            OrganizationPayments = new HashSet<OrganizationPayment>();
        }

        [Key]
        public short CustomerPlanId { get; set; }
        public short PlanId { get; set; }
        public short LoginUserCount { get; set; }
        public short PersonOrItemCount { get; set; }
        [StringLength(1000)]
        public string Formula { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountPerMonth { get; set; }
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
        public int? PaymentStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.CustomerPlans))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(PlanId))]
        [InverseProperty("CustomerPlans")]
        public virtual Plan Plan { get; set; }
        [InverseProperty(nameof(CustomerInvoiceItem.CustomerAppItem))]
        public virtual ICollection<CustomerInvoiceItem> CustomerInvoiceItems { get; set; }
        [InverseProperty(nameof(OrganizationPayment.OrganizationPlan))]
        public virtual ICollection<OrganizationPayment> OrganizationPayments { get; set; }
    }
}
