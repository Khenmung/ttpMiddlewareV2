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
    public class JournalEntriesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public JournalEntriesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/JournalEntries
        [HttpGet]
        public IQueryable<JournalEntry> GetJournalEntries()
        {
            return _context.JournalEntries.AsQueryable().AsNoTracking();
        }

        // GET: api/JournalEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JournalEntry>> GetJournalEntry(int id)
        {
            var journalEntry = await _context.JournalEntries.FindAsync(id);

            if (journalEntry == null)
            {
                return NotFound();
            }

            return journalEntry;
        }

        // PUT: api/JournalEntries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJournalEntry(int id, JournalEntry journalEntry)
        {
            if (id != journalEntry.JournalEntryId)
            {
                return BadRequest();
            }

            _context.Entry(journalEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournalEntryExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<JournalEntry> journalEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.JournalEntries.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            journalEntry.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                //tran.Rollback();
                if (!JournalEntryExists(key))
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
        // POST: api/JournalEntries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JournalEntry>> PostJournalEntry([FromBody] JObject jsonWrapper)
        {
            //_context.JournalEntries.Add(journalEntry);
            //await _context.SaveChangesAsync();

            //return Ok(journalEntry);
            JToken jsonValues = jsonWrapper;
            List<LedgerPosting> _LedgerPosting = new List<LedgerPosting>();
            List<JournalEntry> _JournalEntry = new List<JournalEntry>();
            using var tran = _context.Database.BeginTransaction();
            try
            {
                foreach (JProperty x in jsonValues)
                {
                    if (x.Name == "LedgerPosting")
                        _LedgerPosting = x.Value.ToObject<List<LedgerPosting>>();
                    else if (x.Name == "JournalEntry")
                        _JournalEntry = x.Value.ToObject<List<JournalEntry>>();

                }
                foreach (JournalEntry journal in _JournalEntry)
                {
                    if (journal.JournalEntryId == 0)
                    {
                        _context.JournalEntries.Add(journal);
                        await _context.SaveChangesAsync();
                        _LedgerPosting.Where(x => x.PostingGeneralLedgerId == journal.GeneralLedgerAccountId
                        && x.Debit == journal.Debit).ToList().ForEach(s => s.JournalEntryId = journal.JournalEntryId);
                        //foreach (var posting in postings)
                        //    posting.AccountingVoucherId = voucher.AccountingVoucherId;
                    }
                    else
                    {
                        var existingav = await _context.JournalEntries.Where(x => x.JournalEntryId == journal.JournalEntryId).FirstOrDefaultAsync();
                        if (existingav != null)
                        {
                            existingav.Balance = journal.Balance;
                            existingav.DocDate = journal.DocDate;
                            existingav.PostingDate = journal.PostingDate;
                            existingav.BaseAmount = journal.BaseAmount;
                            existingav.Balance = journal.Balance;
                            existingav.OrgId = journal.OrgId;
                            existingav.SubOrgId = journal.SubOrgId;
                            existingav.Amount = journal.Amount;
                            existingav.Debit = journal.Debit;
                            existingav.Reference = journal.Reference;
                            existingav.Active = journal.Active;
                            existingav.ShortText = journal.ShortText;
                            existingav.UpdatedDate = journal.UpdatedDate;
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
                            existingPosting.JournalEntryId = posting.JournalEntryId;
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

                return Ok(_JournalEntry[0]);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/JournalEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJournalEntry(int id)
        {
            var journalEntry = await _context.JournalEntries.FindAsync(id);
            if (journalEntry == null)
            {
                return NotFound();
            }

            _context.JournalEntries.Remove(journalEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JournalEntryExists(int id)
        {
            return _context.JournalEntries.Any(e => e.JournalEntryId == id);
        }
    }
}
