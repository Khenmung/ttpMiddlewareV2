using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Active), nameof(Deleted), Name = "NonClusteredIndex-20230923-191038")]
    public partial class SchoolFeeType
    {
        public SchoolFeeType()
        {
            StudentClasses = new HashSet<StudentClass>();
            StudentFeeTypes = new HashSet<StudentFeeType>();
        }

        [Key]
        public short FeeTypeId { get; set; }
        [Required]
        [StringLength(30)]
        public string FeeTypeName { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        [Required]
        [StringLength(600)]
        public string Formula { get; set; }
        public byte DefaultType { get; set; }
        public bool Confidential { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
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
        public bool History { get; set; }

        [InverseProperty(nameof(StudentClass.FeeType))]
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        [InverseProperty(nameof(StudentFeeType.FeeType))]
        public virtual ICollection<StudentFeeType> StudentFeeTypes { get; set; }
    }
}
