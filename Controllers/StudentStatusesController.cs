using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;
using ttpMiddleware.CommonFunctions;
using Newtonsoft.Json.Linq;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class StudentStatusesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentStatusesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/FeePaymentRelateds
        [HttpGet]
        public IQueryable<StudentStatus> GetStudentStatuses()
        {
            return _context.StudentStatuses.AsQueryable().AsNoTracking();
        }

        // GET: api/FeePaymentRelateds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentStatus>> GetStudentStatus(int id)
        {
            var feePaymentRelated = await _context.StudentStatuses.FindAsync(id);

            if (feePaymentRelated == null)
            {
                return NotFound();
            }

            return feePaymentRelated;
        }

        // PUT: api/FeePaymentRelateds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentStatus(int id, StudentStatus studentStatus)
        {
            if (id != studentStatus.StudentStatusId)
            {
                return BadRequest();
            }

            _context.Entry(studentStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentStatusesExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentStatus> studentStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentStatuses.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            studentStatus.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentStatusesExists(key))
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
        // POST: api/FeePaymentRelateds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentStatus>> PostStudentStatus([FromBody] JArray jsonWrapper)
        {
            //var _errormessage = "";


            JToken jsonValues = jsonWrapper;
            StudentStatus _StudentStatus = new StudentStatus();
            using var tran = _context.Database.BeginTransaction();
            try
            {
             
                foreach (var x in jsonValues)
                {
                    _StudentStatus = x.ToObject<StudentStatus>();
                    if(_StudentStatus.StudentStatusId==0)
                    _context.StudentStatuses.Add(_StudentStatus);
                    else
                    {
                        var related = _context.StudentStatuses.Where(x => x.StudentStatusId == _StudentStatus.StudentStatusId
                        && x.OrgId== _StudentStatus.OrgId
                        && x.SubOrgId== _StudentStatus.SubOrgId);

                        foreach(var item in related)
                        {
                            item.StudentStatusId = _StudentStatus.StudentStatusId;
                            item.Active = _StudentStatus.Active;
                            _context.Update(item);
                        }
                    }
                }
                await _context.SaveChangesAsync();
                tran.Commit();
                return Ok(_StudentStatus);
            }
            catch(Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/FeePaymentRelateds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentStatuses(int id)
        {
            var feePaymentRelated = await _context.StudentStatuses.FindAsync(id);
            if (feePaymentRelated == null)
            {
                return NotFound();
            }

            _context.StudentStatuses.Remove(feePaymentRelated);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentStatusesExists(int id)
        {
            return _context.StudentStatuses.Any(e => e.StudentStatusId == id);
        }
    }
}
