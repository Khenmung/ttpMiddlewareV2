using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("PageHistory")]
    public partial class PageHistory
    {
        [Key]
        public short PageHistoryId { get; set; }
        [Required]
        public string PageBody { get; set; }
        [StringLength(2000)]
        public string PageLeft { get; set; }
        [StringLength(2000)]
        public string PageRight { get; set; }
        public byte Version { get; set; }
        public short ParentPageId { get; set; }
        public byte Published { get; set; }
        public short? OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.PageHistories))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(ParentPageId))]
        [InverseProperty(nameof(Page.PageHistories))]
        public virtual Page ParentPage { get; set; }
    }
}
