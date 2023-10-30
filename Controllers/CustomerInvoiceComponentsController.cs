using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class CustomerInvoiceComponentsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public CustomerInvoiceComponentsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/CustomerInvoiceComponents
        [HttpGet]
        public IQueryable<CustomerInvoiceComponent> GetCustomerInvoiceComponents()
        {
            return _context.CustomerInvoiceComponents.AsQueryable().AsNoTracking();
        }

        // GET: api/CustomerInvoiceComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInvoiceComponent>> GetCustomerInvoiceComponent(int id)
        {
            var customerInvoiceComponent = await _context.CustomerInvoiceComponents.FindAsync(id);

            if (customerInvoiceComponent == null)
            {
                return NotFound();
            }

            return customerInvoiceComponent;
        }

        // PUT: api/CustomerInvoiceComponents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerInvoiceComponent(int id, CustomerInvoiceComponent customerInvoiceComponent)
        {
            if (id != customerInvoiceComponent.CustomerInvoiceComponentId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(customerInvoiceComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceComponentExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<CustomerInvoiceComponent> customerInvoiceComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.CustomerInvoiceComponents.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            customerInvoiceComponent.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInvoiceComponentExists(key))
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
        // POST: api/CustomerInvoiceComponents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerInvoiceComponent>> PostCustomerInvoiceComponent([FromBody]CustomerInvoiceComponent customerInvoiceComponent)
        {
            _context.CustomerInvoiceComponents.Add(customerInvoiceComponent);
            await _context.SaveChangesAsync();

            return Ok(customerInvoiceComponent);
        }

        // DELETE: api/CustomerInvoiceComponents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInvoiceComponent(int id)
        {
            var customerInvoiceComponent = await _context.CustomerInvoiceComponents.FindAsync(id);
            if (customerInvoiceComponent == null)
            {
                return NotFound();
            }

            _context.CustomerInvoiceComponents.Remove(customerInvoiceComponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerInvoiceComponentExists(int id)
        {
            return _context.CustomerInvoiceComponents.Any(e => e.CustomerInvoiceComponentId == id);
        }
    }
}
