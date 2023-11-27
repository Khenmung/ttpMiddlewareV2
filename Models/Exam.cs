using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(BatchId), nameof(Active), nameof(Deleted), Name = "ExamIndex")]
    public partial class Exam
    {
        public Exam()
        {
            ClassEvaluations = new HashSet<ClassEvaluation>();
            ExamClassGroupMaps = new HashSet<ExamClassGroupMap>();
            ExamMarkConfigs = new HashSet<ExamMarkConfig>();
            ExamNCalculates = new HashSet<ExamNCalculate>();
            ExamResultSubjectMarks = new HashSet<ExamResultSubjectMark>();
            ExamSlots = new HashSet<ExamSlot>();
            ExamStudentResults = new HashSet<ExamStudentResult>();
            QuestionBankNExams = new HashSet<QuestionBankNExam>();
            StudentGrades = new HashSet<StudentGrade>();
            TotalAttendances = new HashSet<TotalAttendance>();
        }

        [Key]
        public short ExamId { get; set; }
        public int ExamNameId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public byte? ReleaseResult { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReleaseDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttendanceStartDate { get; set; }
        [StringLength(100)]
        public string MarkFormula { get; set; }
        public int? CategoryId { get; set; }
        public int? ClassGroupId { get; set; }
        public short? BatchId { get; set; }
        public short Sequence { get; set; }
        public byte? Active { get; set; }
        public bool Deleted { get; set; }
        public short OrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public int SubOrgId { get; set; }
        public int WithHeldResultStatusId { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("Exams")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ExamNameId))]
        [InverseProperty(nameof(MasterItem.Exams))]
        public virtual MasterItem ExamName { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.Exams))]
        public virtual Organization Org { get; set; }
        [InverseProperty(nameof(ClassEvaluation.Exam))]
        public virtual ICollection<ClassEvaluation> ClassEvaluations { get; set; }
        [InverseProperty(nameof(ExamClassGroupMap.Exam))]
        public virtual ICollection<ExamClassGroupMap> ExamClassGroupMaps { get; set; }
        [InverseProperty(nameof(ExamMarkConfig.Exam))]
        public virtual ICollection<ExamMarkConfig> ExamMarkConfigs { get; set; }
        [InverseProperty(nameof(ExamNCalculate.Exam))]
        public virtual ICollection<ExamNCalculate> ExamNCalculates { get; set; }
        [InverseProperty(nameof(ExamResultSubjectMark.Exam))]
        public virtual ICollection<ExamResultSubjectMark> ExamResultSubjectMarks { get; set; }
        [InverseProperty(nameof(ExamSlot.Exam))]
        public virtual ICollection<ExamSlot> ExamSlots { get; set; }
        [InverseProperty(nameof(ExamStudentResult.Exam))]
        public virtual ICollection<ExamStudentResult> ExamStudentResults { get; set; }
        [InverseProperty(nameof(QuestionBankNExam.Exam))]
        public virtual ICollection<QuestionBankNExam> QuestionBankNExams { get; set; }
        [InverseProperty(nameof(StudentGrade.Exam))]
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
        [InverseProperty(nameof(TotalAttendance.Exam))]
        public virtual ICollection<TotalAttendance> TotalAttendances { get; set; }
    }
}
