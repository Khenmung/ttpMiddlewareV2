using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawReportOrgReportColumn
    {
        [Key]
        public short ReportOrgReportColumnId { get; set; }
        public short ReportOrgReportNameId { get; set; }
        [Required]
        [StringLength(50)]
        public string ColumnDisplayName { get; set; }
        [Required]
        [StringLength(1000)]
        public string FormulaOrColumnName { get; set; }
        public byte Sequence { get; set; }
        public short OrgId { get; set; }
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

      
    }
}
