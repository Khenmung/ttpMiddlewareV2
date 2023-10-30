using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class EmpEmployeeSalaryComponentsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmpEmployeeSalaryComponentsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmpEmployeeSalaryComponents
        [HttpGet]
        public IQueryable<EmpEmployeeSalaryComponent> GetEmpEmployeeSalaryComponents()
        {
            return _context.EmpEmployeeSalaryComponents.AsNoTracking().AsQueryable();
        }

        // GET: api/EmpEmployeeSalaryComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpEmployeeSalaryComponent>> GetEmpEmployeeSalaryComponent(int id)
        {
            var empEmployeeSalaryComponent = await _context.EmpEmployeeSalaryComponents.FindAsync(id);

            if (empEmployeeSalaryComponent == null)
            {
                return NotFound();
            }

            return empEmployeeSalaryComponent;
        }

        // PUT: api/EmpEmployeeSalaryComponents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpEmployeeSalaryComponent(int id, EmpEmployeeSalaryComponent empEmployeeSalaryComponent)
        {
            if (id != empEmployeeSalaryComponent.EmployeeSalaryComponentId)
            {
                return BadRequest();
            }

            _context.Entry(empEmployeeSalaryComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeSalaryComponentExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmpEmployeeSalaryComponent> empEmployeeSalaryComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmpEmployeeSalaryComponents.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            try
            {
                empEmployeeSalaryComponent.Patch(entity);
                await _context.SaveChangesAsync();
                return Updated(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        // POST: api/EmpEmployeeSalaryComponents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpEmployeeSalaryComponent>> PostEmpEmployeeSalaryComponent([FromBody] JArray jsonWrapper)
        {
            JToken jsonValues = jsonWrapper;
            EmpEmployeeSalaryComponent employeeSalaryComponent = null;
            //StudentFeeReceipt _StudentFeeReceipt = new StudentFeeReceipt();
            try
            {
                foreach (var entity in jsonValues)
                {
                    employeeSalaryComponent = entity.ToObject<EmpEmployeeSalaryComponent>();
                    if (employeeSalaryComponent.EmployeeSalaryComponentId == 0)
                    {
                        var ex = await _context.EmpEmployeeSalaryComponents.Where(x => x.EmployeeId == employeeSalaryComponent.EmployeeId
                        && x.EmpComponentId == employeeSalaryComponent.EmpComponentId
                        && x.Month == employeeSalaryComponent.Month
                        && x.OrgId == employeeSalaryComponent.OrgId
                        && x.SubOrgId == employeeSalaryComponent.SubOrgId).FirstOrDefaultAsync();
                        if (ex == null)
                            _context.EmpEmployeeSalaryComponents.Add(employeeSalaryComponent);
                        else
                            throw new Exception(String.Format("Record already exists for employee :"+ employeeSalaryComponent.EmployeeId+", component: " + employeeSalaryComponent.EmpComponentId));
                    }
                    else
                    {
                        var ex = await _context.EmpEmployeeSalaryComponents.Where(x => x.EmployeeSalaryComponentId == employeeSalaryComponent.EmployeeSalaryComponentId
                        && x.OrgId == employeeSalaryComponent.OrgId
                        && x.SubOrgId == employeeSalaryComponent.SubOrgId).FirstOrDefaultAsync();
                        if (ex != null)
                        {
                            ex.EmployeeId = employeeSalaryComponent.EmployeeId;
                            ex.EmpComponentId = employeeSalaryComponent.EmpComponentId; 
                            ex.Month = employeeSalaryComponent.Month;
                            ex.OrgId = employeeSalaryComponent.OrgId;
                            ex.Amount = employeeSalaryComponent.Amount;
                            ex.Active = employeeSalaryComponent.Active;
                            ex.ActualFormulaOrAmount = employeeSalaryComponent.ActualFormulaOrAmount;
                            ex.BatchId = employeeSalaryComponent.BatchId;
                            ex.DepartmentId= employeeSalaryComponent.DepartmentId;
                            ex.SubOrgId = employeeSalaryComponent.SubOrgId;
                            ex.UpdatedDate = DateTime.Now;
                            ex.RealeasedDate = employeeSalaryComponent.RealeasedDate;
                            _context.Update(ex);
                        }   
                        else
                            throw new Exception(String.Format("Record not found for employee :" + employeeSalaryComponent.EmployeeId + ", component: " + employeeSalaryComponent.EmpComponentId));
                    }
                }
                await _context.SaveChangesAsync();
                return Ok(employeeSalaryComponent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
          
        }

        // DELETE: api/EmpEmployeeSalaryComponents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpEmployeeSalaryComponent(int id)
        {
            var empEmployeeSalaryComponent = await _context.EmpEmployeeSalaryComponents.FindAsync(id);
            if (empEmployeeSalaryComponent == null)
            {
                return NotFound();
            }

            _context.EmpEmployeeSalaryComponents.Remove(empEmployeeSalaryComponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpEmployeeSalaryComponentExists(int id)
        {
            return _context.EmpEmployeeSalaryComponents.Any(e => e.EmployeeSalaryComponentId == id);
        }
    }
}
