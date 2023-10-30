using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("FeePaymentAndStatus")]
    public partial class FeePaymentAndStatus
    {
        [Key]
        public int FeepaymentAndStatusId { get; set; }
        public int StudentClassId { get; set; }
        public int FeepaymentStatusId { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Required]
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }

        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("FeePaymentAndStatuses")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
