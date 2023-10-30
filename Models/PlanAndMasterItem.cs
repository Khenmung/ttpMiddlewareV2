using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("PlanAndMasterItem")]
    public partial class PlanAndMasterItem
    {
        [Key]
        public short PlanAndMasterDataId { get; set; }
        public short PlanId { get; set; }
        public int MasterDataId { get; set; }
        public byte Active { get; set; }
        public short ApplicationId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }

        [ForeignKey(nameof(MasterDataId))]
        [InverseProperty(nameof(MasterItem.PlanAndMasterItems))]
        public virtual MasterItem MasterData { get; set; }
        [ForeignKey(nameof(PlanId))]
        [InverseProperty("PlanAndMasterItems")]
        public virtual Plan Plan { get; set; }
    }
}
