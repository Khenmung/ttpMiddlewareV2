using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Table("Organization")]
    public partial class Organization
    {
        public Organization()
        {
            AccountingLedgerTrialBalances = new HashSet<AccountingLedgerTrialBalance>();
            AccountingVouchers = new HashSet<AccountingVoucher>();
            AppUsers = new HashSet<AppUser>();
            ApplicationPrices = new HashSet<ApplicationPrice>();
            AttendanceReports = new HashSet<AttendanceReport>();
            Attendances = new HashSet<Attendance>();
            Batches = new HashSet<Batch>();
            ClassGroups = new HashSet<ClassGroup>();
            ClassSubjectMarkComponents = new HashSet<ClassSubjectMarkComponent>();
            ClassSubjects = new HashSet<ClassSubject>();
            CustomFeatureRolePermissions = new HashSet<CustomFeatureRolePermission>();
            CustomerInvoiceCustomers = new HashSet<CustomerInvoice>();
            CustomerInvoiceItems = new HashSet<CustomerInvoiceItem>();
            CustomerInvoiceOrgs = new HashSet<CustomerInvoice>();
            CustomerPlans = new HashSet<CustomerPlan>();
            EmpComponents = new HashSet<EmpComponent>();
            EmpEmployeeGradeSalHistories = new HashSet<EmpEmployeeGradeSalHistory>();
            EmpEmployeeGroupOrgs = new HashSet<EmpEmployeeGroup>();
            EmpEmployeeGroupSubOrgs = new HashSet<EmpEmployeeGroup>();
            EmpEmployeeSalaryComponents = new HashSet<EmpEmployeeSalaryComponent>();
            EmpEmployeeSkills = new HashSet<EmpEmployeeSkill>();
            EmpWorkHistories = new HashSet<EmpWorkHistory>();
            EmployeeAttendances = new HashSet<EmployeeAttendance>();
            EmployeeEducationHistories = new HashSet<EmployeeEducationHistory>();
            EmployeeFamilies = new HashSet<EmployeeFamily>();
            EmployeeMonthlySalaries = new HashSet<EmployeeMonthlySalary>();
            EvaluationExamMaps = new HashSet<EvaluationExamMap>();
            ExamSlots = new HashSet<ExamSlot>();
            ExamStudentResults = new HashSet<ExamStudentResult>();
            Exams = new HashSet<Exam>();
            InventoryItems = new HashSet<InventoryItem>();
            InvoiceComponents = new HashSet<InvoiceComponent>();
            LeaveBalances = new HashSet<LeaveBalance>();
            LeaveEmployeeLeaves = new HashSet<LeaveEmployeeLeaf>();
            LeavePolicies = new HashSet<LeavePolicy>();
            LedgerPostings = new HashSet<LedgerPosting>();
            MasterItems = new HashSet<MasterItem>();
            Messages = new HashSet<Message>();
            OrganizationPayments = new HashSet<OrganizationPayment>();
            PageHistories = new HashSet<PageHistory>();
            Pages = new HashSet<Page>();
            PhotoGalleries = new HashSet<PhotoGallery>();
            ReportOrgReportColumns = new HashSet<ReportOrgReportColumn>();
            ReportOrgReportNames = new HashSet<ReportOrgReportName>();
            RoleUsers = new HashSet<RoleUser>();
            SchoolTimeTables = new HashSet<SchoolTimeTable>();
            SlotAndClassSubjects = new HashSet<SlotAndClassSubject>();
            StorageFnPs = new HashSet<StorageFnP>();
            StudTeacherClassMappings = new HashSet<StudTeacherClassMapping>();
            StudentActivities = new HashSet<StudentActivity>();
            StudentCertificates = new HashSet<StudentCertificate>();
            StudentClassSubjects = new HashSet<StudentClassSubject>();
            StudentClasses = new HashSet<StudentClass>();
            StudentFamilyNFriends = new HashSet<StudentFamilyNFriend>();
            StudentFeeReceipts = new HashSet<StudentFeeReceipt>();
            Students = new HashSet<Student>();
            TaskAssignmentComments = new HashSet<TaskAssignmentComment>();
            TaskAssignments = new HashSet<TaskAssignment>();
            TaskConfigurations = new HashSet<TaskConfiguration>();
            VariableConfigurations = new HashSet<VariableConfiguration>();
        }

        [Key]
        public short OrganizationId { get; set; }
        [Required]
        [StringLength(50)]
        public string OrganizationName { get; set; }
        [StringLength(100)]
        public string LogoPath { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        public short? CityId { get; set; }
        public short? StateId { get; set; }
        public short? CountryId { get; set; }
        [StringLength(50)]
        public string Contact { get; set; }
        [StringLength(50)]
        public string RegistrationNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidTo { get; set; }
        [StringLength(254)]
        public string WebSite { get; set; }
        public byte? Active { get; set; }
        public short? ParentId { get; set; }
        public short? MainOrgId { get; set; }
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
        public bool History { get; set; }
        public Guid SyncId { get; set; }

        [InverseProperty(nameof(AccountingLedgerTrialBalance.Org))]
        public virtual ICollection<AccountingLedgerTrialBalance> AccountingLedgerTrialBalances { get; set; }
        [InverseProperty(nameof(AccountingVoucher.Org))]
        public virtual ICollection<AccountingVoucher> AccountingVouchers { get; set; }
        [InverseProperty(nameof(AppUser.Org))]
        public virtual ICollection<AppUser> AppUsers { get; set; }
        [InverseProperty(nameof(ApplicationPrice.Org))]
        public virtual ICollection<ApplicationPrice> ApplicationPrices { get; set; }
        [InverseProperty(nameof(AttendanceReport.Org))]
        public virtual ICollection<AttendanceReport> AttendanceReports { get; set; }
        [InverseProperty(nameof(Attendance.Org))]
        public virtual ICollection<Attendance> Attendances { get; set; }
        [InverseProperty(nameof(Batch.Org))]
        public virtual ICollection<Batch> Batches { get; set; }
        [InverseProperty(nameof(ClassGroup.Org))]
        public virtual ICollection<ClassGroup> ClassGroups { get; set; }
        [InverseProperty(nameof(ClassSubjectMarkComponent.Org))]
        public virtual ICollection<ClassSubjectMarkComponent> ClassSubjectMarkComponents { get; set; }
        [InverseProperty(nameof(ClassSubject.Org))]
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        [InverseProperty(nameof(CustomFeatureRolePermission.Org))]
        public virtual ICollection<CustomFeatureRolePermission> CustomFeatureRolePermissions { get; set; }
        [InverseProperty(nameof(CustomerInvoice.Customer))]
        public virtual ICollection<CustomerInvoice> CustomerInvoiceCustomers { get; set; }
        [InverseProperty(nameof(CustomerInvoiceItem.Org))]
        public virtual ICollection<CustomerInvoiceItem> CustomerInvoiceItems { get; set; }
        [InverseProperty(nameof(CustomerInvoice.Org))]
        public virtual ICollection<CustomerInvoice> CustomerInvoiceOrgs { get; set; }
        [InverseProperty(nameof(CustomerPlan.Org))]
        public virtual ICollection<CustomerPlan> CustomerPlans { get; set; }
        [InverseProperty(nameof(EmpComponent.Org))]
        public virtual ICollection<EmpComponent> EmpComponents { get; set; }
        [InverseProperty(nameof(EmpEmployeeGradeSalHistory.Org))]
        public virtual ICollection<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistories { get; set; }
        [InverseProperty(nameof(EmpEmployeeGroup.Org))]
        public virtual ICollection<EmpEmployeeGroup> EmpEmployeeGroupOrgs { get; set; }
        [InverseProperty(nameof(EmpEmployeeGroup.SubOrg))]
        public virtual ICollection<EmpEmployeeGroup> EmpEmployeeGroupSubOrgs { get; set; }
        [InverseProperty(nameof(EmpEmployeeSalaryComponent.Org))]
        public virtual ICollection<EmpEmployeeSalaryComponent> EmpEmployeeSalaryComponents { get; set; }
        [InverseProperty(nameof(EmpEmployeeSkill.Org))]
        public virtual ICollection<EmpEmployeeSkill> EmpEmployeeSkills { get; set; }
        [InverseProperty(nameof(EmpWorkHistory.Org))]
        public virtual ICollection<EmpWorkHistory> EmpWorkHistories { get; set; }
        [InverseProperty(nameof(EmployeeAttendance.Org))]
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
        [InverseProperty(nameof(EmployeeEducationHistory.Org))]
        public virtual ICollection<EmployeeEducationHistory> EmployeeEducationHistories { get; set; }
        [InverseProperty(nameof(EmployeeFamily.Org))]
        public virtual ICollection<EmployeeFamily> EmployeeFamilies { get; set; }
        [InverseProperty(nameof(EmployeeMonthlySalary.Org))]
        public virtual ICollection<EmployeeMonthlySalary> EmployeeMonthlySalaries { get; set; }
        [InverseProperty(nameof(EvaluationExamMap.Org))]
        public virtual ICollection<EvaluationExamMap> EvaluationExamMaps { get; set; }
        [InverseProperty(nameof(ExamSlot.Org))]
        public virtual ICollection<ExamSlot> ExamSlots { get; set; }
        [InverseProperty(nameof(ExamStudentResult.Org))]
        public virtual ICollection<ExamStudentResult> ExamStudentResults { get; set; }
        [InverseProperty(nameof(Exam.Org))]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty(nameof(InventoryItem.Org))]
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        [InverseProperty(nameof(InvoiceComponent.Org))]
        public virtual ICollection<InvoiceComponent> InvoiceComponents { get; set; }
        [InverseProperty(nameof(LeaveBalance.Org))]
        public virtual ICollection<LeaveBalance> LeaveBalances { get; set; }
        [InverseProperty(nameof(LeaveEmployeeLeaf.Org))]
        public virtual ICollection<LeaveEmployeeLeaf> LeaveEmployeeLeaves { get; set; }
        [InverseProperty(nameof(LeavePolicy.Org))]
        public virtual ICollection<LeavePolicy> LeavePolicies { get; set; }
        [InverseProperty(nameof(LedgerPosting.Org))]
        public virtual ICollection<LedgerPosting> LedgerPostings { get; set; }
        [InverseProperty(nameof(MasterItem.Org))]
        public virtual ICollection<MasterItem> MasterItems { get; set; }
        [InverseProperty(nameof(Message.Org))]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty(nameof(OrganizationPayment.Org))]
        public virtual ICollection<OrganizationPayment> OrganizationPayments { get; set; }
        [InverseProperty(nameof(PageHistory.Org))]
        public virtual ICollection<PageHistory> PageHistories { get; set; }
        [InverseProperty(nameof(Page.Org))]
        public virtual ICollection<Page> Pages { get; set; }
        [InverseProperty(nameof(PhotoGallery.Org))]
        public virtual ICollection<PhotoGallery> PhotoGalleries { get; set; }
        [InverseProperty(nameof(ReportOrgReportColumn.Org))]
        public virtual ICollection<ReportOrgReportColumn> ReportOrgReportColumns { get; set; }
        [InverseProperty(nameof(ReportOrgReportName.Org))]
        public virtual ICollection<ReportOrgReportName> ReportOrgReportNames { get; set; }
        [InverseProperty(nameof(RoleUser.Org))]
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        [InverseProperty(nameof(SchoolTimeTable.Org))]
        public virtual ICollection<SchoolTimeTable> SchoolTimeTables { get; set; }
        [InverseProperty(nameof(SlotAndClassSubject.Org))]
        public virtual ICollection<SlotAndClassSubject> SlotAndClassSubjects { get; set; }
        [InverseProperty(nameof(StorageFnP.Org))]
        public virtual ICollection<StorageFnP> StorageFnPs { get; set; }
        [InverseProperty(nameof(StudTeacherClassMapping.Org))]
        public virtual ICollection<StudTeacherClassMapping> StudTeacherClassMappings { get; set; }
        [InverseProperty(nameof(StudentActivity.Org))]
        public virtual ICollection<StudentActivity> StudentActivities { get; set; }
        [InverseProperty(nameof(StudentCertificate.Org))]
        public virtual ICollection<StudentCertificate> StudentCertificates { get; set; }
        [InverseProperty(nameof(StudentClassSubject.Org))]
        public virtual ICollection<StudentClassSubject> StudentClassSubjects { get; set; }
        [InverseProperty(nameof(StudentClass.Org))]
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        [InverseProperty(nameof(StudentFamilyNFriend.Org))]
        public virtual ICollection<StudentFamilyNFriend> StudentFamilyNFriends { get; set; }
        [InverseProperty(nameof(StudentFeeReceipt.Org))]
        public virtual ICollection<StudentFeeReceipt> StudentFeeReceipts { get; set; }
        [InverseProperty(nameof(Student.Org))]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty(nameof(TaskAssignmentComment.Org))]
        public virtual ICollection<TaskAssignmentComment> TaskAssignmentComments { get; set; }
        [InverseProperty(nameof(TaskAssignment.Org))]
        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
        [InverseProperty(nameof(TaskConfiguration.Org))]
        public virtual ICollection<TaskConfiguration> TaskConfigurations { get; set; }
        [InverseProperty(nameof(VariableConfiguration.Org))]
        public virtual ICollection<VariableConfiguration> VariableConfigurations { get; set; }
    }
}
