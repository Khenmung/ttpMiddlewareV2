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
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class SyllabusDetailsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public SyllabusDetailsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/SyllabusDetails
        [HttpGet]
        public IQueryable<SyllabusDetail> GetSyllabusDetails()
        {
            return _context.SyllabusDetails.AsQueryable().AsNoTracking();
        }

        // GET: api/SyllabusDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SyllabusDetail>> GetSyllabusDetail(int id)
        {
            var syllabusDetail = await _context.SyllabusDetails.FindAsync(id);

            if (syllabusDetail == null)
            {
                return NotFound();
            }

            return syllabusDetail;
        }

        // PUT: api/SyllabusDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSyllabusDetail(int id, SyllabusDetail syllabusDetail)
        {
            if (id != syllabusDetail.SyllabusId)
            {
                return BadRequest();
            }

            _context.Entry(syllabusDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SyllabusDetailExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<SyllabusDetail> syllabusDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.SyllabusDetails.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            syllabusDetail.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SyllabusDetailExists(key))
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
        // POST: api/SyllabusDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SyllabusDetail>> PostSyllabusDetail([FromBody] SyllabusDetail syllabusDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.SyllabusDetails.Add(syllabusDetail);
                await _context.SaveChangesAsync();

                return Ok(syllabusDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/SyllabusDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSyllabusDetail(int id)
        {
            var syllabusDetail = await _context.SyllabusDetails.FindAsync(id);
            if (syllabusDetail == null)
            {
                return NotFound();
            }

            _context.SyllabusDetails.Remove(syllabusDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SyllabusDetailExists(int id)
        {
            return _context.SyllabusDetails.Any(e => e.SyllabusId == id);
        }
    }
}
