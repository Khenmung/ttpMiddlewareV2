using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("AccountNature")]
    public partial class AccountNature
    {
        public AccountNature()
        {
            GeneralLedgerAccountGroups = new HashSet<GeneralLedger>();
            GeneralLedgerAccountNatures = new HashSet<GeneralLedger>();
        }

        [Key]
        public int AccountNatureId { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountName { get; set; }
        public int? ParentId { get; set; }
        public bool DebitType { get; set; }
        public short IncomeStatementSequence { get; set; }
        public short TBSequence { get; set; }
        public short ExpenseSequence { get; set; }
        public short AssetSequence { get; set; }
        public short LnESequence { get; set; }
        [Required]
        public bool? Active { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        public bool Deleted { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int AccountTypeId { get; set; }

        [InverseProperty(nameof(GeneralLedger.AccountGroup))]
        public virtual ICollection<GeneralLedger> GeneralLedgerAccountGroups { get; set; }
        [InverseProperty(nameof(GeneralLedger.AccountNature))]
        public virtual ICollection<GeneralLedger> GeneralLedgerAccountNatures { get; set; }
    }
}
