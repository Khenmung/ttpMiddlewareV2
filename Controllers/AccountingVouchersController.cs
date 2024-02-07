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

using ttpMiddleware.CommonFunctions;
using Newtonsoft.Json.Linq;

namespace ttpMiddleware.Controllers
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
        public async Task<ActionResult<AccountingVoucher>> PostAccountingVoucher([FromBody] JObject jsonWrapper)
        {
            JToken jsonValues = jsonWrapper;
            List<LedgerPosting> _LedgerPosting = new List<LedgerPosting>();
            List<AccountingVoucher> _AccountingVouchers = new List<AccountingVoucher>();
            using var tran = _context.Database.BeginTransaction();
            try
            {
                foreach (JProperty x in jsonValues)
                {
                    if (x.Name == "LedgerPosting")
                        _LedgerPosting = x.Value.ToObject<List<LedgerPosting>>();
                    else if (x.Name == "AccountingVoucher")
                        _AccountingVouchers = x.Value.ToObject<List<AccountingVoucher>>();

                }
                foreach (AccountingVoucher voucher in _AccountingVouchers)
                {
                    if (voucher.AccountingVoucherId == 0)
                    {
                        _context.AccountingVouchers.Add(voucher);
                        await _context.SaveChangesAsync();
                        _LedgerPosting.Where(x => x.PostingGeneralLedgerId == voucher.GeneralLedgerAccountId 
                        && x.Debit == voucher.Debit).ToList().ForEach(s => s.AccountingVoucherId = voucher.AccountingVoucherId);
                        //foreach (var posting in postings)
                        //    posting.AccountingVoucherId = voucher.AccountingVoucherId;
                    }
                    else
                    {
                        var existingav = await _context.AccountingVouchers.Where(x => x.AccountingVoucherId == voucher.AccountingVoucherId).FirstOrDefaultAsync();
                        if (existingav != null)
                        {
                            existingav.Balance = voucher.Balance;
                            existingav.DocDate = voucher.DocDate;
                            existingav.PostingDate = voucher.PostingDate;
                            existingav.ClassFeeId = voucher.ClassFeeId;
                            existingav.FeeReceiptId = voucher.FeeReceiptId;
                            existingav.ParentId = voucher.ParentId;
                            existingav.BaseAmount = voucher.BaseAmount;
                            existingav.Balance = voucher.Balance;
                            existingav.OrgId = voucher.OrgId;
                            existingav.SubOrgId = voucher.SubOrgId;
                            existingav.Amount = voucher.Amount;
                            existingav.Debit = voucher.Debit;
                            existingav.Reference = voucher.Reference;
                            existingav.Active = voucher.Active;
                            existingav.ShortText = voucher.ShortText;
                            existingav.UpdatedDate = voucher.UpdatedDate;
                            _context.Update(existingav);
                        }
                    }
                }


                foreach (LedgerPosting posting in _LedgerPosting)
                {
                    if (posting.LedgerPostingId == 0)
                        _context.LedgerPostings.Add(posting);
                    else
                    {
                        var existingPosting = await _context.LedgerPostings.Where(x => x.LedgerPostingId == posting.LedgerPostingId).FirstOrDefaultAsync();
                        if (existingPosting != null)
                        {
                            existingPosting.PostingGeneralLedgerId = posting.PostingGeneralLedgerId;
                            existingPosting.AccountingVoucherId = posting.AccountingVoucherId;
                            existingPosting.OrgId = posting.OrgId;
                            existingPosting.SubOrgId = posting.SubOrgId;
                            existingPosting.Amount = posting.Amount;
                            existingPosting.Debit = posting.Debit;
                            existingPosting.Reference = posting.Reference;
                            existingPosting.Active = posting.Active;
                            existingPosting.ShortText = posting.ShortText;
                            existingPosting.UpdatedDate = posting.UpdatedDate;
                            _context.Update(existingPosting);
                        }
                    }
                }
                await _context.SaveChangesAsync();
                tran.Commit();

                return Ok(_AccountingVouchers[0]);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex);
            }

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
