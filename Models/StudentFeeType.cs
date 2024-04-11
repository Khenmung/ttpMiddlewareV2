using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentFeeType")]
    [Index(nameof(StudentClassId), nameof(FeeTypeId), nameof(OrgId), nameof(SubOrgId), nameof(Active), nameof(Deleted), Name = "NonClusteredIndex-20240314-121322")]
    public partial class StudentFeeType
    {
        [Key]
        public int StudentFeeTypeId { get; set; }
        public short FeeTypeId { get; set; }
        public int StudentClassId { get; set; }
        public int FromMonth { get; set; }
        public int ToMonth { get; set; }
        public bool Active { get; set; }
        public bool IsCurrent { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        public bool Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Discount { get; set; }
        public short BatchId { get; set; }

        [ForeignKey(nameof(FeeTypeId))]
        [InverseProperty(nameof(SchoolFeeType.StudentFeeTypes))]
        public virtual SchoolFeeType FeeType { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("StudentFeeTypes")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
