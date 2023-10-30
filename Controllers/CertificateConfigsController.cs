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
    public class CertificateConfigsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public CertificateConfigsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/CertificateConfigs
        [HttpGet]
        public IQueryable<CertificateConfig> GetCertificateConfigs()
        {
            return  _context.CertificateConfigs.AsQueryable().AsNoTracking();
        }

        // GET: api/CertificateConfigs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CertificateConfig>> GetCertificateConfig(int id)
        {
            var certificateConfig = await _context.CertificateConfigs.FindAsync(id);

            if (certificateConfig == null)
            {
                return NotFound();
            }

            return certificateConfig;
        }

        // PUT: api/CertificateConfigs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificateConfig(int id, CertificateConfig certificateConfig)
        {
            if (id != certificateConfig.CertificateConfigId)
            {
                return BadRequest();
            }

            _context.Entry(certificateConfig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificateConfigExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<CertificateConfig> certificateConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.CertificateConfigs.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            certificateConfig.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificateConfigExists(key))
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
        // POST: api/CertificateConfigs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CertificateConfig>> PostCertificateConfig([FromBody]CertificateConfig certificateConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.CertificateConfigs.Add(certificateConfig);
                await _context.SaveChangesAsync();

                return Ok(certificateConfig);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/CertificateConfigs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificateConfig(int id)
        {
            var certificateConfig = await _context.CertificateConfigs.FindAsync(id);
            if (certificateConfig == null)
            {
                return NotFound();
            }

            _context.CertificateConfigs.Remove(certificateConfig);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertificateConfigExists(int id)
        {
            return _context.CertificateConfigs.Any(e => e.CertificateConfigId == id);
        }
    }
}
