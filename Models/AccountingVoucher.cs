﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class AccountingVoucher
    {
        public AccountingVoucher()
        {
            LedgerPostings = new HashSet<LedgerPosting>();
        }

        [Key]
        public int AccountingVoucherId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DocDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PostingDate { get; set; }
        [StringLength(30)]
        public string Reference { get; set; }
        public int LedgerId { get; set; }
        public int TranParentId { get; set; }
        public int? GeneralLedgerAccountId { get; set; }
        public int? FeeReceiptId { get; set; }
        public int? ParentId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BaseAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
        public int? Month { get; set; }
        public short? ClassFeeId { get; set; }
        [Required]
        [StringLength(256)]
        public string ShortText { get; set; }
        public bool? Debit { get; set; }
        public short OrgId { get; set; }
        public int? SubOrgId { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int ActivityTypeId { get; set; }
        public int LedgerPostingId { get; set; }
        public bool History { get; set; }

        [ForeignKey(nameof(ClassFeeId))]
        [InverseProperty("AccountingVouchers")]
        public virtual ClassFee ClassFee { get; set; }
        [ForeignKey(nameof(FeeReceiptId))]
        [InverseProperty(nameof(StudentFeeReceipt.AccountingVouchers))]
        public virtual StudentFeeReceipt FeeReceipt { get; set; }
        [ForeignKey(nameof(GeneralLedgerAccountId))]
        [InverseProperty(nameof(GeneralLedger.AccountingVoucherGeneralLedgerAccounts))]
        public virtual GeneralLedger GeneralLedgerAccount { get; set; }
        [ForeignKey(nameof(LedgerId))]
        [InverseProperty(nameof(AccountingLedgerTrialBalance.AccountingVouchers))]
        public virtual AccountingLedgerTrialBalance Ledger { get; set; }
        [ForeignKey(nameof(LedgerPostingId))]
        [InverseProperty(nameof(GeneralLedger.AccountingVoucherLedgerPostings))]
        public virtual GeneralLedger LedgerPosting { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.AccountingVouchers))]
        public virtual Organization Org { get; set; }
        [InverseProperty("AccountingVoucher")]
        public virtual ICollection<LedgerPosting> LedgerPostings { get; set; }
    }
}
