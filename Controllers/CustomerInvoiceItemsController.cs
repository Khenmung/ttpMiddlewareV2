using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class CustomerInvoiceItemsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public CustomerInvoiceItemsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/CustomerInvoiceItems
        [HttpGet]
        public IQueryable<CustomerInvoiceItem> GetCustomerInvoiceItems()
        {
            return _context.CustomerInvoiceItems.AsQueryable().AsNoTracking();
        }

        // GET: api/CustomerInvoiceItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInvoiceItem>> GetCustomerInvoiceItem(int id)
        {
            var customerInvoiceItem = await _context.CustomerInvoiceItems.FindAsync(id);

            if (customerInvoiceItem == null)
            {
                return NotFound();
            }

            return customerInvoiceItem;
        }

        // PUT: api/CustomerInvoiceItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerInvoiceItem(int id, CustomerInvoiceItem customerInvoiceItem)
        {
            if (id != customerInvoiceItem.CustomerInvoiceItemId)
            {
                return BadRequest();
            }

            _context.Entry(customerInvoiceItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceItemExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<CustomerInvoiceItem> customerInvoiceItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.CustomerInvoiceItems.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            customerInvoiceItem.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceItemExists(key))
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
        // POST: api/CustomerInvoiceItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerInvoiceItem>> PostCustomerInvoiceItem([FromBody]CustomerInvoiceItem customerInvoiceItem)
        {
            _context.CustomerInvoiceItems.Add(customerInvoiceItem);
            await _context.SaveChangesAsync();

            return Ok(customerInvoiceItem);
        }

        // DELETE: api/CustomerInvoiceItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInvoiceItem(int id)
        {
            var customerInvoiceItem = await _context.CustomerInvoiceItems.FindAsync(id);
            if (customerInvoiceItem == null)
            {
                return NotFound();
            }

            _context.CustomerInvoiceItems.Remove(customerInvoiceItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerInvoiceItemExists(int id)
        {
            return _context.CustomerInvoiceItems.Any(e => e.CustomerInvoiceItemId == id);
        }
    }
}
