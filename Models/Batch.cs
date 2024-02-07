using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    public partial class Batch
    {
        public Batch()
        {
            AccountingLedgerTrialBalances = new HashSet<AccountingLedgerTrialBalance>();
            AttendanceReports = new HashSet<AttendanceReport>();
            Attendances = new HashSet<Attendance>();
            ClassSubjectMarkComponents = new HashSet<ClassSubjectMarkComponent>();
            ClassSubjects = new HashSet<ClassSubject>();
            EmployeeAttendances = new HashSet<EmployeeAttendance>();
            Events = new HashSet<Event>();
            ExamSlots = new HashSet<ExamSlot>();
            ExamStudentResults = new HashSet<ExamStudentResult>();
            Exams = new HashSet<Exam>();
            LeaveBalances = new HashSet<LeaveBalance>();
            LeavePolicies = new HashSet<LeavePolicy>();
            RoleUsers = new HashSet<RoleUser>();
            SchoolTimeTables = new HashSet<SchoolTimeTable>();
            SlotAndClassSubjects = new HashSet<SlotAndClassSubject>();
            StudTeacherClassMappings = new HashSet<StudTeacherClassMapping>();
            StudentActivities = new HashSet<StudentActivity>();
            StudentCertificates = new HashSet<StudentCertificate>();
            StudentClassSubjects = new HashSet<StudentClassSubject>();
            StudentClasses = new HashSet<StudentClass>();
            StudentFeeReceipts = new HashSet<StudentFeeReceipt>();
            TeacherSubjects = new HashSet<TeacherSubject>();
        }

        [Key]
        public short BatchId { get; set; }
        [Required]
        [StringLength(50)]
        public string BatchName { get; set; }
        public byte CurrentBatch { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        public short OrgId { get; set; }
        public byte Active { get; set; }
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

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.Batches))]
        public virtual Organization Org { get; set; }
        [InverseProperty(nameof(AccountingLedgerTrialBalance.Batch))]
        public virtual ICollection<AccountingLedgerTrialBalance> AccountingLedgerTrialBalances { get; set; }
        [InverseProperty(nameof(AttendanceReport.FinancialYearNavigation))]
        public virtual ICollection<AttendanceReport> AttendanceReports { get; set; }
        [InverseProperty(nameof(Attendance.Batch))]
        public virtual ICollection<Attendance> Attendances { get; set; }
        [InverseProperty(nameof(ClassSubjectMarkComponent.Batch))]
        public virtual ICollection<ClassSubjectMarkComponent> ClassSubjectMarkComponents { get; set; }
        [InverseProperty(nameof(ClassSubject.Batch))]
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        [InverseProperty(nameof(EmployeeAttendance.Batch))]
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
        [InverseProperty(nameof(Event.Batch))]
        public virtual ICollection<Event> Events { get; set; }
        [InverseProperty(nameof(ExamSlot.Batch))]
        public virtual ICollection<ExamSlot> ExamSlots { get; set; }
        [InverseProperty(nameof(ExamStudentResult.Batch))]
        public virtual ICollection<ExamStudentResult> ExamStudentResults { get; set; }
        [InverseProperty(nameof(Exam.Batch))]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty(nameof(LeaveBalance.Batch))]
        public virtual ICollection<LeaveBalance> LeaveBalances { get; set; }
        [InverseProperty(nameof(LeavePolicy.Batch))]
        public virtual ICollection<LeavePolicy> LeavePolicies { get; set; }
        [InverseProperty(nameof(RoleUser.Batch))]
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        [InverseProperty(nameof(SchoolTimeTable.Batch))]
        public virtual ICollection<SchoolTimeTable> SchoolTimeTables { get; set; }
        [InverseProperty(nameof(SlotAndClassSubject.Batch))]
        public virtual ICollection<SlotAndClassSubject> SlotAndClassSubjects { get; set; }
        [InverseProperty(nameof(StudTeacherClassMapping.Batch))]
        public virtual ICollection<StudTeacherClassMapping> StudTeacherClassMappings { get; set; }
        [InverseProperty(nameof(StudentActivity.Batch))]
        public virtual ICollection<StudentActivity> StudentActivities { get; set; }
        [InverseProperty(nameof(StudentCertificate.Batch))]
        public virtual ICollection<StudentCertificate> StudentCertificates { get; set; }
        [InverseProperty(nameof(StudentClassSubject.Batch))]
        public virtual ICollection<StudentClassSubject> StudentClassSubjects { get; set; }
        [InverseProperty(nameof(StudentClass.Batch))]
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        [InverseProperty(nameof(StudentFeeReceipt.Batch))]
        public virtual ICollection<StudentFeeReceipt> StudentFeeReceipts { get; set; }
        [InverseProperty(nameof(TeacherSubject.Batch))]
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}
