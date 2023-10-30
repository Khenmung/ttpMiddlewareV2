using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(ApplicationId), nameof(Deleted), Name = "NonClusteredIndex-20230904-201642")]
    public partial class ReportConfigItem
    {
        public ReportConfigItem()
        {
            ReportOrgReportNames = new HashSet<ReportOrgReportName>();
        }

        [Key]
        public int ReportConfigItemId { get; set; }
        [Required]
        [StringLength(50)]
        public string ReportName { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        public int ParentId { get; set; }
        public int ApplicationId { get; set; }
        public short? OrgId { get; set; }
        [StringLength(3000)]
        public string Formula { get; set; }
        public byte? ColumnSequence { get; set; }
        public int? DropdownId { get; set; }
        [StringLength(450)]
        public string TableNames { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        public short BatchId { get; set; }
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

        [InverseProperty(nameof(ReportOrgReportName.ReportConfigData))]
        public virtual ICollection<ReportOrgReportName> ReportOrgReportNames { get; set; }
    }
}
