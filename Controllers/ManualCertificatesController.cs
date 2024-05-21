using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("ManualCertificates")]
    [EnableQuery]
    public class ManualCertificatesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ManualCertificatesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ManualCertificates
        [HttpGet]
        public IQueryable<ManualCertificate> GetManualCertificates()
        {
            return _context.ManualCertificates.AsQueryable().AsNoTracking();
        }

        // GET: api/ManualCertificates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManualCertificate>> GetManualCertificate(int id)
        {
            var manualCertificate = await _context.ManualCertificates.FindAsync(id);

            if (manualCertificate == null)
            {
                return NotFound();
            }

            return manualCertificate;
        }

        // PUT: api/ManualCertificates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManualCertificate(int id, ManualCertificate manualCertificate)
        {
            if (id != manualCertificate.CertificateDataId)
            {
                return BadRequest();
            }

            _context.Entry(manualCertificate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManualCertificateExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ManualCertificate> manualCertificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ManualCertificates.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            manualCertificate.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ManualCertificateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            return Updated(entity);
        }
        // POST: api/ManualCertificates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ManualCertificate>> PostManualCertificate([FromBody]ManualCertificate manualCertificate)
        {
            try
            {
                _context.ManualCertificates.Add(manualCertificate);
                await _context.SaveChangesAsync();

                return Ok(manualCertificate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        // DELETE: api/ManualCertificates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManualCertificate(int id)
        {
            var manualCertificate = await _context.ManualCertificates.FindAsync(id);
            if (manualCertificate == null)
            {
                return NotFound();
            }

            _context.ManualCertificates.Remove(manualCertificate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManualCertificateExists(int id)
        {
            return _context.ManualCertificates.Any(e => e.CertificateDataId == id);
        }
    }
}
