using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class Plan
    {
        public Plan()
        {
            CustomerPlanFeatures = new HashSet<CustomerPlanFeature>();
            CustomerPlans = new HashSet<CustomerPlan>();
            PlanAndMasterItems = new HashSet<PlanAndMasterItem>();
            PlanFeatures = new HashSet<PlanFeature>();
        }

        [Key]
        public short PlanId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public byte Sequence { get; set; }
        [StringLength(1000)]
        public string Logic { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PCPM { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MinPrice { get; set; }
        public short? MinCount { get; set; }
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

        [InverseProperty(nameof(CustomerPlanFeature.Plan))]
        public virtual ICollection<CustomerPlanFeature> CustomerPlanFeatures { get; set; }
        [InverseProperty(nameof(CustomerPlan.Plan))]
        public virtual ICollection<CustomerPlan> CustomerPlans { get; set; }
        [InverseProperty(nameof(PlanAndMasterItem.Plan))]
        public virtual ICollection<PlanAndMasterItem> PlanAndMasterItems { get; set; }
        [InverseProperty(nameof(PlanFeature.Plan))]
        public virtual ICollection<PlanFeature> PlanFeatures { get; set; }
    }
}
