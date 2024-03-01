using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ApplicationPrice")]
    public partial class ApplicationPrice
    {
        [Key]
        public short ApplicationPriceId { get; set; }
        public int ApplicationId { get; set; }
        public byte PCPM { get; set; }
        public byte MinCount { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal? MinPrice { get; set; }
        public int? CurrencyId { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(ApplicationId))]
        [InverseProperty(nameof(MasterItem.ApplicationPriceApplications))]
        public virtual MasterItem Application { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        [InverseProperty(nameof(MasterItem.ApplicationPriceCurrencies))]
        public virtual MasterItem Currency { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.ApplicationPrices))]
        public virtual Organization Org { get; set; }
    }
}
