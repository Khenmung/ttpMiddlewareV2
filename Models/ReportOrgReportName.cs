using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class ReportOrgReportName
    {
        public ReportOrgReportName()
        {
            ReportOrgReportColumns = new HashSet<ReportOrgReportColumn>();
        }

        [Key]
        public short ReportOrgReportNameId { get; set; }
        public int ReportConfigDataId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserReportName { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.ReportOrgReportNames))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(ReportConfigDataId))]
        [InverseProperty(nameof(ReportConfigItem.ReportOrgReportNames))]
        public virtual ReportConfigItem ReportConfigData { get; set; }
        [InverseProperty(nameof(ReportOrgReportColumn.ReportOrgReportName))]
        public virtual ICollection<ReportOrgReportColumn> ReportOrgReportColumns { get; set; }
    }
}
