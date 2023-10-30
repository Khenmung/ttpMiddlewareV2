using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class StudentFeeReceipt
    {
        public StudentFeeReceipt()
        {
            AccountingVouchers = new HashSet<AccountingVoucher>();
        }

        [Key]
        public int StudentFeeReceiptId { get; set; }
        public int StudentClassId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
        public int ReceiptNo { get; set; }
        [StringLength(20)]
        public string OffLineReceiptNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReceiptDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? AdjustedAccountId { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Balance { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("StudentFeeReceipts")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.StudentFeeReceipts))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("StudentFeeReceipts")]
        public virtual StudentClass StudentClass { get; set; }
        [InverseProperty(nameof(AccountingVoucher.FeeReceipt))]
        public virtual ICollection<AccountingVoucher> AccountingVouchers { get; set; }
    }
}
