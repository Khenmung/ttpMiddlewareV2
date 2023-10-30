using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ttpMiddleware.Data;
using ttpMiddleware.Models;

#nullable disable

namespace ttpMiddleware.RawModels
{
    public class RawMasterItem:ISync
    {
        private ttpauthContext _context;
        public RawMasterItem(ttpauthContext context)
        {
            _context=context;
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
        public string ParentName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(450)]
        public string CreatedBy { get; set; }
        public short OrgId { get; set; }
        public int SubOrgId { get; set; }
        public bool Deleted { get; set; }
        public short? DepartmentId { get; set; }
        public short? CustomerPlanId { get; set; }
        public string CustomerPlanName { get; set; }
        public int? ApplicationId { get; set; }
        public byte? Active { get; set; }
        [StringLength(450)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? Confidential { get; set; }

        public async Task<IActionResult> Sync(JProperty data)
        {
            List<RawMasterItem> _data = data.Value.ToObject<List<RawMasterItem>>();

            foreach (var item in _data)
            {
                var _parentId = await _context.MasterItems.Where(x => x.MasterDataId == item.ParentId
                && x.OrgId == item.OrgId
                && x.SubOrgId == item.SubOrgId).FirstOrDefaultAsync();
                if (_parentId != null)
                {
                    var _localdata = await _context.MasterItems.Where(x => x.MasterDataId == item.MasterDataId
                                && x.OrgId == item.OrgId
                                && x.SubOrgId == item.SubOrgId).FirstOrDefaultAsync();

                    if (_localdata != null)
                    {
                        _localdata.MasterDataName = item.MasterDataName;
                        _context.Update(item);
                    }
                    else
                    {
                        _context.Add(item);
                    }
                }
                else
                {
                    _parentId = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == item.ParentName.ToLower()
                                        && x.OrgId == item.OrgId
                                        && x.SubOrgId == item.SubOrgId).FirstOrDefaultAsync();
                    if( _parentId != null)
                    {

                    }
                }
            }
            return null;
        }

       
        //[ForeignKey(nameof(OrgId))]
        //[InverseProperty(nameof(Organization.MasterItems))]
        //public class RawOrganization Org { get; set; }
        //[InverseProperty(nameof(ApplicationPrice.Application))]
        //public class RawICollection<ApplicationPrice> ApplicationPriceApplications { get; set; }
        //[InverseProperty(nameof(ApplicationPrice.Currency))]
        //public class RawICollection<ApplicationPrice> ApplicationPriceCurrencies { get; set; }
        //[InverseProperty(nameof(ClassSubjectMarkComponent.SubjectComponent))]
        //public class RawICollection<ClassSubjectMarkComponent> ClassSubjectMarkComponents { get; set; }
        //[InverseProperty(nameof(ClassSubject.Subject))]
        //public class RawICollection<ClassSubject> ClassSubjects { get; set; }
        //[InverseProperty(nameof(CustomFeatureRolePermission.Role))]
        //public class RawICollection<CustomFeatureRolePermission> CustomFeatureRolePermissions { get; set; }
        //[InverseProperty(nameof(CustomerInvoice.PaymentStatus))]
        //public class RawICollection<CustomerInvoice> CustomerInvoices { get; set; }
        //[InverseProperty(nameof(EmpComponent.ComponentType))]
        //public class RawICollection<EmpComponent> EmpComponents { get; set; }
        //[InverseProperty(nameof(EmpEmployeeGradeSalHistory.Department))]
        //public class RawICollection<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistoryDepartments { get; set; }
        //[InverseProperty(nameof(EmpEmployeeGradeSalHistory.Designation))]
        //public class RawICollection<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistoryDesignations { get; set; }
        //[InverseProperty(nameof(EmpEmployeeGradeSalHistory.EmpGrade))]
        //public class RawICollection<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistoryEmpGrades { get; set; }
        //[InverseProperty(nameof(EmpEmployeeGradeSalHistory.WorkAccount))]
        //public class RawICollection<EmpEmployeeGradeSalHistory> EmpEmployeeGradeSalHistoryWorkAccounts { get; set; }
        //[InverseProperty(nameof(EmployeeFamily.FamilyRelationShip))]
        //public class RawICollection<EmployeeFamily> EmployeeFamilyFamilyRelationShips { get; set; }
        //[InverseProperty(nameof(EmployeeFamily.GenderNavigation))]
        //public class RawICollection<EmployeeFamily> EmployeeFamilyGenderNavigations { get; set; }
        //[InverseProperty(nameof(ExamStudentSubjectResult.ExamStatusNavigation))]
        //public class RawICollection<ExamStudentSubjectResult> ExamStudentSubjectResults { get; set; }
        //[InverseProperty(nameof(Exam.ExamName))]
        //public class RawICollection<Exam> Exams { get; set; }
        //[InverseProperty(nameof(FeeDefinition.FeeCategory))]
        //public class RawICollection<FeeDefinition> FeeDefinitions { get; set; }
        //[InverseProperty(nameof(InventoryItem.Category))]
        //public class RawICollection<InventoryItem> InventoryItemCategories { get; set; }
        //[InverseProperty(nameof(InventoryItem.Unit))]
        //public class RawICollection<InventoryItem> InventoryItemUnits { get; set; }
        //[InverseProperty(nameof(InvoiceComponent.Component))]
        //public class RawICollection<InvoiceComponent> InvoiceComponents { get; set; }
        //[InverseProperty(nameof(LeaveEmployeeLeaf.LeaveStatus))]
        //public class RawICollection<LeaveEmployeeLeaf> LeaveEmployeeLeaves { get; set; }
        //[InverseProperty(nameof(LeavePolicy.LeaveName))]
        //public class RawICollection<LeavePolicy> LeavePolicies { get; set; }
        //[InverseProperty(nameof(OrganizationPayment.PaymentModeNavigation))]
        //public class RawICollection<OrganizationPayment> OrganizationPayments { get; set; }
        //[InverseProperty(nameof(Page.Application))]
        //public class RawICollection<Page> Pages { get; set; }
        //[InverseProperty(nameof(PlanAndMasterItem.MasterData))]
        //public class RawICollection<PlanAndMasterItem> PlanAndMasterItems { get; set; }
        //[InverseProperty(nameof(RoleUser.Role))]
        //public class RawICollection<RoleUser> RoleUsers { get; set; }
        //[InverseProperty(nameof(StorageFnP.DocType))]
        //public class RawICollection<StorageFnP> StorageFnPs { get; set; }
        //[InverseProperty(nameof(StudTeacherClassMapping.Section))]
        //public class RawICollection<StudTeacherClassMapping> StudTeacherClassMappings { get; set; }
        //[InverseProperty(nameof(Student.Bloodgroup))]
        //public class RawICollection<Student> StudentBloodgroups { get; set; }
        //[InverseProperty(nameof(Student.Category))]
        //public class RawICollection<Student> StudentCategories { get; set; }
        //[InverseProperty(nameof(Student.Gender))]
        //public class RawICollection<Student> StudentGenders { get; set; }
        //[InverseProperty(nameof(StudentGrade.SubjectCategory))]
        //public class RawICollection<StudentGrade> StudentGrades { get; set; }
        //[InverseProperty(nameof(Student.PermanentAddressCity))]
        //public class RawICollection<Student> StudentPermanentAddressCities { get; set; }
        //[InverseProperty(nameof(Student.PermanentAddressCountry))]
        //public class RawICollection<Student> StudentPermanentAddressCountries { get; set; }
        //[InverseProperty(nameof(Student.PermanentAddressState))]
        //public class RawICollection<Student> StudentPermanentAddressStates { get; set; }
        //[InverseProperty(nameof(Student.ReasonForLeaving))]
        //public class RawICollection<Student> StudentReasonForLeavings { get; set; }
        //[InverseProperty(nameof(Student.Religion))]
        //public class RawICollection<Student> StudentReligions { get; set; }
        //[InverseProperty(nameof(TaskAssignment.AssignmentStatus))]
        //public class RawICollection<TaskAssignment> TaskAssignments { get; set; }
    }
}
