﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class CustomerPlanFeature
    {
        [Key]
        public short CustomerPlanFeatureId { get; set; }
        [Required]
        [StringLength(250)]
        public string FeatureName { get; set; }
        public short PlanId { get; set; }
        public byte Active { get; set; }
        public byte Sequence { get; set; }
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
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(PlanId))]
        [InverseProperty("CustomerPlanFeatures")]
        public virtual Plan Plan { get; set; }
    }
}
