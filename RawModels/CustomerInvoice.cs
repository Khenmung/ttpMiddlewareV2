using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("CustomerInvoice")]
    public class RawCustomerInvoice
    {
        public RawCustomerInvoice()
        {
           
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

        
    }
}
