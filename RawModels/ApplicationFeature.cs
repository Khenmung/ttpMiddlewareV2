using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawApplicationFeature
    {
        [Key]
        public short ApplicationFeatureId { get; set; }
        [Required]
        [StringLength(250)]
        public string FeatureName { get; set; }
        public int ApplicationId { get; set; }
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

        //[ForeignKey(nameof(ApplicationId))]
        //[InverseProperty(nameof(MasterItem.ApplicationFeatures))]
        //public class RawMasterItem Application { get; set; }
    }
}
