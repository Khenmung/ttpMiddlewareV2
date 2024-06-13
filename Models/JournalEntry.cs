﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("JournalEntry")]
    public partial class JournalEntry
    {
        public JournalEntry()
        {
            LedgerPostings = new HashSet<LedgerPosting>();
        }

        [Key]
        public int JournalEntryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DocDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PostingDate { get; set; }
        [StringLength(30)]
        public string Reference { get; set; }
        public int? GeneralLedgerAccountId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BaseAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
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
        public int LedgerPostingId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(GeneralLedgerAccountId))]
        [InverseProperty(nameof(GeneralLedger.JournalEntryGeneralLedgerAccounts))]
        public virtual GeneralLedger GeneralLedgerAccount { get; set; }
        [ForeignKey(nameof(LedgerPostingId))]
        [InverseProperty(nameof(GeneralLedger.JournalEntryLedgerPostings))]
        public virtual GeneralLedger LedgerPosting { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.JournalEntries))]
        public virtual Organization Org { get; set; }
        [InverseProperty("JournalEntry")]
        public virtual ICollection<LedgerPosting> LedgerPostings { get; set; }
    }
}
