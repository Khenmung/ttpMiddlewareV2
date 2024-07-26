using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Identity;
using ttpMiddleware.Data.Entities;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using ttpMiddleware.Common;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class EmpEmployeesController : ProtectedController
    {
        private IConfiguration _configuration;
        private readonly ttpauthContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmpEmployeesController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            ttpauthContext context)
        {
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }

        // GET: api/EmpEmployees
        [HttpGet]
        public IQueryable<EmpEmployee> GetEmpEmployees()
        {
            return _context.EmpEmployees.AsQueryable().AsNoTracking();
        }

        // GET: api/EmpEmployees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpEmployee>> GetEmpEmployee(int id)
        {
            var empEmployee = await _context.EmpEmployees.FindAsync(id);

            if (empEmployee == null)
            {
                return NotFound();
            }

            return empEmployee;
        }

        // PUT: api/EmpEmployees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpEmployee(int id, EmpEmployee empEmployee)
        {
            if (id != empEmployee.EmpEmployeeId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(empEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmpEmployee> empEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmpEmployees.FindAsync(key);
            var _existingemail = entity.EmailAddress;
            if (entity == null)
            {
                return NotFound();
            }
            var tran = _context.Database.BeginTransaction();
            empEmployee.Patch(entity);
            try
            {
                //await _context.SaveChangesAsync();
                var _empHistory = await _context.EmpEmployeeGradeSalHistories.Where(x => x.EmployeeId == entity.EmpEmployeeId
                && x.IsCurrent == 1 && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();
                if (_empHistory.Count > 0)
                {
                    var _empHistoryothers = await _context.EmpEmployeeGradeSalHistories.Where(x => x.EmployeeId == entity.EmpEmployeeId
                    && x.EmployeeGradeHistoryId != _empHistory[0].EmployeeGradeHistoryId
                    && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();

                    //making sure there is only one iscurrent=1 at any point of time.
                    foreach (var item in _empHistoryothers)
                    {
                        item.IsCurrent = 0;
                        _context.EmpEmployeeGradeSalHistories.Update(item);
                    }

                    _empHistory[0].EmpGradeId = (int)entity.EmpGradeId;
                    _empHistory[0].DepartmentId = (int)entity.DepartmentId;
                    _empHistory[0].WorkAccountId = entity.WorkAccountId;
                    _empHistory[0].DesignationId = entity.DesignationId;
                    _empHistory[0].IsCurrent = 1;
                    _context.EmpEmployeeGradeSalHistories.Update(_empHistory[0]);

                }
                else
                {
                    var emphistory = new EmpEmployeeGradeSalHistory();
                    emphistory.EmployeeGradeHistoryId = 0;
                    emphistory.EmpGradeId = (int)entity.EmpGradeId;
                    emphistory.DepartmentId = (int)entity.DepartmentId;
                    emphistory.WorkAccountId = entity.WorkAccountId;
                    emphistory.DesignationId = entity.DesignationId;
                    emphistory.EmployeeId = entity.EmpEmployeeId;
                    emphistory.OrgId = entity.OrgId;
                    emphistory.SubOrgId = (int)entity.SubOrgId;
                    emphistory.IsCurrent = 1;
                    emphistory.Active = 1;
                    emphistory.FromDate = DateTime.Now;
                    emphistory.ToDate = DateTime.Now;
                    _context.EmpEmployeeGradeSalHistories.Add(emphistory);
                    //await _context.SaveChangesAsync();
                }
                var _existingaccount = await _context.GeneralLedgers.Where(x => x.EmployeeId == entity.EmpEmployeeId 
                && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).FirstOrDefaultAsync();
                if (_existingaccount == null)
                {
                    var _employeeAccountNatureId = await _context.AccountNatures.Where(x => x.AccountName.ToLower() == "liability"
                    && x.OrgId == entity.OrgId)
                        .Select(s => s.AccountNatureId).FirstOrDefaultAsync();
                    
                    var _employeeAccountGroupId = await _context.AccountNatures.Where(x => x.SubOrgId== entity.SubOrgId 
                    && x.OrgId== entity.OrgId && x.AccountName.ToLower() == "current liability")
                        .Select(s => s.AccountNatureId).FirstOrDefaultAsync();
                    var _employeeLedger = new GeneralLedger()
                    {
                        GeneralLedgerName = entity.FirstName + " " + entity.LastName + "-" + entity.EmployeeCode,
                        EmployeeId = entity.EmpEmployeeId,
                        OrgId = entity.OrgId,
                        SubOrgId= (int)entity.SubOrgId,
                        Active = 1,
                        AccountNatureId = _employeeAccountNatureId,
                        AccountGroupId = _employeeAccountGroupId,
                        AccountSubGroupId = 0,
                        Deleted = false,
                        CreatedDate = DateTime.Now
                    };
                    _context.GeneralLedgers.Add(_employeeLedger);
                }
                var _attendance = await _context.EmployeeAttendances.Where(x => x.EmployeeId == entity.EmpEmployeeId).ToListAsync();
                foreach(var attendance in _attendance)
                {
                    attendance.DepartmentId = (int)entity.DepartmentId;
                    _context.Update(attendance);
                }
                if (entity.EmailAddress.Length > 0 && _existingemail != entity.EmailAddress)
                {
                    var userexist = await _userManager.FindByIdAsync(entity.UserId);
                    if (userexist != null)
                    {

                        userexist.Email = entity.EmailAddress;
                        await _userManager.UpdateAsync(userexist);
                    }
                    //        var common = new commonfunctions(_configuration, _userManager, _context);
                    //        var user = new UserRegistrationDto()
                    //        {
                    //            Email = entity.EmailAddress,
                    //            ContactNo = entity.ContactNo,
                    //            Username = entity.EmailAddress,
                    //            Password = entity.FirstName.Replace(" ", "").Substring(0, 2) + "@" + Convert.ToDateTime(entity.DOB).ToString("ddMMyyyy")
                    //        };

                    //        var _userId = await common.RegisterUser(user, (int)entity.OrgId, "employee");

                    //        entity.UserId = _userId;
                    //    }
                    //    else
                    //    {
                    //        throw new Exception("Email already in use.");
                    //    }
                    //    entity.UserId = userexist.Id;
                }
                await _context.SaveChangesAsync();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                if (!EmpEmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return Updated(entity);
        }
        // POST: api/EmpEmployees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpEmployee>> PostEmpEmployee([FromBody] JArray jsonWrapper)
        {
            using var tran = _context.Database.BeginTransaction();
            try
            {
                JToken jsonValues = jsonWrapper;
                EmpEmployee _employee = new EmpEmployee();

                foreach (var x in jsonValues)
                {
                    _employee = x.ToObject<EmpEmployee>();

                    if (_employee.EmpEmployeeId > 0)
                    {
                        var existingemployee = await _context.EmpEmployees.Where(x => x.EmpEmployeeId == _employee.EmpEmployeeId
                        && x.SubOrgId == _employee.SubOrgId && x.OrgId == _employee.OrgId).FirstOrDefaultAsync();

                        foreach (PropertyInfo prop in existingemployee.GetType().GetProperties())
                        {
                            if (prop.GetValue(_employee, null) != null)
                                prop.SetValue(existingemployee, prop.GetValue(_employee, null));
                        }

                        _context.EmpEmployees.Update(existingemployee);
                        //_context.SaveChanges();

                    }
                    else
                    {
                        _context.EmpEmployees.Add(_employee);
                                          
                    }
                    await _context.SaveChangesAsync();
                    var empgrade = new EmpEmployeeGradeSalHistory()
                    {
                        EmployeeId = _employee.EmpEmployeeId,
                        DepartmentId = (int)_employee.DepartmentId,
                        EmpGradeId = (int)_employee.EmpGradeId,
                        DesignationId = (int)_employee.DesignationId,
                        CTC = 0,
                        WorkAccountId = _employee.WorkAccountId,
                        FromDate = _employee.DOJ,
                        ToDate = DateTime.Now.AddYears(25),
                        Active = 1,
                        OrgId = _employee.OrgId,
                        SubOrgId = (int)_employee.SubOrgId,
                        IsCurrent = 1,
                        CreatedDate = DateTime.Now,
                        CreatedBy = _employee.CreatedBy
                    };
                    _context.EmpEmployeeGradeSalHistories.Add(empgrade);

                    var _employeeAccountNatureId = await _context.AccountNatures.Where(x => x.AccountName.ToLower() == "liability" 
                    && x.OrgId == 0 && x.Active == true)
                        .Select(s => s.AccountNatureId).FirstOrDefaultAsync();
                    var _employeeAccountGroupId = await _context.AccountNatures.Where(x =>
                                                    x.SubOrgId == _employee.SubOrgId 
                                                    && x.OrgId == _employee.OrgId && x.AccountName.ToLower() == "current liability").Select(s => s.AccountNatureId).FirstOrDefaultAsync();
                    var _employeeLedger = new GeneralLedger()
                    {
                        GeneralLedgerName = _employee.FirstName + " " + _employee.LastName + "-" + _employee.EmployeeCode,
                        EmployeeId = _employee.EmpEmployeeId,
                        OrgId = _employee.OrgId,
                        SubOrgId= (int)_employee.SubOrgId,
                        Active = 1,
                        AccountNatureId = _employeeAccountNatureId,
                        AccountGroupId = _employeeAccountGroupId,
                        AccountSubGroupId = 0,
                        Deleted = false,
                        CreatedDate = DateTime.Now
                    };
                    _context.GeneralLedgers.Add(_employeeLedger);
                    await _context.SaveChangesAsync();

                    //only if email is not empty, register user and assign role.
                    //if (_employee.EmailAddress.Length > 0)
                    //{
                    //    var common = new commonfunctions(_configuration, _userManager, _context);
                    //    var user = new UserRegistrationDto()
                    //    {
                    //        Email = _employee.EmailAddress,
                    //        ContactNo = _employee.ContactNo,
                    //        Username = _employee.EmailAddress,
                    //        Password = _employee.FirstName.Replace(" ", "").Substring(0, 2) + "@" + Convert.ToDateTime(_employee.DOB).ToString("ddMMyyyy")
                    //    };

                    //    var _userId = await common.RegisterUser(user, (int)_employee.OrgId, "employee");

                    //    _employee.UserId = _userId;
                    //}
                }

                await _context.SaveChangesAsync();
                tran.Commit();
                return Ok(_employee);

            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/EmpEmployees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpEmployee(int id)
        {
            var empEmployee = await _context.EmpEmployees.FindAsync(id);
            if (empEmployee == null)
            {
                return NotFound();
            }

            _context.EmpEmployees.Remove(empEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpEmployeeExists(int id)
        {
            return _context.EmpEmployees.Any(e => e.EmpEmployeeId == id);
        }
    }
}
