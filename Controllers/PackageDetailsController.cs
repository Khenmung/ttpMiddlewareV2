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
    public class PackageDetailsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public PackageDetailsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/PackageDetails
        [HttpGet]
        public IQueryable<PackageDetail> GetPackageDetails()
        {
            return _context.PackageDetails.AsQueryable().AsNoTracking();
        }

        // GET: api/PackageDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageDetail>> GetPackageDetail(Guid id)
        {
            var packageDetail = await _context.PackageDetails.FindAsync(id);

            if (packageDetail == null)
            {
                return NotFound();
            }

            return packageDetail;
        }

        // PUT: api/PackageDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackageDetail(int id, PackageDetail packageDetail)
        {
            if (id != packageDetail.PackageDetailId)
            {
                return BadRequest();
            }

            _context.Entry(packageDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageDetailExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<PackageDetail> packageDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.PackageDetails.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            packageDetail.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                //tran.Rollback();
                if (!PackageDetailExists(key))
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
        // POST: api/PackageDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PackageDetail>> PostPackageDetail([FromBody]PackageDetail packageDetail)
        {
            _context.PackageDetails.Add(packageDetail);
            await _context.SaveChangesAsync();

            return Ok(packageDetail);
        }

        // DELETE: api/PackageDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageDetail(Guid id)
        {
            var packageDetail = await _context.PackageDetails.FindAsync(id);
            if (packageDetail == null)
            {
                return NotFound();
            }

            _context.PackageDetails.Remove(packageDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageDetailExists(int id)
        {
            return _context.PackageDetails.Any(e => e.PackageDetailId== id);
        }
    }
}
