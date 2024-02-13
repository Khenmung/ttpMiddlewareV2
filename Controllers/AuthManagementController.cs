using System;
using System.IO;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ttpMiddleware.Configuration;
using ttpMiddleware.Data.Entities;
using ttpMiddleware.Models.DTOs.Requests;
using ttpMiddleware.Models.DTOs.Responses;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Web;
using ttpMiddleware.Common;
using Microsoft.AspNet.OData.Builder;

namespace ttpMiddleware.Controllers
{
    [Route("api/[controller]")] // api/authManagement
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly TokenValidationParameters _tokenValidationParams;
        //private readonly AppDBContext _appDBContext;
        private readonly ttpauthContext _appDBContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthManagementController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParams,
            ttpauthContext apiDbContext,
            SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParams = tokenValidationParams;
            _appDBContext = apiDbContext;
            _signInManager = signInManager;
        }
        [HttpGet]
        [EnableQuery]
        //[Route("GetUsers")]
        public IQueryable<ApplicationUser> GetUser()
        {
            return _userManager.Users.AsQueryable();
        }
        [HttpPatch]
        [Route("{key}")]
        public async Task<ActionResult<ApplicationUser>> PatchMe(string key, [FromBody] Delta<ApplicationUser> user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = _userManager.Users.Where(a => a.Id == key).FirstOrDefault();
            if (entity == null)
            {
                return NotFound();
            }
            user.Patch(entity);
            try
            {
                IdentityResult result = await _userManager.UpdateAsync(entity);


            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

            return Ok(entity);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword changepwd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                                "Invalid field value"
                            },
                    Success = false
                });
            }
            var user = await _userManager.FindByIdAsync(changepwd.UserId);
            var result = await _userManager.ChangePasswordAsync(user, changepwd.OldPassword, changepwd.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                                result.Errors.ToList()[0].Code
            },
                    Success = false
                });

            }
            else
            {
                return Ok();
            }
            //await _signInManager.RefreshSignInAsync(user);
        }
        [HttpPost]
        [Route("SendSMS")]
        public string SendSMS()
        {
            var sms = new SendSMS();
            return sms.send();
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                                "ForgotPasswordConfirmation"
                            },
                    Success = false
                });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                                "ForgotPasswordConfirmation"
                            },
                    Success = false
                });
            }
            try
            {

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = HttpUtility.UrlEncode(code);
                var callbackUrl = _configuration.GetSection("EmailSettings").GetSection("siteUrl").Value + "/#/auth/reset?code=" + code + "&userid=" + user.Id;
                var common = new commonfunctions(_configuration, _userManager, _appDBContext);
                var emailbody = "Dear " + user.UserName + ",<br> <a href='" + callbackUrl + "'>Please reset your password by clicking here.</a>";
                await common.SendEmail(emailbody, "Reset Password", user.Email, user.Id, user.OrgId);

                return Ok();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(_configuration.GetSection("FilePath").GetSection("errorlogprod").Value, ex.InnerException.Message);
                throw;
            }

        }
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByIdAsync(resetPassword.UserId);
            try
            {


                if (user != null)
                {
                    var token = resetPassword.Code.Replace(" ", "+");
                    var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
                    if (result.Succeeded)
                        return Ok();
                    else
                    {
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                                "Reset Password error"
                            },
                            Success = false
                        });
                    }
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "ResetPassworderror"
                            },
                        Success = false
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                                ex.Message
                            },
                    Success = false
                }); ;
            }
        }
        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmail confirmEmail)
        {
            if (confirmEmail.userId == null || confirmEmail.code == null)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                                "empty data"
                            },
                    Success = false
                });
            }
            var user = await _userManager.FindByIdAsync(confirmEmail.userId);
            //var _code = confirmEmail.code.Replace(' ','+');
            System.IO.File.AppendAllText(_configuration.GetSection("FilePath").GetSection("errorlogprod").Value, "confirming: " + confirmEmail.code);
            var result = await _userManager.ConfirmEmailAsync(user, confirmEmail.code);

            if (result.Succeeded)
            {
                var _roleUser = await _appDBContext.RoleUsers
                    .Join(_appDBContext.MasterItems,
                    r => r.RoleId,
                    m => m.MasterDataId,
                    (r, m) => new { r, m.MasterDataName })
                    .Where(x => x.r.UserId == user.Id).FirstOrDefaultAsync();
                return Ok(_roleUser);
            }
            else
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                                result.Errors.ToArray().ToString()
                            },
                    Success = false
                });
            }

        }
        [HttpPost]
        public async Task<IActionResult> SyncData([FromBody] JObject data)
        {
            JToken jsonValues = data;
            //List<Student> _StudentList = new List<Student>();
            //var _data;
            foreach (JProperty x in jsonValues)
            {
                //builder.EntitySet<MasterItem>("MasterItems");
                if (x.Name == "MasterItem")
                {

                }
                else if (x.Name == "Page")
                {
                    List<Page> _data = x.Value.ToObject<List<Page>>();
                    foreach (var item in _data)
                    {
                        var _localdata = await _appDBContext.Pages.Where(x => x.PageId == item.PageId).FirstOrDefaultAsync();
                        if (_localdata != null)
                        {
                            _appDBContext.Update(item);
                        }
                        else
                        {
                            _appDBContext.Add(item);
                        }
                    }
                }
            }

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Page>("Pages");
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

            builder.EntitySet<AttendanceReport>("AttendanceReports");
            builder.EntitySet<LeavePolicy>("LeavePolicies");

            builder.EntitySet<ReportConfigItem>("ReportConfigItems");
            builder.EntitySet<ReportOrgReportName>("ReportOrgReportNames");
            builder.EntitySet<ReportOrgReportColumn>("ReportOrgReportColumns");

            //builder.EntitySet<ApplicationPrice>("ApplicationPrices");
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
            builder.EntitySet<EmployeeGroupActivityParticipant>("EmployeeGroupActivityParticipants");
            //builder.EntitySet<EmployeeGeneratedCertificate>("EmployeeGeneratedCertificates");
            builder.EntitySet<ExamMarkConfig>("ExamMarkConfigs");
            builder.EntitySet<ExamClassGroupMap>("ExamClassGroupMaps");
            builder.EntitySet<EmployeeAttendance>("EmployeeAttendances");
            builder.EntitySet<OrgPaymentDetail>("OrgPaymentDetails");
            builder.EntitySet<SubjectComponent>("SubjectComponents");
            builder.EntitySet<EmpEmployeeSalaryComponent>("EmpEmployeeSalaryComponents");
            return NoContent();
        }
        [HttpPost]
        [Route("EmailDuplicateCheck")]
        public async Task<IActionResult> EmailDuplicateCheck([FromBody] CheckEmailDuplicate user)
        {
            var duplicate = false;
            if (user.Id > 0)
            {
                var existingstudent = await _appDBContext.Students.Where(x => x.EmailAddress == user.Email && x.StudentId != user.Id).ToListAsync(); //_userManager.FindByEmailAsync(user.Email);
                if (existingstudent.Count() == 0)
                {
                    var existingemp = await _appDBContext.EmpEmployees.Where(x => x.EmailAddress == user.Email && x.EmpEmployeeId != user.Id).ToListAsync();
                    if (existingemp.Count() > 0)
                        duplicate = true;

                }
                else
                    duplicate = true;
            }
            else
            {
                var existingstudent = await _appDBContext.Students.Where(x => x.EmailAddress == user.Email).ToListAsync(); //_userManager.FindByEmailAsync(user.Email);
                if (existingstudent.Count() == 0)
                {
                    var existingemp = await _appDBContext.EmpEmployees.Where(x => x.EmailAddress == user.Email).ToListAsync();
                    if (existingemp.Count() > 0)
                        duplicate = true;
                }
                else
                    duplicate = true;
            }
            return Ok(duplicate);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                                "Invalid model."
                            },
                    Success = false
                });
            }
            var tran = _appDBContext.Database.BeginTransaction();
            try
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByIdAsync(user.Id);

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                                "User not found"
                            },
                        Success = false
                    });
                }
                else
                {
                    //var _userType = _appDBContext.MasterItems
                    existingUser.Email = user.Email;
                    existingUser.ValidTo = user.ValidTo;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Active = user.Active;
                    await _userManager.UpdateAsync(existingUser);

                    var _student = await _appDBContext.Students.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                    if (_student != null)
                    {
                        _student.EmailAddress = user.Email;
                        _appDBContext.Students.Update(_student);

                    }
                    else
                    {
                        var _employee = await _appDBContext.EmpEmployees.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                        if (_employee != null)
                        {
                            _employee.EmailAddress = user.Email;
                            _appDBContext.EmpEmployees.Update(_employee);
                        }
                    }
                    await _appDBContext.SaveChangesAsync();
                    tran.Commit();
                }

                return Ok();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw;
            }

        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // We can utilise the model
                    var existingUser = await _userManager.FindByEmailAsync(user.Email);

                    if (existingUser != null)
                    {
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                                "Email already in use"
                            },
                            Success = false
                        });
                    }
                    using var transaction = _appDBContext.Database.BeginTransaction();
                    try
                    {

                        var orgobj = _appDBContext.Organizations.Where(a => a.OrganizationName.ToLower() == user.OrganizationName.ToLower())
                            .Select(a => a).FirstOrDefault();
                        var trialdays = Convert.ToDouble(_configuration.GetSection("ApplicationConfig").GetSection("trialdays").Value);
                        if (orgobj == null)
                        {
                            orgobj = new Organization()
                            {
                                OrganizationName = user.OrganizationName,
                                Contact = user.ContactNo,
                                ValidFrom = DateTime.Now,
                                ValidTo = DateTime.Now.AddDays(trialdays),
                                CreatedDate = DateTime.Now,
                                Active = 1
                            };
                            _appDBContext.Organizations.Add(orgobj);
                            //var _master = new MasterItem();
                            //_master.ParentId
                            _appDBContext.SaveChanges();
                        }
                        else if (user.RoleName.ToLower() == "admin" && orgobj != null)
                        {
                            return BadRequest(new RegistrationResponse()
                            {
                                Errors = new List<string>() {
                                        "Organization name already exists"
                                        },
                                Success = false
                            });
                            //return BadRequest("Organization name already exists!");
                        }
                        var existingUserId = user.UserId;
                        var common = new commonfunctions(_configuration, _userManager, _appDBContext);

                        var message = await common.RegisterUser(user, (int)orgobj.OrganizationId, user.RoleName);
                        user.UserId = message;
                        if (existingUserId == null && user.RoleName.ToLower() == "admin")
                        {
                            var _commonPanelId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig").GetSection("CommonAppId").Value);
                            var _EmployeeAppId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig").GetSection("EmployeeAppId").Value);
                            var _EduAppId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig").GetSection("EduAppId").Value);

                            var _subOrgParentId = await _appDBContext.MasterItems.Where(x => x.MasterDataName.ToLower() == "company"
                                                    && x.ParentId == 0
                                                    && x.OrgId == 0
                                                    && x.ApplicationId == _commonPanelId
                                                    && x.Active == 1).Select(s => s.MasterDataId).FirstOrDefaultAsync();
                            //var _company = new MasterItem()
                            //{
                            //    MasterDataName = "Primary",
                            //    Active = 1,
                            //    Deleted = false,
                            //    ParentId = (short)_subOrgParentId,
                            //    ApplicationId = _commonPanelId,
                            //    OrgId = orgobj.OrganizationId,
                            //    CreatedDate = DateTime.Now
                            //};
                            //_appDBContext.MasterItems.Add(_company);
                            //_appDBContext.SaveChanges(true);
                            var _company = await _appDBContext.MasterItems.Where(x => x.ParentId == _subOrgParentId
                                                                                && x.OrgId == orgobj.OrganizationId).FirstOrDefaultAsync();
                            _company.SubOrgId = _company.MasterDataId;
                            _appDBContext.MasterItems.Update(_company);

                            orgobj.SubOrgId = _company.MasterDataId;
                            _appDBContext.Organizations.Update(orgobj);

                            var employee = new EmpEmployee();
                            employee.FirstName = user.Username;
                            employee.ContactNo = user.ContactNo;
                            employee.UserId = user.UserId;
                            employee.EmailAddress = user.Email;
                            employee.FatherName = "to be updated";
                            employee.DOB = new DateTime(1990, 01, 01);
                            employee.DOJ = DateTime.Now;
                            employee.OrgId = orgobj.OrganizationId;
                            employee.SubOrgId = _company.MasterDataId;
                            employee.Active = 1;
                            _appDBContext.EmpEmployees.Add(employee);

                            var _desigParentId = await _appDBContext.MasterItems.Where(x => x.MasterDataName.ToLower() == "designation"
                                                    && x.ParentId == 0
                                                    && x.OrgId == 0
                                                    && x.ApplicationId == _EmployeeAppId
                                                    && x.Active == 1).Select(s => s.MasterDataId).FirstOrDefaultAsync();
                            var _designation = new MasterItem()
                            {
                                MasterDataName = "Admin Designation",
                                Active = 1,
                                Deleted = false,
                                ParentId = _desigParentId,
                                ApplicationId = _EmployeeAppId,
                                OrgId = orgobj.OrganizationId,
                                SubOrgId = _company.MasterDataId,
                                CreatedDate = DateTime.Now
                            };
                            _appDBContext.MasterItems.Add(_designation);

                            var _gradeParentId = await _appDBContext.MasterItems.Where(x => x.MasterDataName.ToLower() == "employee grade"
                                                    && x.ParentId == 0
                                                    && x.OrgId == 0
                                                    && x.ApplicationId == _EmployeeAppId
                                                    && x.Active == 1).Select(s => s.MasterDataId).FirstOrDefaultAsync();

                            var _grade = new MasterItem()
                            {
                                MasterDataName = "Admin Grade",
                                Active = 1,
                                Deleted = false,
                                ParentId = _gradeParentId,
                                ApplicationId = _EmployeeAppId,
                                OrgId = orgobj.OrganizationId,
                                SubOrgId = _company.MasterDataId,
                                CreatedDate = DateTime.Now
                            };
                            _appDBContext.MasterItems.Add(_grade);

                            var _departmentParentId = await _appDBContext.MasterItems.Where(x => x.MasterDataName.ToLower() == "department"
                                                    && x.ParentId == 0
                                                    && x.OrgId == 0
                                                    && x.ApplicationId == _commonPanelId
                                                    && x.Active == 1).Select(s => s.MasterDataId).FirstOrDefaultAsync();

                            var _department = new MasterItem()
                            {
                                MasterDataName = "Admin Department",
                                Active = 1,
                                Deleted = false,
                                ParentId = _departmentParentId,
                                ApplicationId = _commonPanelId,
                                SubOrgId = _company.MasterDataId,
                                OrgId = orgobj.OrganizationId,
                                CreatedDate = DateTime.Now

                            };
                            _appDBContext.MasterItems.Add(_department);

                            _appDBContext.SaveChanges();


                            var employmenthistory = new EmpEmployeeGradeSalHistory()
                            {
                                ManagerId = 0,
                                ReportingTo = 0,
                                DesignationId = _designation.MasterDataId,
                                DepartmentId = _department.MasterDataId,
                                EmpGradeId = _grade.MasterDataId,
                                EmployeeId = employee.EmpEmployeeId,
                                FromDate = DateTime.Now,
                                ToDate = DateTime.Now.AddMonths(12),
                                OrgId = orgobj.OrganizationId,
                                SubOrgId = _company.MasterDataId,
                                Active = 1,
                                IsCurrent = 1,
                                CreatedDate = DateTime.Now,
                                Deleted = false
                            };
                            _appDBContext.EmpEmployeeGradeSalHistories.Add(employmenthistory);
                            _appDBContext.SaveChanges();

                        }
                        else if (user.RoleName.ToLower() == "student")
                        {
                            var _student = await _appDBContext.Students.Where(x => x.EmailAddress == user.Email).FirstOrDefaultAsync();
                            if (_student != null)
                            {
                                _student.UserId = user.UserId;
                                _appDBContext.Students.Update(_student);
                            }
                        }
                        else if (user.RoleName.ToLower() == "employee")
                        {
                            var _employee = await _appDBContext.EmpEmployees.Where(x => x.EmailAddress == user.Email).FirstOrDefaultAsync();
                            if (_employee != null)
                            {
                                _employee.UserId = user.UserId;
                                _appDBContext.EmpEmployees.Update(_employee);
                            }
                        }
                        await _appDBContext.SaveChangesAsync();
                        transaction.Commit();
                        return Ok(user);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                        ex.Message
                        },
                            Success = false
                        });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        ex.Message
                    },
                        Success = false
                    });
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _userManager.FindByEmailAsync(user.Email);

                    if (existingUser == null)
                    {
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                                "Invalid login request"
                            },
                            Success = false
                        });
                    }
                    //else if (Convert.ToDateTime(existingUser.ValidTo).Date < DateTime.UtcNow.Date)
                    //{
                    //    return BadRequest(new RegistrationResponse()
                    //    {
                    //        Errors = new List<string>() {
                    //            "User validity expired."
                    //        },
                    //        Success = false
                    //    });
                    //}
                    else if (existingUser.Active == 0)
                    {
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                                "User is not active."
                            },
                            Success = false
                        });
                    }
                    else if (existingUser.EmailConfirmed == false)
                    {
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                                "Email confirmation pending"
                            },
                            Success = false
                        });
                    }
                    else
                    {
                        bool valid = false;
                        var OrgId = existingUser.OrgId;
                        var org = await _appDBContext.Organizations.Where(x => x.OrganizationId == existingUser.OrgId && x.Active == 1).ToListAsync();

                        for (var count = 0; count < org.Count; count++)
                        {
                            if (Convert.ToDateTime(org[count].ValidTo).Date >= DateTime.UtcNow.Date)
                            {
                                valid = true;
                                break;
                            }
                        }

                        if (!valid)
                        {
                            return BadRequest(new RegistrationResponse()
                            {
                                Errors = new List<string>() {
                                "User validity expired."
                            },
                                Success = false
                            });
                        }
                    }

                    var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                    if (!isCorrect)
                    {
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                                "Invalid login Id or password."
                            },
                            Success = false
                        });
                    }

                    if (existingUser.SubOrgId == 0)
                    {
                        var common = new commonfunctions(_configuration, _userManager, _appDBContext);
                        var message = await common.UpdateSubOrgId(existingUser);
                        if (!message)
                            return BadRequest("Updating SubOrgId error.");
                    }

                    var jwtToken = await GenerateJwtToken(existingUser);
                    return Ok(jwtToken);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await VerifyAndGenerateToken(tokenRequest);

                if (result == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                            "Invalid tokens"
                        },
                        Success = false
                    });
                }

                return Ok(result);
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Success = false
            });
        }
        [HttpPost]
        [Route("getInvoiceSingle")]
        public async Task<IActionResult> GetInvoiceSingle(OrganizationAndBatch orgAndBatch)
        {
            //StudentName, FeeName, Month *, MonthName, ClassName, SectionName, Amount
            var studclass = await _appDBContext.StudentClasses
                .Join(_appDBContext.SchoolFeeTypes, stud => stud.FeeTypeId, fee => fee.FeeTypeId, (stud, fee) => new { stud, fee })
                .Join(_appDBContext.ClassFees, a => a.stud.ClassId, cls => cls.ClassId, (a, cls) => new { a, cls })
                .Join(_appDBContext.FeeDefinitions, b => b.cls.FeeDefinitionId, defn => defn.FeeDefinitionId, (b, defn) => new { b, defn })
                .Where(x => x.b.a.stud.Active == 1
                && x.b.a.stud.StudentClassId == orgAndBatch.StudentClassId
                && x.b.cls.Active == 1
                && x.defn.Active == 1
                && x.b.a.fee.Active == 1
                && x.b.a.stud.OrgId == orgAndBatch.OrgId
                && x.b.a.stud.SubOrgId == orgAndBatch.SubOrgId
                && x.b.a.stud.BatchId == orgAndBatch.BatchId
                && x.b.cls.Month > 0)
                .Select(x => new
                {
                    SectionId = x.b.a.stud.SectionId,
                    Month = x.b.cls.Month,
                    RollNo = x.b.a.stud.RollNo,
                    StudentClassId = x.b.a.stud.StudentClassId,
                    Formula = x.b.a.fee.Formula,
                    FeeName = x.defn.FeeName,
                    Amount = x.b.cls.Amount
                })
                .ToListAsync();

            return Ok(studclass);
        }
        [HttpPost]
        [Route("getinvoice")]
        public async Task<IActionResult> GetInvoice(OrganizationAndBatch orgAndBatch)
        {
            //StudentName, FeeName, Month *, MonthName, ClassName, SectionName, Amount
            var studclass = await _appDBContext.StudentClasses
                .Join(_appDBContext.SchoolFeeTypes, studcls => studcls.FeeTypeId, fee => fee.FeeTypeId, (studcls, fee) => new { studcls, fee })
                .Join(_appDBContext.ClassFees, a => a.studcls.ClassId, cls => cls.ClassId, (a, cls) => new { a, cls })
                .Join(_appDBContext.FeeDefinitions, b => b.cls.FeeDefinitionId, defn => defn.FeeDefinitionId, (b, defn) => new { b, defn })
                .Where(x => x.b.a.studcls.Active == 1
                //&& x.b.cls.Month == orgAndBatch.Month
                && x.b.cls.Active == 1
                && x.defn.Active == 1
                && x.b.a.fee.Active == 1
                && x.b.a.studcls.OrgId == orgAndBatch.OrgId
                && x.b.a.studcls.SubOrgId == orgAndBatch.SubOrgId
                && x.b.a.studcls.BatchId == orgAndBatch.BatchId
                && x.b.a.studcls.SemesterId == orgAndBatch.SemesterId
                && x.b.a.studcls.SectionId == orgAndBatch.SectionId
                && x.b.a.studcls.StudentClassId == orgAndBatch.StudentClassId
                && x.b.cls.Month > 0)
                .Select(x => new
                {
                    SemesterId = x.b.a.studcls.SemesterId,
                    SectionId = x.b.a.studcls.SectionId,
                    Month = x.b.cls.Month,
                    RollNo = x.b.a.studcls.RollNo,
                    StudentClassId = x.b.a.studcls.StudentClassId,
                    Formula = x.b.a.fee.Formula,
                    FeeName = x.defn.FeeName,
                    Amount = x.b.cls.Amount
                })
                .ToListAsync();

            return Ok(studclass);
        }
        [HttpPost]
        [Route("createinvoice")]
        public async Task<IActionResult> createInvoice([FromBody] JArray invoices)
        {
            JToken jsonValues = invoices;
            AccountingLedgerTrialBalance _invoice = new AccountingLedgerTrialBalance();
            try
            {


                foreach (var x in jsonValues)
                {

                    _invoice = x.ToObject<AccountingLedgerTrialBalance>();
                    //if (_invoice.Month == 202205)
                    //{
                    //    Console.WriteLine("hi");
                    //}
                    var today = Convert.ToInt32(DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0'));
                    var existing = await _appDBContext.AccountingLedgerTrialBalances.Where(x => x.MonthDisplay == _invoice.MonthDisplay
                                && x.StudentClassId == _invoice.StudentClassId
                                && x.OrgId == _invoice.OrgId
                                && x.SubOrgId == _invoice.SubOrgId
                                && x.Active == 1).FirstOrDefaultAsync();
                    if (existing == null)
                    {
                        _invoice.CreatedDate = DateTime.Now;
                        _appDBContext.AccountingLedgerTrialBalances.Add(_invoice);
                    }
                    // only if any amount is not yet paid. TotalCredit == 0.
                    // Balance ==0 but not paid means free student fee
                    //fees can be updated only for current future months.
                    else if (existing.TotalCredit == 0 && (existing.Balance > 0 || _invoice.Month >= today))
                    {
                        existing.ClassId = _invoice.ClassId;
                        existing.SectionId = _invoice.SectionId;
                        existing.SemesterId = _invoice.SemesterId;
                        existing.BaseAmount = _invoice.BaseAmount;
                        existing.TotalDebit = _invoice.TotalDebit;
                        existing.Balance = _invoice.Balance;
                        existing.UpdatedDate = DateTime.Now;

                        _appDBContext.AccountingLedgerTrialBalances.Update(existing);
                    }
                }
                _appDBContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        private async Task<AuthResult> GenerateJwtToken(ApplicationUser user)
        {
            try
            {


                var jwtTokenHandler = new JwtSecurityTokenHandler();
                //var _roleObj = await _appDBContext.RoleUsers.Join(_appDBContext.MasterItems, r => r.RoleId, m => m.MasterDataId, (r, m) => new { r, m })
                //    .Where(x => x.r.UserId == user.Id).ToListAsync();
                var _roleObj = await _appDBContext.RoleUsers.Where(x => x.UserId == user.Id).ToListAsync();
                var _role = "";
                foreach (var item in _roleObj)
                {
                    var obj = await _appDBContext.MasterItems.Where(x => x.MasterDataId == item.RoleId).Select(s => s.MasterDataName).FirstOrDefaultAsync();
                    if (obj != null)
                        _role = obj.ToLower();
                }
                //var employeeId = ;
                //var studentId = Nullable();
                //if (_role == "employee")
                int _subOrgId = 0;
                var employee = await _appDBContext.EmpEmployees.Where(x => x.UserId == user.Id)
                    .Select(s => new { EmployeeId = s.EmpEmployeeId, SubOrgId = s.SubOrgId }).FirstOrDefaultAsync();
                var _employeeId = 0;
                var _studentId = 0;
                var student = await _appDBContext.Students.Where(x => x.UserId == user.Id).Select(s => new { StudentId = s.StudentId, SubOrgId = s.SubOrgId }).FirstOrDefaultAsync();
                if (employee != null)
                {
                    _employeeId = employee.EmployeeId;
                    _subOrgId = user.SubOrgId;//  (int)(employee.SubOrgId == null ? 0 : employee.SubOrgId);
                }
                else
                {
                    _subOrgId = user.SubOrgId;
                    _studentId = student.StudentId;
                }


                var PlanId = await _appDBContext.CustomerPlans.Where(x => x.SubOrgId == user.SubOrgId && x.OrgId == user.OrgId && x.Active == 1)
                    .Select(s => new { CustomerPlanId = s.CustomerPlanId, PlanId = s.PlanId }).FirstOrDefaultAsync();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
                int _customerPlanId = 0, _planId = 0;
                if (PlanId != null)
                {
                    _customerPlanId = PlanId.CustomerPlanId;
                    _planId = PlanId.PlanId;
                }
                //var _storageFnP = await _appDBContext.StorageFnPs.Where(x => x.OrgId == user.OrgId && x.Description.ToLower() == "organization logo").ToListAsync();
                //var _logoPath = "";
                //var _orgName = await _appDBContext.Organizations.Where(x => x.OrganizationId == user.OrgId).ToListAsync();
                //if (_storageFnP.Count > 0 && _orgName.Count > 0)
                //{
                //    _logoPath = "/uploads/" + _orgName[0].OrganizationName + "/organization logo/" + _storageFnP[0].UpdatedFileFolderName;
                //}
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("orgId", user.OrgId.ToString()),
                    new Claim("subOrgId", _subOrgId.ToString()),
                    new Claim("planId", _planId.ToString()),
                    new Claim("customerPlanId", _customerPlanId.ToString()),
                    new Claim("employeeId", _employeeId.ToString()),
                    new Claim("studentId", _studentId.ToString()),
                    new Claim("role", _role),
                    //new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Millisecond.ToString()),
                    //new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddDays(1).Millisecond.ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(10), // 5-10 
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = jwtTokenHandler.WriteToken(token);

                var refreshToken = new RefreshToken()
                {
                    JwtId = token.Id,
                    IsUsed = false,
                    IsRevoked = false,
                    UserId = user.Id,
                    AddedDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddMinutes(30),
                    Token = RandomString(35) + Guid.NewGuid()
                };

                await _appDBContext.RefreshTokens.AddAsync(refreshToken);
                await _appDBContext.SaveChangesAsync();

                return new AuthResult()
                {
                    Token = jwtToken,
                    Success = true,
                    RefreshToken = refreshToken.Token
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validation 1 - Validation JWT token format
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out var validatedToken);

                // Validation 2 - Validate encryption alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Validation 3 - validate expiry date
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has not yet expired"
                        }
                    };
                }

                // validation 4 - validate existence of the token
                var storedToken = await _appDBContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                if (storedToken == null)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token does not exist"
                        }
                    };
                }

                // Validation 5 - validate if used
                if (storedToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has been used"
                        }
                    };
                }

                // Validation 6 - validate if revoked
                if (storedToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has been revoked"
                        }
                    };
                }

                // Validation 7 - validate the id
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token doesn't match"
                        }
                    };
                }

                // update current token 

                storedToken.IsUsed = true;
                _appDBContext.RefreshTokens.Update(storedToken);
                await _appDBContext.SaveChangesAsync();

                // Generate a new token
                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
                return await GenerateJwtToken(dbUser);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Lifetime validation failed. The token is expired."))
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has expired please re-login"
                        }
                    };

                }
                else
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Something went wrong."
                        }
                    };
                }
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

    }
}