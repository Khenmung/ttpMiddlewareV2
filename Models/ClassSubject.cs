using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("ClassSubject")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(BatchId), nameof(Active), nameof(Deleted), Name = "classsubjectindex")]
    public partial class ClassSubject
    {
        public ClassSubject()
        {
            Attendances = new HashSet<Attendance>();
            ClassSubjectMarkComponents = new HashSet<ClassSubjectMarkComponent>();
            ExamMarkConfigs = new HashSet<ExamMarkConfig>();
            SlotAndClassSubjects = new HashSet<SlotAndClassSubject>();
            StudentClassSubjects = new HashSet<StudentClassSubject>();
            SubjectComponents = new HashSet<SubjectComponent>();
            TeacherSubjects = new HashSet<TeacherSubject>();
        }

        [Key]
        public int ClassSubjectId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public short SubjectTypeId { get; set; }
        public int? SubjectCategoryId { get; set; }
        public byte? Credits { get; set; }
        public int? TeacherId { get; set; }
        public bool? Confidential { get; set; }
        public byte Active { get; set; }
        public short OrgId { get; set; }
        public short? BatchId { get; set; }
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
        public int SectionId { get; set; }
        public int SemesterId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("ClassSubjects")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.ClassSubjects))]
        public virtual ClassMaster Class { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.ClassSubjects))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(SubjectId))]
        [InverseProperty(nameof(MasterItem.ClassSubjects))]
        public virtual MasterItem Subject { get; set; }
        [ForeignKey(nameof(SubjectTypeId))]
        [InverseProperty("ClassSubjects")]
        public virtual SubjectType SubjectType { get; set; }
        [ForeignKey(nameof(TeacherId))]
        [InverseProperty(nameof(EmpEmployee.ClassSubjects))]
        public virtual EmpEmployee Teacher { get; set; }
        [InverseProperty(nameof(Attendance.ClassSubject))]
        public virtual ICollection<Attendance> Attendances { get; set; }
        [InverseProperty(nameof(ClassSubjectMarkComponent.ClassSubject))]
        public virtual ICollection<ClassSubjectMarkComponent> ClassSubjectMarkComponents { get; set; }
        [InverseProperty(nameof(ExamMarkConfig.ClassSubject))]
        public virtual ICollection<ExamMarkConfig> ExamMarkConfigs { get; set; }
        [InverseProperty(nameof(SlotAndClassSubject.ClassSubject))]
        public virtual ICollection<SlotAndClassSubject> SlotAndClassSubjects { get; set; }
        [InverseProperty(nameof(StudentClassSubject.ClassSubject))]
        public virtual ICollection<StudentClassSubject> StudentClassSubjects { get; set; }
        [InverseProperty(nameof(SubjectComponent.ClassSubject))]
        public virtual ICollection<SubjectComponent> SubjectComponents { get; set; }
        [InverseProperty(nameof(TeacherSubject.ClassSubject))]
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}
