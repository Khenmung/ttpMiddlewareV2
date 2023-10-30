using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controllers]")]
    [EnableQuery]
    public class ExamClassGroupMapsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ExamClassGroupMapsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ExamClassGroupMaps
        [HttpGet]
        public IQueryable<ExamClassGroupMap> GetExamClassGroupMaps()
        {
            return _context.ExamClassGroupMaps.AsQueryable().AsNoTracking();
        }

        // GET: api/ExamClassGroupMaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamClassGroupMap>> GetExamClassGroupMap(short id)
        {
            var examClassGroupMap = await _context.ExamClassGroupMaps.FindAsync(id);

            if (examClassGroupMap == null)
            {
                return NotFound();
            }

            return examClassGroupMap;
        }

        // PUT: api/ExamClassGroupMaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamClassGroupMap(short id, ExamClassGroupMap examClassGroupMap)
        {
            if (id != examClassGroupMap.ExamClassGroupMapId)
            {
                return BadRequest();
            }

            _context.Entry(examClassGroupMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamClassGroupMapExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ExamClassGroupMap> examClassGroupMap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ExamClassGroupMaps.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            examClassGroupMap.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //tran.Rollback();
                if (!ExamClassGroupMapExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                throw;
            }

            return Updated(entity);
        }
        // POST: api/ExamClassGroupMaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamClassGroupMap>> PostExamClassGroupMap([FromBody]ExamClassGroupMap examClassGroupMap)
        {

            try
            {
                _context.ExamClassGroupMaps.Add(examClassGroupMap);
                await _context.SaveChangesAsync();

                return Ok(examClassGroupMap);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);

            }
            
        }

        // DELETE: api/ExamClassGroupMaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamClassGroupMap(short id)
        {
            var examClassGroupMap = await _context.ExamClassGroupMaps.FindAsync(id);
            if (examClassGroupMap == null)
            {
                return NotFound();
            }

            _context.ExamClassGroupMaps.Remove(examClassGroupMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamClassGroupMapExists(short id)
        {
            return _context.ExamClassGroupMaps.Any(e => e.ExamClassGroupMapId == id);
        }
    }
}
