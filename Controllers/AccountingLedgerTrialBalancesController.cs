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
    public class AccountingLedgerTrialBalancesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public AccountingLedgerTrialBalancesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/AccountingLedgerTrialBalances
        [HttpGet]
        public IQueryable<AccountingLedgerTrialBalance> GetAccountingLedgerTrialBalances()
        {
            return _context.AccountingLedgerTrialBalances.AsQueryable().AsNoTracking();
        }
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<AccountingLedgerTrialBalance> accountingLedgerTrialBalance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.AccountingLedgerTrialBalances.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            accountingLedgerTrialBalance.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingLedgerTrialBalanceExists(key))
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
        // GET: api/AccountingLedgerTrialBalances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountingLedgerTrialBalance>> GetAccountingLedgerTrialBalance(int id)
        {
            var accountingLedgerTrialBalance = await _context.AccountingLedgerTrialBalances.FindAsync(id);

            if (accountingLedgerTrialBalance == null)
            {
                return NotFound();
            }

            return accountingLedgerTrialBalance;
        }

        // PUT: api/AccountingLedgerTrialBalances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountingLedgerTrialBalance(int id, AccountingLedgerTrialBalance accountingLedgerTrialBalance)
        {
            if (id != accountingLedgerTrialBalance.LedgerId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(accountingLedgerTrialBalance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingLedgerTrialBalanceExists(id))
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

        // POST: api/AccountingLedgerTrialBalances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountingLedgerTrialBalance>> PostAccountingLedgerTrialBalance([FromBody] AccountingLedgerTrialBalance accountingLedgerTrialBalance)
        {
            var _existing = await _context.AccountingLedgerTrialBalances.Where(x => x.StudentClassId == accountingLedgerTrialBalance.StudentClassId
            && x.Month == accountingLedgerTrialBalance.Month
            && x.OrgId == accountingLedgerTrialBalance.OrgId
            && x.SubOrgId == accountingLedgerTrialBalance.SubOrgId
            ).ToListAsync();
            if (_existing.Count > 0)
            {
                return BadRequest("Record already exists!.");
            }
            else
            {
                _context.AccountingLedgerTrialBalances.Add(accountingLedgerTrialBalance);
                await _context.SaveChangesAsync();
                return Ok(accountingLedgerTrialBalance);
            }
        }

        // DELETE: api/AccountingLedgerTrialBalances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountingLedgerTrialBalance(int id)
        {
            var accountingLedgerTrialBalance = await _context.AccountingLedgerTrialBalances.FindAsync(id);
            if (accountingLedgerTrialBalance == null)
            {
                return NotFound();
            }

            _context.AccountingLedgerTrialBalances.Remove(accountingLedgerTrialBalance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountingLedgerTrialBalanceExists(int id)
        {
            return _context.AccountingLedgerTrialBalances.Any(e => e.LedgerId == id);
        }
    }
}
