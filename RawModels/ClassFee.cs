using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.RawModels
{
    [Table("ClassFee")]
    public class RawClassFee
    {
        public RawClassFee()
        {
            
        }

        [Key]
        public short ClassFeeId { get; set; }
        public short FeeDefinitionId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? SemesterId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public short BatchId { get; set; }
        public int Month { get; set; }
        public byte? Recurring { get; set; }
        public byte Active { get; set; }
        public int? LocationId { get; set; }
        public byte? PaymentOrder { get; set; }
        public short OrgId { get; set; }
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

      
    }
}
