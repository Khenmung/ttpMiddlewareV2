using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(BatchId), nameof(Active), nameof(Deleted), Name = "NonClusteredIndex-20230830-202157")]
    public partial class ClassSubjectMarkComponent
    {
        public ClassSubjectMarkComponent()
        {
            ExamStudentSubjectResults = new HashSet<ExamStudentSubjectResult>();
        }

        [Key]
        public short ClassSubjectMarkComponentId { get; set; }
        public int ClassSubjectId { get; set; }
        public int SubjectComponentId { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal FullMark { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal PassMark { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal? OverallPassMark { get; set; }
        public short? ExamId { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int SectionId { get; set; }
        public int SemesterId { get; set; }
        public int ClassId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("ClassSubjectMarkComponents")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ClassSubjectId))]
        [InverseProperty("ClassSubjectMarkComponents")]
        public virtual ClassSubject ClassSubject { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.ClassSubjectMarkComponents))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(SubjectComponentId))]
        [InverseProperty(nameof(MasterItem.ClassSubjectMarkComponents))]
        public virtual MasterItem SubjectComponent { get; set; }
        [InverseProperty(nameof(ExamStudentSubjectResult.ClassSubjectMarkComponent))]
        public virtual ICollection<ExamStudentSubjectResult> ExamStudentSubjectResults { get; set; }
    }
}
