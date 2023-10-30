using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using ttpMiddleware.Configuration;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    public class CustomerInvoicesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public CustomerInvoicesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/CustomerInvoices
        [HttpGet]
        [EnableQuery]
        public IQueryable<CustomerInvoice> GetCustomerInvoices()
        {
            return _context.CustomerInvoices.AsQueryable().AsNoTracking();
        }

        // GET: api/CustomerInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInvoice>> GetCustomerInvoice(short id)
        {
            var customerInvoice = await _context.CustomerInvoices.FindAsync(id);

            if (customerInvoice == null)
            {
                return NotFound();
            }

            return customerInvoice;
        }

        // PUT: api/CustomerInvoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerInvoice(short id, CustomerInvoice customerInvoice)
        {
            if (id != customerInvoice.CustomerInvoiceId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(customerInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<CustomerInvoice> customerInvoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.CustomerInvoices.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            customerInvoice.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceExists(key))
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
        // POST: api/CustomerInvoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerInvoice>> PostCustomerInvoice(CustomerInvoice customerInvoice)
        {
            _context.CustomerInvoices.Add(customerInvoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerInvoice", new { id = customerInvoice.CustomerInvoiceId }, customerInvoice);
        }

        // DELETE: api/CustomerInvoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInvoice(int id)
        {
            var customerInvoice = await _context.CustomerInvoices.FindAsync(id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            _context.CustomerInvoices.Remove(customerInvoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerInvoiceExists(int id)
        {
            return _context.CustomerInvoices.Any(e => e.CustomerInvoiceId == id);
        }
    }
}
