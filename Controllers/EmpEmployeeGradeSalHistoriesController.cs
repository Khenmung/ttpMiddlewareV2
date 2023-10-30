using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNet.OData.Routing;
using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class EmpEmployeeGradeSalHistoriesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmpEmployeeGradeSalHistoriesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmpEmployeeGradeSalHistories
        [HttpGet]
        public IQueryable<EmpEmployeeGradeSalHistory> GetEmpEmployeeGradeSalHistories()
        {
            return _context.EmpEmployeeGradeSalHistories.AsQueryable().AsNoTracking();
        }

        // GET: api/EmpEmployeeGradeSalHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpEmployeeGradeSalHistory>> GetEmpEmployeeGradeSalHistory(short id)
        {
            var empEmployeeGradeSalHistory = await _context.EmpEmployeeGradeSalHistories.FindAsync(id);

            if (empEmployeeGradeSalHistory == null)
            {
                return NotFound();
            }

            return empEmployeeGradeSalHistory;
        }
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<EmpEmployeeGradeSalHistory> empEmployeeGradeSalHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var entity = await _context.EmpEmployeeGradeSalHistories.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            var _existingDepartmentId = entity.DepartmentId;
            empEmployeeGradeSalHistory.Patch(entity);

            var tran = _context.Database.BeginTransaction();
            try
            {
                //compare old and new departmentid
                //if departmentid is changed, update salarycomponents table as well
                if (_existingDepartmentId != entity.DepartmentId)
                {
                    var salaryComponents = await _context.EmpEmployeeSalaryComponents.Where(x => x.EmployeeId == entity.EmployeeId
                    && x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId).ToListAsync();
                    
                    foreach (var component in salaryComponents)
                    {
                        component.DepartmentId = entity.DepartmentId;
                        _context.Update(component);
                    }
                }
                if (entity.IsCurrent == 1)
                {
                    var others = await _context.EmpEmployeeGradeSalHistories.Where(x =>
                    x.EmployeeGradeHistoryId != entity.EmployeeGradeHistoryId
                    && x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId
                    && x.EmployeeId == entity.EmployeeId).ToListAsync();
                    foreach (var item in others)
                    {
                        item.IsCurrent = 0;
                        _context.EmpEmployeeGradeSalHistories.Update(item);
                    }
                }
                else
                {
                    var others = await _context.EmpEmployeeGradeSalHistories.Where(x =>
                    x.EmployeeGradeHistoryId != entity.EmployeeGradeHistoryId
                    && x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId
                    && x.EmployeeId == entity.EmployeeId
                    && x.IsCurrent == 1 && x.Active == 1).ToListAsync();
                    if (others.Count() == 0)
                    {
                        throw new Exception("There must be atleast one row current.");
                    }
                }
                tran.Commit();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                tran.Rollback();
                if (!EmpEmployeeGradeSalHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(entity);
        }
        // PUT: api/EmpEmployeeGradeSalHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpEmployeeGradeSalHistory(short id, EmpEmployeeGradeSalHistory empEmployeeGradeSalHistory)
        {
            if (id != empEmployeeGradeSalHistory.EmployeeGradeHistoryId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(empEmployeeGradeSalHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeGradeSalHistoryExists(id))
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

        // POST: api/EmpEmployeeGradeSalHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpEmployeeGradeSalHistory>> PostEmpEmployeeGradeSalHistory([FromBody] EmpEmployeeGradeSalHistory empEmployeeGradeSalHistory)
        {

            var tran = _context.Database.BeginTransaction();
            try
            {
                var _allothers = await _context.EmpEmployeeGradeSalHistories.Where(x => x.EmployeeId == empEmployeeGradeSalHistory.EmployeeId
                && x.OrgId == empEmployeeGradeSalHistory.OrgId
                && x.SubOrgId == empEmployeeGradeSalHistory.SubOrgId
                ).ToListAsync();
                if (_allothers != null)
                {
                    foreach (var item in _allothers)
                    {
                        item.IsCurrent = 0;
                        _context.EmpEmployeeGradeSalHistories.Update(item);
                    }
                }
                _context.EmpEmployeeGradeSalHistories.Add(empEmployeeGradeSalHistory);
                var _emp = await _context.EmpEmployees.Where(x => x.EmpEmployeeId == empEmployeeGradeSalHistory.EmployeeId).FirstOrDefaultAsync();
                if (_emp != null)
                {
                    _emp.ManagerId = (int)empEmployeeGradeSalHistory.ManagerId;
                    _context.Update(_emp);
                }
                    
                await _context.SaveChangesAsync();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw;

            }
            return Ok(empEmployeeGradeSalHistory);
        }

        // DELETE: api/EmpEmployeeGradeSalHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpEmployeeGradeSalHistory(short id)
        {
            var empEmployeeGradeSalHistory = await _context.EmpEmployeeGradeSalHistories.FindAsync(id);
            if (empEmployeeGradeSalHistory == null)
            {
                return NotFound();
            }

            _context.EmpEmployeeGradeSalHistories.Remove(empEmployeeGradeSalHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpEmployeeGradeSalHistoryExists(short id)
        {
            return _context.EmpEmployeeGradeSalHistories.Any(e => e.EmployeeGradeHistoryId == id);
        }
    }
}
