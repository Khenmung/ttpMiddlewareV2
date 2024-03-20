using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentActivity")]
    public partial class StudentActivity
    {
        [Key]
        public short StudentActivityId { get; set; }
        public int? StudentClassId { get; set; }
        public int? StudentId { get; set; }
        public int? GroupId { get; set; }
        [StringLength(1000)]
        public string Activity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActivityDate { get; set; }
        public byte? Active { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public short? OrgId { get; set; }
        public short? BatchId { get; set; }
        public short? TeacherId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool History { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("StudentActivities")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.StudentActivities))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("StudentActivities")]
        public virtual Student Student { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("StudentActivities")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
