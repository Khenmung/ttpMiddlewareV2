using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(Active), nameof(Deleted), nameof(ClassId), nameof(ClassName), nameof(Sequence), Name = "classmaster")]
    public partial class ClassMaster
    {
        public ClassMaster()
        {
            ClassFees = new HashSet<ClassFee>();
            ClassGroupMappings = new HashSet<ClassGroupMapping>();
            ClassSubjects = new HashSet<ClassSubject>();
            CourseYearSemesters = new HashSet<CourseYearSemester>();
            ExamMarkConfigs = new HashSet<ExamMarkConfig>();
            SchoolClassPeriods = new HashSet<SchoolClassPeriod>();
            SchoolTimeTables = new HashSet<SchoolTimeTable>();
            StudTeacherClassMappings = new HashSet<StudTeacherClassMapping>();
            StudentClasses = new HashSet<StudentClass>();
            StudentEvaluationResults = new HashSet<StudentEvaluationResult>();
            TotalAttendances = new HashSet<TotalAttendance>();
        }

        [Key]
        public int ClassId { get; set; }
        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }
        public int? DurationId { get; set; }
        public byte? MinStudent { get; set; }
        public short? MaxStudent { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public int? StudyAreaId { get; set; }
        public int? StudyModeId { get; set; }
        public short? BatchId { get; set; }
        public byte? Sequence { get; set; }
        public bool? Confidential { get; set; }
        public byte Active { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public short OrgId { get; set; }
        public bool Deleted { get; set; }
        public int SubOrgId { get; set; }
        public int CategoryId { get; set; }
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [InverseProperty(nameof(ClassFee.Class))]
        public virtual ICollection<ClassFee> ClassFees { get; set; }
        [InverseProperty(nameof(ClassGroupMapping.Class))]
        public virtual ICollection<ClassGroupMapping> ClassGroupMappings { get; set; }
        [InverseProperty(nameof(ClassSubject.Class))]
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        [InverseProperty(nameof(CourseYearSemester.Class))]
        public virtual ICollection<CourseYearSemester> CourseYearSemesters { get; set; }
        [InverseProperty(nameof(ExamMarkConfig.Class))]
        public virtual ICollection<ExamMarkConfig> ExamMarkConfigs { get; set; }
        [InverseProperty(nameof(SchoolClassPeriod.Class))]
        public virtual ICollection<SchoolClassPeriod> SchoolClassPeriods { get; set; }
        [InverseProperty(nameof(SchoolTimeTable.Class))]
        public virtual ICollection<SchoolTimeTable> SchoolTimeTables { get; set; }
        [InverseProperty(nameof(StudTeacherClassMapping.Class))]
        public virtual ICollection<StudTeacherClassMapping> StudTeacherClassMappings { get; set; }
        [InverseProperty(nameof(StudentClass.Class))]
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        [InverseProperty(nameof(StudentEvaluationResult.Class))]
        public virtual ICollection<StudentEvaluationResult> StudentEvaluationResults { get; set; }
        [InverseProperty(nameof(TotalAttendance.Class))]
        public virtual ICollection<TotalAttendance> TotalAttendances { get; set; }
    }
}
