using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(PageId), nameof(PageTitle), nameof(ApplicationId), nameof(label), nameof(Active), Name = "NonClusteredIndex-20230923-192959")]
    public partial class Page
    {
        public Page()
        {
            PageHistories = new HashSet<PageHistory>();
            PlanFeatures = new HashSet<PlanFeature>();
        }

        [Key]
        public short PageId { get; set; }
        [Required]
        [StringLength(100)]
        public string PageTitle { get; set; }
        public short ParentId { get; set; }
        public int ApplicationId { get; set; }
        [Required]
        [StringLength(100)]
        public string label { get; set; }
        [Required]
        [StringLength(200)]
        public string link { get; set; }
        public byte Active { get; set; }
        public short? OrgId { get; set; }
        [StringLength(100)]
        public string faIcon { get; set; }
        [StringLength(100)]
        public string FullPath { get; set; }
        [StringLength(250)]
        public string PhotoPath { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public byte? PlanId { get; set; }
        public byte? IsTemplate { get; set; }
        public short? DisplayOrder { get; set; }
        public byte? HasSubmenu { get; set; }
        public byte? HomePage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(ApplicationId))]
        [InverseProperty(nameof(MasterItem.Pages))]
        public virtual MasterItem Application { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.Pages))]
        public virtual Organization Org { get; set; }
        [InverseProperty(nameof(PageHistory.ParentPage))]
        public virtual ICollection<PageHistory> PageHistories { get; set; }
        [InverseProperty(nameof(PlanFeature.Page))]
        public virtual ICollection<PlanFeature> PlanFeatures { get; set; }
    }
}
