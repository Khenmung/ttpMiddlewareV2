using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("LedgerPosting")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Active), nameof(Reference), nameof(Deleted), nameof(History), Name = "NonClusteredIndex-20240608-154206")]
    public partial class LedgerPosting
    {
        [Key]
        public int LedgerPostingId { get; set; }
        public int JournalEntryId { get; set; }
        public int PostingGeneralLedgerId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public bool Debit { get; set; }
        [Required]
        [StringLength(256)]
        public string ShortText { get; set; }
        [StringLength(30)]
        public string Reference { get; set; }
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
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(JournalEntryId))]
        [InverseProperty("LedgerPostings")]
        public virtual JournalEntry JournalEntry { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.LedgerPostings))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(PostingGeneralLedgerId))]
        [InverseProperty(nameof(GeneralLedger.LedgerPostings))]
        public virtual GeneralLedger PostingGeneralLedger { get; set; }
    }
}
