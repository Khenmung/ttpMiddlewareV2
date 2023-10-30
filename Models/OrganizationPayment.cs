using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("OrganizationPayment")]
    public partial class OrganizationPayment
    {
        [Key]
        public int OrganizationPaymentId { get; set; }
        public short OrgId { get; set; }
        public short OrganizationPlanId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? AmountPerMonth { get; set; }
        public short NoOfStudents { get; set; }
        [Column(TypeName = "decimal(2, 2)")]
        public decimal Discount { get; set; }
        public short PaidMonths { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PaymentDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public int PaymentMode { get; set; }
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

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.OrganizationPayments))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(OrganizationPlanId))]
        [InverseProperty(nameof(CustomerPlan.OrganizationPayments))]
        public virtual CustomerPlan OrganizationPlan { get; set; }
        [ForeignKey(nameof(PaymentMode))]
        [InverseProperty(nameof(MasterItem.OrganizationPayments))]
        public virtual MasterItem PaymentModeNavigation { get; set; }
        [InverseProperty("OrgPaymentDetailNavigation")]
        public virtual OrgPaymentDetail OrgPaymentDetail { get; set; }
    }
}
