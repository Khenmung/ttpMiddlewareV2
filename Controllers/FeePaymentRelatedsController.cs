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
using Newtonsoft.Json.Linq;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class FeePaymentRelatedsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public FeePaymentRelatedsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/FeePaymentRelateds
        [HttpGet]
        public IQueryable<FeePaymentRelated> GetFeePaymentRelateds()
        {
            return _context.FeePaymentRelateds.AsQueryable().AsNoTracking();
        }

        // GET: api/FeePaymentRelateds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeePaymentRelated>> GetFeePaymentRelated(int id)
        {
            var feePaymentRelated = await _context.FeePaymentRelateds.FindAsync(id);

            if (feePaymentRelated == null)
            {
                return NotFound();
            }

            return feePaymentRelated;
        }

        // PUT: api/FeePaymentRelateds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeePaymentRelated(int id, FeePaymentRelated feePaymentRelated)
        {
            if (id != feePaymentRelated.FeePaymentRelatedId)
            {
                return BadRequest();
            }

            _context.Entry(feePaymentRelated).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeePaymentRelatedExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<FeePaymentRelated> feePaymentRelated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.FeePaymentRelateds.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            feePaymentRelated.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeePaymentRelatedExists(key))
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
        public async Task<ActionResult<FeePaymentRelated>> PostFeePaymentRelated([FromBody] JArray jsonWrapper)
        {
            //var _errormessage = "";


            JToken jsonValues = jsonWrapper;
            FeePaymentRelated _feePaymentRelated = new FeePaymentRelated();
            using var tran = _context.Database.BeginTransaction();
            try
            {
             
                foreach (var x in jsonValues)
                {
                    _feePaymentRelated = x.ToObject<FeePaymentRelated>();
                    if(_feePaymentRelated.FeePaymentRelatedId==0)
                    _context.FeePaymentRelateds.Add(_feePaymentRelated);
                    else
                    {
                        var related = _context.FeePaymentRelateds.Where(x => x.FeePaymentRelatedId == _feePaymentRelated.FeePaymentRelatedId
                        && x.OrgId== _feePaymentRelated.OrgId
                        && x.SubOrgId== _feePaymentRelated.SubOrgId);

                        foreach(var item in related)
                        {
                            item.FeepaymentStatusId = _feePaymentRelated.FeepaymentStatusId;
                            item.Active = _feePaymentRelated.Active;
                            _context.Update(item);
                        }
                    }
                }
                await _context.SaveChangesAsync();
                tran.Commit();
                return Ok(_feePaymentRelated);
            }
            catch(Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/FeePaymentRelateds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeePaymentRelated(int id)
        {
            var feePaymentRelated = await _context.FeePaymentRelateds.FindAsync(id);
            if (feePaymentRelated == null)
            {
                return NotFound();
            }

            _context.FeePaymentRelateds.Remove(feePaymentRelated);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeePaymentRelatedExists(int id)
        {
            return _context.FeePaymentRelateds.Any(e => e.FeePaymentRelatedId == id);
        }
    }
}
