using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentAdditional")]
    public partial class StudentAdditional
    {
        [Key]
        public int StudentAdditionalId { get; set; }
        public int StudentId { get; set; }
        public int LabelId { get; set; }
        [Required]
        [StringLength(100)]
        public string ContentVal { get; set; }
        public short? OrgId { get; set; }
        public int? SubOrgId { get; set; }
        public bool? Deleted { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public Guid? SyncId { get; set; }
        public bool History { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("StudentAdditionals")]
        public virtual Student Student { get; set; }
    }
}
