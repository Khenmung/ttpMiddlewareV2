using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentClassSubject")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(BatchId), nameof(ClassId), nameof(SectionId), nameof(Active), nameof(Deleted), nameof(SemesterId), Name = "index_StudentClassSubject_OrgIdBatchIdActiveClassIdSectionIdDeleted")]
    public partial class StudentClassSubject
    {
        public StudentClassSubject()
        {
            ExamResultSubjectMarks = new HashSet<ExamResultSubjectMark>();
        }

        [Key]
        public int StudentClassSubjectId { get; set; }
        public int ClassSubjectId { get; set; }
        public int StudentClassId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short BatchId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int SemesterId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("StudentClassSubjects")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ClassSubjectId))]
        [InverseProperty("StudentClassSubjects")]
        public virtual ClassSubject ClassSubject { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.StudentClassSubjects))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(StudentClassId))]
        [InverseProperty("StudentClassSubjects")]
        public virtual StudentClass StudentClass { get; set; }
        [InverseProperty(nameof(ExamResultSubjectMark.StudentClassSubject))]
        public virtual ICollection<ExamResultSubjectMark> ExamResultSubjectMarks { get; set; }
    }
}
