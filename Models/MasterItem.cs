using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ttpMiddleware.Models
{
    [Index(nameof(OrgId), nameof(SubOrgId), nameof(ParentId), nameof(ApplicationId), nameof(Active), nameof(Deleted), Name = "masterindex")]
    public partial class MasterItem
    {
        public MasterItem()
        {
            ApplicationPriceApplications = new HashSet<ApplicationPrice>();
            ApplicationPriceCurrencies = new HashSet<ApplicationPrice>();
            ClassSubjectMarkComponents = new HashSet<ClassSubjectMarkComponent>();
            ClassSubjects = new HashSet<ClassSubject>();
            CustomFeatureRolePermissions = new HashSet<CustomFeatureRolePermission>();
            CustomerInvoices = new HashSet<CustomerInvoice>();
            EmpComponents = new HashSet<EmpComponent>();
            EmpEmployeeGradeSalHistoryDepartments = new HashSet<EmpEmployeeGradeSalHistory>();
            EmpEmployeeGradeSalHistoryDesignations = new HashSet<EmpEmployeeGradeSalHistory>();
            EmpEmployeeGradeSalHistoryEmpGrades = new HashSet<EmpEmployeeGradeSalHistory>();
            EmpEmployeeGradeSalHistoryWorkAccounts = new HashSet<EmpEmployeeGradeSalHistory>();
            EmployeeFamilyFamilyRelationShips = new HashSet<EmployeeFamily>();
            EmployeeFamilyGenderNavigations = new HashSet<EmployeeFamily>();
            Exams = new HashSet<Exam>();
            FeeDefinitions = new HashSet<FeeDefinition>();
            InventoryItemCategories = new HashSet<InventoryItem>();
            InventoryItemUnits = new HashSet<InventoryItem>();
            InvoiceComponents = new HashSet<InvoiceComponent>();
            LeaveEmployeeLeaves = new HashSet<LeaveEmployeeLeaf>();
            LeavePolicies = new HashSet<LeavePolicy>();
            OrganizationPayments = new HashSet<OrganizationPayment>();
            Pages = new HashSet<Page>();
            PlanAndMasterItems = new HashSet<PlanAndMasterItem>();
            RoleUsers = new HashSet<RoleUser>();
            StorageFnPs = new HashSet<StorageFnP>();
            StudTeacherClassMappings = new HashSet<StudTeacherClassMapping>();
            StudentBloodgroups = new HashSet<Student>();
            StudentCategories = new HashSet<Student>();
            StudentGenders = new HashSet<Student>();
            StudentGrades = new HashSet<StudentGrade>();
            StudentPermanentAddressCities = new HashSet<Student>();
            StudentPermanentAddressCountries = new HashSet<Student>();
            StudentPermanentAddressStates = new HashSet<Student>();
            StudentReasonForLeavings = new HashSet<Student>();
            StudentReligions = new HashSet<Student>();
            TaskAssignments = new HashSet<TaskAssignment>();
        }

        [Key]
        public int MasterDataId { get; set; }
        [Required]
        [StringLength(50)]
        public string MasterDataName { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [StringLength(256)]
        public string Logic { get; set; }
        public byte? Sequence { get; set; }
        public int? ParentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        public bool Deleted { get; set; }
        public short? DepartmentId { get; set; }
        public short? CustomerPlanId { get; set; }
        public int? ApplicationId { get; set; }
        public byte? Active { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? Confidential { get; set; }

        [ForeignKey(nameof(OrgId))]
        [InverseProperty(nameof(Organization.MasterItems))]
        public virtual Organization Org { get; set; }
        [InverseProperty(nameof(ApplicationPrice.Application))]
        public virtual ICollection<ApplicationPrice> ApplicationPriceApplications { get; set; }
        [InverseProperty(nameof(ApplicationPrice.Currency))]
        public virtual ICollection<ApplicationPrice> ApplicationPriceCurrencies { get; set; }
        [InverseProperty(nameof(ClassSubjectMarkComponent.SubjectComponent))]
        public virtual ICollection<ClassSubjectMarkComponent> ClassSubjectMarkComponents { get; set; }
        [InverseProperty(nameof(ClassSubject.Subject))]
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        [InverseProperty(nameof(CustomFeatureRolePermission.Role))]
        public virtual ICollection<CustomFeatureRolePermission> CustomFeatureRolePermissions { get; set; }
        [InverseProperty(nameof(CustomerInvoice.PaymentStatus))]
        public virtual ICollection<CustomerInvoice> CustomerInvoices { get; set; }
        [InverseProperty(nameof(EmpComponent.ComponentType))]
        public virtual ICollection<EmpComponent> EmpComponents { get; set; }
        [InverseProperty(nameof(EmpEmployeeGradeSalHistory.Department))]
        public virtual ICollection<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistoryDepartments { get; set; }
        [InverseProperty(nameof(EmpEmployeeGradeSalHistory.Designation))]
        public virtual ICollection<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistoryDesignations { get; set; }
        [InverseProperty(nameof(EmpEmployeeGradeSalHistory.EmpGrade))]
        public virtual ICollection<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistoryEmpGrades { get; set; }
        [InverseProperty(nameof(EmpEmployeeGradeSalHistory.WorkAccount))]
        public virtual ICollection<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistoryWorkAccounts { get; set; }
        [InverseProperty(nameof(EmployeeFamily.FamilyRelationShip))]
        public virtual ICollection<EmployeeFamily> EmployeeFamilyFamilyRelationShips { get; set; }
        [InverseProperty(nameof(EmployeeFamily.GenderNavigation))]
        public virtual ICollection<EmployeeFamily> EmployeeFamilyGenderNavigations { get; set; }
        [InverseProperty(nameof(Exam.ExamName))]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty(nameof(FeeDefinition.FeeCategory))]
        public virtual ICollection<FeeDefinition> FeeDefinitions { get; set; }
        [InverseProperty(nameof(InventoryItem.Category))]
        public virtual ICollection<InventoryItem> InventoryItemCategories { get; set; }
        [InverseProperty(nameof(InventoryItem.Unit))]
        public virtual ICollection<InventoryItem> InventoryItemUnits { get; set; }
        [InverseProperty(nameof(InvoiceComponent.Component))]
        public virtual ICollection<InvoiceComponent> InvoiceComponents { get; set; }
        [InverseProperty(nameof(LeaveEmployeeLeaf.LeaveStatus))]
        public virtual ICollection<LeaveEmployeeLeaf> LeaveEmployeeLeaves { get; set; }
        [InverseProperty(nameof(LeavePolicy.LeaveName))]
        public virtual ICollection<LeavePolicy> LeavePolicies { get; set; }
        [InverseProperty(nameof(OrganizationPayment.PaymentModeNavigation))]
        public virtual ICollection<OrganizationPayment> OrganizationPayments { get; set; }
        [InverseProperty(nameof(Page.Application))]
        public virtual ICollection<Page> Pages { get; set; }
        [InverseProperty(nameof(PlanAndMasterItem.MasterData))]
        public virtual ICollection<PlanAndMasterItem> PlanAndMasterItems { get; set; }
        [InverseProperty(nameof(RoleUser.Role))]
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
        [InverseProperty(nameof(StorageFnP.DocType))]
        public virtual ICollection<StorageFnP> StorageFnPs { get; set; }
        [InverseProperty(nameof(StudTeacherClassMapping.Section))]
        public virtual ICollection<StudTeacherClassMapping> StudTeacherClassMappings { get; set; }
        [InverseProperty(nameof(Student.Bloodgroup))]
        public virtual ICollection<Student> StudentBloodgroups { get; set; }
        [InverseProperty(nameof(Student.Category))]
        public virtual ICollection<Student> StudentCategories { get; set; }
        [InverseProperty(nameof(Student.Gender))]
        public virtual ICollection<Student> StudentGenders { get; set; }
        [InverseProperty(nameof(StudentGrade.SubjectCategory))]
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
        [InverseProperty(nameof(Student.PermanentAddressCity))]
        public virtual ICollection<Student> StudentPermanentAddressCities { get; set; }
        [InverseProperty(nameof(Student.PermanentAddressCountry))]
        public virtual ICollection<Student> StudentPermanentAddressCountries { get; set; }
        [InverseProperty(nameof(Student.PermanentAddressState))]
        public virtual ICollection<Student> StudentPermanentAddressStates { get; set; }
        [InverseProperty(nameof(Student.ReasonForLeaving))]
        public virtual ICollection<Student> StudentReasonForLeavings { get; set; }
        [InverseProperty(nameof(Student.Religion))]
        public virtual ICollection<Student> StudentReligions { get; set; }
        [InverseProperty(nameof(TaskAssignment.AssignmentStatus))]
        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
    }
}
