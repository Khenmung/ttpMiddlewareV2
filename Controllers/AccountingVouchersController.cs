using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]    
    public class AccountingVouchersController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public AccountingVouchersController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/AccountingVouchers
        [HttpGet]
        public IQueryable<AccountingVoucher> GetAccountingVouchers()
        {
            return _context.AccountingVouchers.AsQueryable().AsNoTracking();
        }

        // GET: api/AccountingVouchers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountingVoucher>> GetAccountingVoucher(int id)
        {
            var accountingVoucher = await _context.AccountingVouchers.FindAsync(id);

            if (accountingVoucher == null)
            {
                return NotFound();
            }

            return accountingVoucher;
        }

        // PUT: api/AccountingVouchers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountingVoucher(int id, AccountingVoucher accountingVoucher)
        {
            if (id != accountingVoucher.AccountingVoucherId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(accountingVoucher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingVoucherExists(id))
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

        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<AccountingVoucher> accountingVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.AccountingVouchers.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            accountingVoucher.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingVoucherExists(key))
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

        // POST: api/AccountingVouchers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountingVoucher>> PostAccountingVoucher([FromBody]AccountingVoucher accountingVoucher)
        {
            _context.AccountingVouchers.Add(accountingVoucher);
            await _context.SaveChangesAsync();

            return Ok(accountingVoucher);
        }

        // DELETE: api/AccountingVouchers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountingVoucher(int id)
        {
            var accountingVoucher = await _context.AccountingVouchers.FindAsync(id);
            if (accountingVoucher == null)
            {
                return NotFound();
            }

            _context.AccountingVouchers.Remove(accountingVoucher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountingVoucherExists(int id)
        {
            return _context.AccountingVouchers.Any(e => e.AccountingVoucherId == id);
        }
    }
}
