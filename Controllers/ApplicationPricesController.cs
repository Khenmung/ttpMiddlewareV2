using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class ApplicationPricesController : ODataController
    {
        private readonly ttpauthContext _context;

        public ApplicationPricesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationPrices
        [HttpGet]
        public IQueryable<ApplicationPrice> GetApplicationPrices()
        {
            return _context.ApplicationPrices.AsQueryable().AsNoTracking();
        }

        // GET: api/ApplicationPrices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationPrice>> GetApplicationPrice(short id)
        {
            var applicationPrice = await _context.ApplicationPrices.FindAsync(id);

            if (applicationPrice == null)
            {
                return NotFound();
            }

            return applicationPrice;
        }

        // PUT: api/ApplicationPrices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationPrice(short id, ApplicationPrice applicationPrice)
        {
            if (id != applicationPrice.ApplicationPriceId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(applicationPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationPriceExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ApplicationPrice> applicationPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ApplicationPrices.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            applicationPrice.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationPriceExists(key))
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
        // POST: api/ApplicationPrices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationPrice>> PostApplicationPrice([FromBody]ApplicationPrice applicationPrice)
        {
            _context.ApplicationPrices.Add(applicationPrice);
            await _context.SaveChangesAsync();

            return Ok(applicationPrice);
        }

        // DELETE: api/ApplicationPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationPrice(short id)
        {
            var applicationPrice = await _context.ApplicationPrices.FindAsync(id);
            if (applicationPrice == null)
            {
                return NotFound();
            }

            _context.ApplicationPrices.Remove(applicationPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationPriceExists(short id)
        {
            return _context.ApplicationPrices.Any(e => e.ApplicationPriceId == id);
        }
    }
}
