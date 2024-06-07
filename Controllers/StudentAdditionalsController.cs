using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;
using ttpMiddleware.CommonFunctions;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class StudentAdditionalsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentAdditionalsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentAdditionals
        [HttpGet]
        public IQueryable<StudentAdditional> GetStudentAdditionals()
        {
            return _context.StudentAdditionals.AsQueryable().AsNoTracking();
        }

        // GET: api/StudentAdditionals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentAdditional>> GetStudentAdditional(int id)
        {
            var studentAdditional = await _context.StudentAdditionals.FindAsync(id);

            if (studentAdditional == null)
            {
                return NotFound();
            }

            return studentAdditional;
        }

        // PUT: api/StudentAdditionals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentAdditional(int id, StudentAdditional studentAdditional)
        {
            if (id != studentAdditional.StudentAdditionalId)
            {
                return BadRequest();
            }

            _context.Entry(studentAdditional).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!StudentAdditionalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return NoContent();
        }
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentAdditional> studentAdditional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentAdditionals.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            studentAdditional.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                //tran.Rollback();
                if (!StudentAdditionalExists(key))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                return BadRequest(ex);
            }

            return Updated(entity);
        }
        // POST: api/StudentAdditionals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentAdditional>> PostStudentAdditional([FromBody]StudentAdditional studentAdditional)
        {
            _context.StudentAdditionals.Add(studentAdditional);
            await _context.SaveChangesAsync();

            return Ok(studentAdditional);
        }

        // DELETE: api/StudentAdditionals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentAdditional(int id)
        {
            var studentAdditional = await _context.StudentAdditionals.FindAsync(id);
            if (studentAdditional == null)
            {
                return NotFound();
            }

            _context.StudentAdditionals.Remove(studentAdditional);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentAdditionalExists(int id)
        {
            return _context.StudentAdditionals.Any(e => e.StudentAdditionalId == id);
        }
    }
}
