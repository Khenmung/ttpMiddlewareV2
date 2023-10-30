using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("FeePaymentRelated")]
    public partial class FeePaymentRelated
    {
        [Key]
        public int FeePaymentRelatedId { get; set; }
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
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
        public int ClassId { get; set; }

        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("FeePaymentRelateds")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
