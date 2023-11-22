using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.OData.Edm;
using ttpMiddleware.Data.Entities;
using ttpMiddleware.Models;

namespace ttpMiddleware.Configuration
{
    public static class EDMModel
    {
        public static IEdmModel getEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<IdentityUser>("IdentityUsers");
            builder.EntitySet<AspNetUser>("AspNetUsers");
            builder.EntitySet<ApplicationUser>("AuthManagement");
            builder.EntitySet<ApplicationUser>("UserColls");
            builder.EntitySet<Page>("Pages");
            builder.EntitySet<PageHistory>("PageHistories");
            builder.EntitySet<PhotoGallery>("PhotoGalleries");
            builder.EntitySet<Message>("Messages");
            builder.EntitySet<StorageFnP>("StorageFnPs");
            builder.EntitySet<ClassFee>("ClassFees");
            builder.EntitySet<Student>("Students");
            builder.EntitySet<StudentClass>("StudentClasses");
            //builder.EntitySet<StudentFeePayment>("StudentFeePayments");
            builder.EntitySet<StudentFeeReceipt>("StudentFeeReceipts");
            builder.EntitySet<TaskConfiguration>("TaskConfigurations");
            //builder.EntitySet<ApplicationUser>("ApplicationUsers");
            builder.EntitySet<RoleUser>("RoleUsers");
            builder.EntitySet<Attendance>("Attendances");
            builder.EntitySet<Organization>("Organizations");
            builder.EntitySet<ClassSubject>("ClassSubjects");
            builder.EntitySet<Exam>("Exams");
            builder.EntitySet<ExamSlot>("ExamSlots");
            builder.EntitySet<SlotAndClassSubject>("SlotAndClassSubjects");
            builder.EntitySet<StudentClassSubject>("StudentClassSubjects");
            //builder.EntitySet<StudentActivity>("StudentActivities");
            builder.EntitySet<SubjectType>("SubjectTypes");
            builder.EntitySet<ClassSubjectMarkComponent>("ClassSubjectMarkComponents");
            builder.EntitySet<ExamStudentResult>("ExamStudentResults");
            builder.EntitySet<ExamStudentSubjectResult>("ExamStudentSubjectResults");
            builder.EntitySet<Batch>("Batches");
            builder.EntitySet<StudentCertificate>("StudentCertificates");
            builder.EntitySet<ApplicationFeature>("ApplicationFeatures");
            builder.EntitySet<ApplicationFeatureRolesPerm>("ApplicationFeatureRolesPerms");

            builder.EntitySet<EmpEmployee>("EmpEmployees");
            builder.EntitySet<EmpEmployeeSkill>("EmpEmployeeSkills");
            builder.EntitySet<EmployeeFamily>("EmployeeFamilies");
            builder.EntitySet<LeaveEmployeeLeaf>("LeaveEmployeeLeaves");
            builder.EntitySet<EmployeeMonthlySalary>("EmployeeMonthlySalaries");
            builder.EntitySet<EmpEmployeeSalaryComponent>("EmpEmployeeSalaryComponents");
            builder.EntitySet<EmpComponent>("EmpComponents");
            builder.EntitySet<EmpEmployeeSkill>("EmpEmployeeSkills");
            builder.EntitySet<EmployeeEducationHistory>("EmployeeEducationHistories");
            builder.EntitySet<EmpWorkHistory>("EmpWorkHistories");
            builder.EntitySet<LeaveEmployeeLeaf>("LeaveEmployeeLeaves");
            builder.EntitySet<EmpEmployeeGradeSalHistory>("EmpEmployeeGradeSalHistories");
            builder.EntitySet<VariableConfiguration>("VariableConfigurations");

            builder.EntitySet<EmpManagerGroupMapping>("EmpManagerGroupMappings");
            builder.EntitySet<EmpEmployeeGroup>("EmpEmployeeGroups");
            builder.EntitySet<StudTeacherClassMapping>("StudTeacherClassMappings");
            builder.EntitySet<AccountingVoucher>("AccountingVouchers");
            builder.EntitySet<AccountingLedgerTrialBalance>("AccountingLedgerTrialBalances");
            builder.EntitySet<SchoolFeeType>("SchoolFeeTypes");
            builder.EntitySet<SchoolClassPeriod>("SchoolClassPeriods");
            builder.EntitySet<SchoolTimeTable>("SchoolTimeTables");
            builder.EntitySet<TaskAssignment>("TaskAssignments");
            builder.EntitySet<TaskAssignmentComment>("TaskAssignmentComments");
            builder.EntitySet<LeaveBalance>("LeaveBalances");
            builder.EntitySet<MasterItem>("MasterItems");
            builder.EntitySet<AttendanceReport>("AttendanceReports");
            builder.EntitySet<LeavePolicy>("LeavePolicies");

            builder.EntitySet<ReportConfigItem>("ReportConfigItems");
            builder.EntitySet<ReportOrgReportName>("ReportOrgReportNames");
            builder.EntitySet<ReportOrgReportColumn>("ReportOrgReportColumns");

            builder.EntitySet<ApplicationPrice>("ApplicationPrices");
            builder.EntitySet<InventoryItem>("InventoryItems");
            builder.EntitySet<CustomerInvoice>("CustomerInvoices");
            //builder.EntitySet<CustomerApp>("CustomerApps");
            builder.EntitySet<CustomerInvoiceComponent>("CustomerInvoiceComponents");
            builder.EntitySet<ClassMaster>("ClassMasters");
            builder.EntitySet<ClassGroup>("ClassGroups");
            builder.EntitySet<Event>("Events");
            builder.EntitySet<Holiday>("Holidays");
            builder.EntitySet<FeeDefinition>("FeeDefinitions");
            builder.EntitySet<Plan>("Plans");
            builder.EntitySet<PlanFeature>("PlanFeatures");
            builder.EntitySet<CustomerPlan>("CustomerPlans");
            builder.EntitySet<PlanAndMasterItem>("PlanAndMasterItems");
            builder.EntitySet<EmployeeActivity>("EmployeeActivities");
            builder.EntitySet<OrganizationPayment>("OrganizationPayments");
            builder.EntitySet<GeneralLedger>("GeneralLedgers");
            builder.EntitySet<ClassEvaluation>("ClassEvaluations");
            //builder.EntitySet<EmployeeEvaluation>("EmployeeEvaluations");
            builder.EntitySet<StudentEvaluation>("StudentEvaluations");
            builder.EntitySet<StudentEvaluationResult>("StudentEvaluationResults");
            //builder.EntitySet<EmployeeEvaluationDetail>("EmployeeEvaluationDetails");
            builder.EntitySet<ClassEvaluationOption>("ClassEvaluationOptions");
            builder.EntitySet<EvaluationExamMap>("EvaluationExamMaps");
            builder.EntitySet<EvaluationMaster>("EvaluationMasters");
            builder.EntitySet<StudentFamilyNFriend>("StudentFamilyNFriends");
            builder.EntitySet<CustomerPlanFeature>("CustomerPlanFeatures");
            builder.EntitySet<ClassGroupMapping>("ClassGroupMappings");
            builder.EntitySet<ErrorLog>("ErrorLogs");
            builder.EntitySet<SportResult>("SportResults");
            builder.EntitySet<AccountNature>("AccountNatures");
            builder.EntitySet<TeacherSubject>("TeacherSubjects");
            builder.EntitySet<CustomFeatureRolePermission>("CustomFeatureRolePermissions");
            builder.EntitySet<CustomFeature>("CustomFeatures");
            builder.EntitySet<RulesOrPolicy>("RulesOrPolicies");
            builder.EntitySet<StudentGrade>("StudentGrades");
            builder.EntitySet<ExamResultSubjectMark>("ExamResultSubjectMarks");
            builder.EntitySet<TeacherPeriod>("TeacherPeriods");
            builder.EntitySet<TotalAttendance>("TotalAttendances");
            builder.EntitySet<EmployeeTotalAttedance>("EmployeeTotalAttedances");
            builder.EntitySet<ExamNCalculate>("ExamNCalculates");
            builder.EntitySet<GeneratedCertificate>("GeneratedCertificates");
            builder.EntitySet<QuestionBank>("QuestionBanks");
            builder.EntitySet<QuestionBankNExam>("QuestionBankNExams");
            builder.EntitySet<GroupActivityParticipant>("GroupActivityParticipants");
            builder.EntitySet<GroupPoint>("GroupPoints");
            builder.EntitySet<AchievementAndPoint>("AchievementAndPoints");
            builder.EntitySet<CertificateConfig>("CertificateConfigs");
            builder.EntitySet<SyllabusDetail>("SyllabusDetails");
            builder.EntitySet<EmployeeActivity>("EmployeeActivities");
            //builder.EntitySet<EmployeeGroupActivityParticipant>("EmployeeGroupActivityParticipants");
            //builder.EntitySet<EmployeeGeneratedCertificate>("EmployeeGeneratedCertificates");
            builder.EntitySet<ExamMarkConfig>("ExamMarkConfigs");
            builder.EntitySet<ExamClassGroupMap>("ExamClassGroupMaps");
            builder.EntitySet<EmployeeAttendance>("EmployeeAttendances");
            builder.EntitySet<OrgPaymentDetail>("OrgPaymentDetails");
            builder.EntitySet<SubjectComponent>("SubjectComponents");
            builder.EntitySet<EmpEmployeeSalaryComponent>("EmpEmployeeSalaryComponents");
            builder.EntitySet<EvaluationResultMark>("EvaluationResultMarks");
            //builder.EntitySet<SubjectComponent>("SubjectComponents");
            builder.EntitySet<StudentStatus>("StudentStatuses");

            return builder.GetEdmModel();
        }
    }
}
