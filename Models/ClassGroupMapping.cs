using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ClassGroupMapping")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Deleted), Name = "NonClusteredIndex-20230830-205313")]
    public partial class ClassGroupMapping
    {
        [Key]
        public short ClassGroupMappingId { get; set; }
        public int ClassId { get; set; }
        public short ClassGroupId { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public short BatchId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }
        public int SectionId { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.ClassGroupMappings))]
        public virtual ClassMaster Class { get; set; }
        [ForeignKey(nameof(ClassGroupId))]
        [InverseProperty("ClassGroupMappings")]
        public virtual ClassGroup ClassGroup { get; set; }
    }
}
