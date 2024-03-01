using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("OrgPaymentDetail")]
    public partial class OrgPaymentDetail
    {
        [Key]
        public int OrgPaymentDetailId { get; set; }
        public int OrgPaymentId { get; set; }
        [Required]
        [StringLength(100)]
        public string ItemName { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Discount { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Rate { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }
        [Required]
        public bool? Active { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(OrgPaymentDetailId))]
        [InverseProperty(nameof(OrganizationPayment.OrgPaymentDetail))]
        public virtual OrganizationPayment OrgPaymentDetailNavigation { get; set; }
    }
}
