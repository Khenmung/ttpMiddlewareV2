using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using ttpMiddleware.Data.Entities;
using ttpMiddleware.Models;
using ttpMiddleware.Models.DTOs.Requests;
using System.IO;
using ttpMiddleware.Models.DTOs.Responses;

namespace ttpMiddleware.Common
{
    public class commonfunctions
    {
        private UserManager<ApplicationUser> _userManager;
        private ttpauthContext _context;
        IConfiguration _configuration;
        public commonfunctions(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            ttpauthContext context
            )
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> UpdateSubOrgId(ApplicationUser user)
        {
            try
            {

                //var _org = await _context.Organizations.Where(x => x.OrganizationId == org.OrganizationId && x.SubOrgId == 0).ToListAsync();
                //if (_org.Count > 0)
                //{
                
                var _company = await _context.MasterItems.Where(x =>
                x.MasterDataName.ToLower() == "primary" && x.OrgId == user.OrgId).FirstOrDefaultAsync();
                
                var _subOrgId = _company.MasterDataId;

                _company.SubOrgId = _subOrgId;
                _context.MasterItems.Update(_company);

                user.SubOrgId = _subOrgId;
                await _userManager.UpdateAsync(user);

                var _masterItems = await _context.MasterItems.Where(x => x.SubOrgId==0 && x.OrgId == user.OrgId).ToListAsync();
                foreach (var _masterItem in _masterItems)
                {
                    _masterItem.SubOrgId = _subOrgId;
                    _context.MasterItems.Update(_masterItem);
                }
                var _classmasters = await _context.ClassMasters.Where(x => x.SubOrgId == 0 && x.OrgId == user.OrgId).ToListAsync();
                foreach (var _classmaster in _classmasters)
                {
                    _classmaster.SubOrgId = _subOrgId;
                    _context.ClassMasters.Update(_classmaster);
                }
                var _feeDefinitions = await _context.FeeDefinitions.Where(x => x.SubOrgId == 0 && x.OrgId == user.OrgId).ToListAsync();
                foreach (var _feeDefinition in _feeDefinitions)
                {
                    _feeDefinition.SubOrgId = _subOrgId;
                    _context.FeeDefinitions.Update(_feeDefinition);
                }
                var _employees = await _context.EmpEmployees.Where(x => x.SubOrgId == 0 && x.OrgId == user.OrgId).ToListAsync();
                foreach (var _employee in _employees)
                {
                    _employee.SubOrgId = _subOrgId;
                    _context.EmpEmployees.Update(_employee);
                }
                var _batches = await _context.Batches.Where(x => x.SubOrgId == 0 && x.OrgId == user.OrgId).ToListAsync();
                foreach (var _batch in _batches)
                {
                    _batch.SubOrgId = _subOrgId;
                    _context.Batches.Update(_batch);
                }
                var _students = await _context.Students.Where(x => x.SubOrgId == 0 && x.OrgId == user.OrgId).ToListAsync();
                foreach (var _student in _students)
                {
                    _student.SubOrgId = _subOrgId;
                    _context.Students.Update(_student);
                }
                _context.SaveChanges();
                //}
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<string> RegisterUser(UserRegistrationDto user, int OrgId, string StudentOrEmployeeOrTeacher)
        {
            var errormessage = "";
            var userId = "";
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            //try
            //{
            if (existingUser != null)
            {
                throw new Exception("Email already in use");
            }
            else
            {
                var trialdays = Convert.ToDouble(_configuration.GetSection("ApplicationConfig").GetSection("trialdays").Value);
                var _userTypeParentId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig").GetSection("UserTypeParentId").Value);
                var _commonPanelId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig").GetSection("CommonAppId").Value);
                var _userTypeId = 0;
                var _studentOrEmployee = "";

                var _subOrgParentId = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == "company"
                                                    && x.ParentId == 0
                                                    && x.OrgId == 0
                                                    && x.ApplicationId == _commonPanelId
                                                    && x.Active == 1).Select(s => s.MasterDataId).FirstOrDefaultAsync();
                var _companySubOrgId = 0;
                var primaryExist = await _context.MasterItems.Where(x => x.ParentId == _subOrgParentId
                && x.OrgId == OrgId
                && x.MasterDataName == "Primary"
                && x.Active == 1).Select(x=>x.MasterDataId).FirstOrDefaultAsync();
                if (primaryExist==0)
                {
                    
                    var _company = new MasterItem()
                    {
                        MasterDataName = "Primary",
                        Active = 1,
                        Deleted = false,
                        ParentId = _subOrgParentId,
                        ApplicationId = _commonPanelId,
                        OrgId = (short)OrgId,
                        CreatedDate = DateTime.Now
                    };
                    _context.MasterItems.Add(_company);
                    _context.SaveChanges(true);
                    _companySubOrgId = _company.MasterDataId;
                }
                else
                {
                    _companySubOrgId = primaryExist;
                }
                //Usertype to masteritem
                var studentrole = await _context.MasterItems.Where(x =>
                x.MasterDataName == "Student"
                && x.OrgId == (short)OrgId
                && x.SubOrgId == _companySubOrgId
                && x.ParentId == _userTypeParentId)
                    .Select(s => s.MasterDataId).ToListAsync();

                var _masterStudent = new MasterItem();
                if (studentrole.Count == 0)
                {
                    _masterStudent.ParentId = _userTypeParentId;
                    _masterStudent.MasterDataName = "Student";
                    _masterStudent.OrgId = (short)OrgId;
                    _masterStudent.SubOrgId = _companySubOrgId;
                    _masterStudent.Active = 1;
                    _masterStudent.Deleted = false;
                    _masterStudent.ApplicationId = _commonPanelId;//common panel
                    _masterStudent.CreatedDate = DateTime.Now;
                    _context.MasterItems.Add(_masterStudent);
                    await _context.SaveChangesAsync();
                    _userTypeId = _masterStudent.MasterDataId;
                }
                else if (StudentOrEmployeeOrTeacher.ToLower() == "student")
                {
                    _studentOrEmployee = StudentOrEmployeeOrTeacher;
                    _userTypeId = studentrole[0];
                }
                var employeerole = await _context.MasterItems.Where(x =>
                x.MasterDataName == "Employee"
                && x.OrgId == (short)OrgId
                && x.SubOrgId == _companySubOrgId
                && x.ParentId == _userTypeParentId).Select(s => s.MasterDataId).ToListAsync();

                var _masterEmp = new MasterItem();
                if (employeerole.Count == 0)
                {
                    _masterEmp.ParentId = _userTypeParentId;
                    _masterEmp.MasterDataName = "Employee";
                    _masterEmp.OrgId = (short)OrgId;
                    _masterEmp.SubOrgId = _companySubOrgId;
                    _masterEmp.Active = 1;
                    _masterEmp.Deleted = false;
                    _masterEmp.ApplicationId = _commonPanelId;//common panel
                    _masterEmp.CreatedDate = DateTime.Now;
                    _context.MasterItems.Add(_masterEmp);
                    await _context.SaveChangesAsync();
                    _userTypeId = _masterEmp.MasterDataId;
                }
                else if (StudentOrEmployeeOrTeacher.ToLower() == "admin" || StudentOrEmployeeOrTeacher.ToLower() == "employee")
                {
                    _studentOrEmployee = "employee";
                    _userTypeId = employeerole[0];
                }

                //if (StudentOrEmployeeOrTeacher.ToLower() == "admin" || StudentOrEmployeeOrTeacher.ToLower()=="employee")
                //{
                //    _studentOrEmployee = "employee";
                //}
                //else
                //{
                //    _studentOrEmployee = StudentOrEmployeeOrTeacher;
                //}


                var RoleParentId = _context.MasterItems.Where(x => x.MasterDataName == "Role"
                && x.ParentId == 0).Select(s => s.MasterDataId).FirstOrDefault();

                var studentOrEmployeeRoleId = _context.MasterItems.Where(x =>
                    x.MasterDataName.ToLower() == StudentOrEmployeeOrTeacher.ToLower()
                    && x.ParentId == RoleParentId
                    && x.OrgId == OrgId
                    && x.SubOrgId == _companySubOrgId
                    ).Select(s => s.MasterDataId).FirstOrDefault();


                //var _userTypeId = _context.MasterItems.Where(x => x.MasterDataName.ToLower() == StudentOrEmployeeOrTeacher.ToLower()
                //    && x.ParentId == _userTypeParentId
                //    && x.OrgId == OrgId).Select(s => s.MasterDataId).FirstOrDefault();


                var newUser = new ApplicationUser()
                {
                    Email = user.Email,
                    UserName = user.Username.Replace(" ", ""),
                    OrgId = OrgId,
                    SubOrgId = _companySubOrgId,
                    Active = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now.AddDays(trialdays),
                    EmailConfirmed = true,
                    UserTypeId = _userTypeId
                };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {

                    if (studentOrEmployeeRoleId == 0)
                    {
                        var _master = new MasterItem();
                        _master.ParentId = RoleParentId;
                        _master.MasterDataName = StudentOrEmployeeOrTeacher;
                        _master.OrgId = (short)newUser.OrgId;
                        _master.SubOrgId = _companySubOrgId;
                        _master.Active = 1;
                        _master.Deleted = false;
                        _master.ApplicationId = _commonPanelId;//common panel
                        _context.MasterItems.Add(_master);
                        await _context.SaveChangesAsync();
                        studentOrEmployeeRoleId = _master.MasterDataId;
                    }
                    if (studentOrEmployeeRoleId > 0)
                    {
                        var roleuser = new RoleUser()
                        {
                            OrgId = (short)newUser.OrgId,
                            SubOrgId = _companySubOrgId,
                            RoleId = studentOrEmployeeRoleId,
                            UserId = newUser.Id,
                            Active = 1,
                        };
                        _context.RoleUsers.Add(roleuser);
                        _context.SaveChanges();

                        userId = newUser.Id;

                        //var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                        //File.AppendAllText(_configuration.GetSection("FilePath").GetSection("errorlogdev").Value, "before encoding: " + token);
                        //token = HttpUtility.UrlEncode(token);
                        //File.AppendAllText(_configuration.GetSection("FilePath").GetSection("errorlogdev").Value, "after encoding: " + token);
                        //var apiurl = _configuration.GetSection("EmailSettings").GetSection("siteUrl").Value + "/#/auth/confirmemail/" + newUser.Id + "/" + token;
                        //var emailstring = "Dear " + user.Username + ",<br><a href='" + apiurl + "'>Please click this link for email verification.</a>";

                        //var result = SendEmail(emailstring, "Email Verification", user.Email, "", OrgId);


                    }

                }
                else
                {

                    //transaction.Rollback();
                    foreach (var msg in isCreated.Errors)
                        errormessage += msg.Description + "<br>";
                    throw new Exception(errormessage);
                }
            }        //return errormessage;
            //}
            //catch (Exception ex)
            //{
            //    //var result= LogError("commonfunction-register", ex.InnerException.Message, user.UserId, OrgId);
            //    throw ex;
            //}

            return userId;
        }
        public void LogError(string moduleName, string detail, string userId, int orgId)
        {
            try
            {
                //var parentId = await _context.MasterItems.Where(x => x.OrgId == 0 && x.MasterDataName.ToLower() == "error status").ToListAsync();

                //var statusId = await _context.MasterItems
                //    .Where(x => x.OrgId == orgId && x.MasterDataName.ToLower() == "pending" && x.ParentId == parentId[0].MasterDataId).ToListAsync();
                ErrorLog log = new ErrorLog()
                {
                    Detail = detail,
                    CreatedDate = DateTime.Now,
                    CreatedBy = userId,
                    OrgId = (short)orgId,
                    ModuleName = moduleName,
                    StatusId = 0
                };
                _context.ErrorLogs.Add(log);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public async Task SendEmail([FromBody] string htmlString, string subject, string to, string userId, int orgId)
        {
            try
            {
                var fromEmail = _configuration.GetSection("EmailSettings").GetSection("emailFrom").Value;
                var emailpwd = _configuration.GetSection("EmailSettings").GetSection("emailPwd").Value;
                var emailhost = _configuration.GetSection("EmailSettings").GetSection("emailHost").Value;
                var emailportno = _configuration.GetSection("EmailSettings").GetSection("emailPortno").Value;

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromEmail);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = Convert.ToInt32(emailportno);// 587;
                smtp.Host = emailhost;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromEmail, emailpwd);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);
                //return "success";
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(_configuration.GetSection("FilePath").GetSection("errorlogdev").Value, ex.InnerException.Message + "\n");
                //return "error suppressed";
                //LogError("send email", ex.StackTrace, userId, orgId);
                //throw;
            }
        }
        public void AddToCustomFeature(CustomFeature customFeature, bool pConfidential, string tableName)
        {
            try
            {
                var existingCustomerFeature = _context.CustomFeatures.Where(x => x.TableName.ToLower() == tableName.ToLower()
                   && x.TableRowId == customFeature.TableRowId
                   && x.OrgId == customFeature.OrgId
                   && x.SubOrgId == customFeature.SubOrgId
                   ).Select(s => s).ToList();
                if (existingCustomerFeature.Count > 0)
                {
                    foreach (var item in existingCustomerFeature)
                    {
                        item.CustomFeatureName = customFeature.CustomFeatureName;
                        item.Active = !customFeature.Active ? false : pConfidential;
                        _context.CustomFeatures.Update(item);

                    }
                    _context.SaveChanges();
                }
                else if (pConfidential == true)
                {
                    var dupCustomerFeature = _context.CustomFeatures.Where(x => x.CustomFeatureName.ToLower() == customFeature.CustomFeatureName.ToLower()
                            && x.OrgId == customFeature.OrgId
                            && x.SubOrgId == customFeature.SubOrgId
                            //&& x.TableId == tableName
                            && x.ApplicationId == customFeature.ApplicationId).Select(s => s).ToList();
                    if (dupCustomerFeature.Count == 0)
                    {
                        var _customerFeature = new CustomFeature()
                        {
                            CustomFeatureId = 0,
                            CustomFeatureName = customFeature.CustomFeatureName,
                            TableName = tableName,
                            TableRowId = customFeature.TableRowId,
                            TableNameId = customFeature.TableNameId,
                            OrgId = customFeature.OrgId,
                            SubOrgId = customFeature.SubOrgId,
                            ApplicationId = customFeature.ApplicationId,
                            Active = customFeature.Active,
                            Deleted = false,
                            CreatedDate = DateTime.Now
                        };
                        _context.CustomFeatures.Add(_customerFeature);
                        _context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("duplicate custom feature found: " + customFeature.CustomFeatureName);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public async void CreateInvoice(int pMonth,int pOrgId,int pBatchId)
        //{
        //    var studclass = await _context.StudentClasses
        //        .Join(_context.SchoolFeeTypes, stud => stud.FeeTypeId, fee => fee.FeeTypeId, (stud, fee) => new { stud, fee })
        //        .Join(_context.ClassFees, a => a.stud.ClassId, cls => cls.ClassId, (a, cls) => new { a, cls })
        //        .Join(_context.FeeDefinitions, b => b.cls.FeeDefinitionId, defn => defn.FeeDefinitionId, (b, defn) => new { b, defn })
        //        .Where(x => x.b.a.stud.Active == 1
        //        && x.b.cls.Month == pMonth
        //        && x.b.a.stud.OrgId == pOrgId
        //        && x.b.a.stud.BatchId == pBatchId)
        //        .Select(x => new
        //        {
        //            SectionId = x.b.a.stud.SectionId,
        //            Month = x.b.cls.Month,
        //            RollNo = x.b.a.stud.RollNo,
        //            StudentClassId = x.b.a.stud.StudentClassId,
        //            Formula = x.b.a.fee.Formula,
        //            FeeName = x.defn.FeeName,
        //            Amount = x.b.cls.Amount
        //        }).ToListAsync();


        //}
    }
}
