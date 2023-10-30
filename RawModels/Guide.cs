using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawGuide
    {
        [Key]
        public short GuideId { get; set; }
        public int? ApplicationId { get; set; }
        public int? ModuleId { get; set; }
        public int? PageId { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }
    }
}
