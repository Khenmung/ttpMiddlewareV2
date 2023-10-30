using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ClassGroup")]
    public partial class ClassGroup
    {
        public ClassGroup()
        {
            ClassGroupMappings = new HashSet<ClassGroupMapping>();
            ExamClassGroupMaps = new HashSet<ExamClassGroupMap>();
        }

        [Key]
        public short ClassGroupId { get; set; }
        [Required]
        [StringLength(50)]
        public string GroupName { get; set; }
        public int ClassGroupTypeId { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
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

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.ClassGroups))]
        public virtual Organization Org { get; set; }
        [InverseProperty(nameof(ClassGroupMapping.ClassGroup))]
        public virtual ICollection<ClassGroupMapping> ClassGroupMappings { get; set; }
        [InverseProperty(nameof(ExamClassGroupMap.ClassGroup))]
        public virtual ICollection<ExamClassGroupMap> ExamClassGroupMaps { get; set; }
    }
}
