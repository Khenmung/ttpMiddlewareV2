using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("AccountingLedgerTrialBalance")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(BatchId), nameof(TotalDebit), nameof(Balance), nameof(StudentClassId), nameof(MonthDisplay), nameof(Active), nameof(Deleted), Name = "NonClusteredIndex-20231005-194615")]
    public partial class AccountingLedgerTrialBalance
    {
        public AccountingLedgerTrialBalance()
        {
            AccountingVouchers = new HashSet<AccountingVoucher>();
        }

        [Key]
        public int LedgerId { get; set; }
        public int? StudentClassId { get; set; }
        public int? EmployeeId { get; set; }
        public int? GeneralLedgerId { get; set; }
        public int? MonthDisplay { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BaseAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalDebit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCredit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int ClassId { get; set; }
        public int SemesterId { get; set; }
        public int SectionId { get; set; }
        public int Month { get; set; }
        public bool History { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("AccountingLedgerTrialBalances")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(EmpEmployee.AccountingLedgerTrialBalances))]
        public virtual EmpEmployee Employee { get; set; }
        [ForeignKey(nameof(GeneralLedgerId))]
        [InverseProperty("AccountingLedgerTrialBalances")]
        public virtual GeneralLedger GeneralLedger { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.AccountingLedgerTrialBalances))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("AccountingLedgerTrialBalances")]
        public virtual StudentClass StudentClass { get; set; }
        [InverseProperty(nameof(AccountingVoucher.Ledger))]
        public virtual ICollection<AccountingVoucher> AccountingVouchers { get; set; }
    }
}
