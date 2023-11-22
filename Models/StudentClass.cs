using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("StudentClass")]
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(BatchId), nameof(IsCurrent), nameof(StudentClassId), nameof(ClassId), nameof(Deleted), Name = "Indx_StudentclassOrgId_BatchId_StudentClassId_ClassId_")]
    public partial class StudentClass
    {
        public StudentClass()
        {
            AccountingLedgerTrialBalances = new HashSet<AccountingLedgerTrialBalance>();
            Attendances = new HashSet<Attendance>();
            ExamResultSubjectMarks = new HashSet<ExamResultSubjectMark>();
            ExamStudentResults = new HashSet<ExamStudentResult>();
            GroupActivityParticipants = new HashSet<GroupActivityParticipant>();
            StorageFnPs = new HashSet<StorageFnP>();
            StudentActivities = new HashSet<StudentActivity>();
            StudentCertificates = new HashSet<StudentCertificate>();
            StudentClassSubjects = new HashSet<StudentClassSubject>();
            StudentFeeReceipts = new HashSet<StudentFeeReceipt>();
            StudentStatuses = new HashSet<StudentStatus>();
            TaskAssignments = new HashSet<TaskAssignment>();
        }

        [Key]
        public int StudentClassId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        [StringLength(30)]
        public string RollNo { get; set; }
        public int? HouseId { get; set; }
        public int? SectionId { get; set; }
        public short BatchId { get; set; }
        public short? FeeTypeId { get; set; }
        public int? SemesterId { get; set; }
        public int? CourseYearId { get; set; }
        [StringLength(10)]
        public string AdmissionNo { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public byte Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        public byte? Promoted { get; set; }
        [StringLength(50)]
        public string PhotoPath { get; set; }
        public bool Deleted { get; set; }
        [Required]
        public bool? IsCurrent { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("StudentClasses")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(ClassMaster.StudentClasses))]
        public virtual ClassMaster Class { get; set; }
        [ForeignKey(nameof(FeeTypeId))]
        [InverseProperty(nameof(SchoolFeeType.StudentClasses))]
        public virtual SchoolFeeType FeeType { get; set; }
        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.StudentClasses))]
        public virtual Organization Org { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("StudentClasses")]
        public virtual Student Student { get; set; }
        [InverseProperty(nameof(AccountingLedgerTrialBalance.StudentClass))]
        public virtual ICollection<AccountingLedgerTrialBalance> AccountingLedgerTrialBalances { get; set; }
        [InverseProperty(nameof(Attendance.StudentClass))]
        public virtual ICollection<Attendance> Attendances { get; set; }
        [InverseProperty(nameof(ExamResultSubjectMark.StudentClass))]
        public virtual ICollection<ExamResultSubjectMark> ExamResultSubjectMarks { get; set; }
        [InverseProperty(nameof(ExamStudentResult.StudentClass))]
        public virtual ICollection<ExamStudentResult> ExamStudentResults { get; set; }
        [InverseProperty(nameof(GroupActivityParticipant.StudentClass))]
        public virtual ICollection<GroupActivityParticipant> GroupActivityParticipants { get; set; }
        [InverseProperty(nameof(StorageFnP.StudentClass))]
        public virtual ICollection<StorageFnP> StorageFnPs { get; set; }
        [InverseProperty(nameof(StudentActivity.StudentClass))]
        public virtual ICollection<StudentActivity> StudentActivities { get; set; }
        [InverseProperty(nameof(StudentCertificate.StudentClass))]
        public virtual ICollection<StudentCertificate> StudentCertificates { get; set; }
        [InverseProperty(nameof(StudentClassSubject.StudentClass))]
        public virtual ICollection<StudentClassSubject> StudentClassSubjects { get; set; }
        [InverseProperty(nameof(StudentFeeReceipt.StudentClass))]
        public virtual ICollection<StudentFeeReceipt> StudentFeeReceipts { get; set; }
        [InverseProperty(nameof(StudentStatus.StudentClass))]
        public virtual ICollection<StudentStatus> StudentStatuses { get; set; }
        [InverseProperty(nameof(TaskAssignment.AssignedToClass))]
        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
    }
}
