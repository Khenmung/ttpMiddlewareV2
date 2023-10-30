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
    public class GeneratedCertificatesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public GeneratedCertificatesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/GeneratedCertificates
        [HttpGet]
        public IQueryable<GeneratedCertificate> GetGeneratedCertificates()
        {
            return _context.GeneratedCertificates.AsQueryable().AsNoTracking();
        }
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<GeneratedCertificate> generatedCertificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.GeneratedCertificates.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            generatedCertificate.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();
                return Updated(entity);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //tran.Rollback();
                if (!GeneratedCertificateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex) ;
                }
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                return BadRequest(ex);
            }

            
        }
        // GET: api/GeneratedCertificates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneratedCertificate>> GetGeneratedCertificate(int id)
        {
            var generatedCertificate = await _context.GeneratedCertificates.FindAsync(id);

            if (generatedCertificate == null)
            {
                return NotFound();
            }

            return generatedCertificate;
        }

        // PUT: api/GeneratedCertificates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneratedCertificate(int id, GeneratedCertificate generatedCertificate)
        {
            if (id != generatedCertificate.GeneratedCertificateId)
            {
                return BadRequest();
            }

            _context.Entry(generatedCertificate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneratedCertificateExists(id))
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

        // POST: api/GeneratedCertificates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeneratedCertificate>> PostGeneratedCertificate([FromBody] GeneratedCertificate generatedCertificate)
        {
            _context.GeneratedCertificates.Add(generatedCertificate);
            await _context.SaveChangesAsync();

            return Ok(generatedCertificate);
        }

        // DELETE: api/GeneratedCertificates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneratedCertificate(int id)
        {
            var generatedCertificate = await _context.GeneratedCertificates.FindAsync(id);
            if (generatedCertificate == null)
            {
                return NotFound();
            }

            _context.GeneratedCertificates.Remove(generatedCertificate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneratedCertificateExists(int id)
        {
            return _context.GeneratedCertificates.Any(e => e.GeneratedCertificateId == id);
        }
    }
}
