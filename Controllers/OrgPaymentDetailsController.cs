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
    [ODataRoutePrefix("OrgPaymentDetails")]
    [EnableQuery]
    public class OrgPaymentDetailsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public OrgPaymentDetailsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/OrgPaymentDetails
        [HttpGet]
        public IQueryable<OrgPaymentDetail> GetOrgPaymentDetails()
        {
            return _context.OrgPaymentDetails.AsQueryable().AsNoTracking();
        }

        // GET: api/OrgPaymentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrgPaymentDetail>> GetOrgPaymentDetail(int id)
        {
            var orgPaymentDetail = await _context.OrgPaymentDetails.FindAsync(id);

            if (orgPaymentDetail == null)
            {
                return NotFound();
            }

            return orgPaymentDetail;
        }

        // PUT: api/OrgPaymentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrgPaymentDetail(int id, OrgPaymentDetail orgPaymentDetail)
        {
            if (id != orgPaymentDetail.OrgPaymentDetailId)
            {
                return BadRequest();
            }

            _context.Entry(orgPaymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrgPaymentDetailExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<OrgPaymentDetail> orgPaymentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.OrgPaymentDetails.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            orgPaymentDetail.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();
       
            }
            catch (DbUpdateConcurrencyException)
            {
                //tran.Rollback();
                if (!OrgPaymentDetailExists(key))
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
        // POST: api/OrgPaymentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrgPaymentDetail>> PostOrgPaymentDetail([FromBody]OrgPaymentDetail orgPaymentDetail)
        {
            _context.OrgPaymentDetails.Add(orgPaymentDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrgPaymentDetailExists(orgPaymentDetail.OrgPaymentDetailId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(orgPaymentDetail);
        }

        // DELETE: api/OrgPaymentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrgPaymentDetail(int id)
        {
            var orgPaymentDetail = await _context.OrgPaymentDetails.FindAsync(id);
            if (orgPaymentDetail == null)
            {
                return NotFound();
            }

            _context.OrgPaymentDetails.Remove(orgPaymentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrgPaymentDetailExists(int id)
        {
            return _context.OrgPaymentDetails.Any(e => e.OrgPaymentDetailId == id);
        }
    }
}
