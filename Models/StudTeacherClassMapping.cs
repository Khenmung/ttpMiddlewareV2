using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudTeacherClassMapping")]
    public partial class StudTeacherClassMapping
    {
        [Key]
        public int TeacherClassMappingId { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int? HelperId { get; set; }
        public int SectionId { get; set; }
        public short BatchId { get; set; }
        public short OrgId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte Active { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("StudTeacherClassMappings")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.StudTeacherClassMappings))]
        public virtual ClassMaster Class { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.StudTeacherClassMappings))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(SectionId))]
        [InverseProperty(nameof(MasterItem.StudTeacherClassMappings))]
        public virtual MasterItem Section { get; set; }
        [ForeignKey(nameof(TeacherId))]
        [InverseProperty(nameof(EmpEmployee.StudTeacherClassMappings))]
        public virtual EmpEmployee Teacher { get; set; }
    }
}
