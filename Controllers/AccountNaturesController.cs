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
    public class AccountNaturesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public AccountNaturesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/AccountNatures
        [HttpGet]
        public IQueryable<AccountNature> GetAccountNatures()
        {
            return _context.AccountNatures.AsQueryable().AsNoTracking();
        }

        // GET: api/AccountNatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountNature>> GetAccountNature(int id)
        {
            var AccountNature = await _context.AccountNatures.FindAsync(id);

            if (AccountNature == null)
            {
                return NotFound();
            }

            return AccountNature;
        }

        // PUT: api/AccountNatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountNature(int id, AccountNature AccountNature)
        {
            if (id != AccountNature.AccountNatureId)
            {
                return BadRequest();
            }

            _context.Entry(AccountNature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountNatureExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<AccountNature> accountNature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.AccountNatures.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            accountNature.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountNatureExists(key))
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
        // POST: api/AccountNatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountNature>> PostAccountNature([FromBody]AccountNature AccountNature)
        {
            _context.AccountNatures.Add(AccountNature);
            await _context.SaveChangesAsync();

            return Ok(AccountNature);
        }

        // DELETE: api/AccountNatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountNature(int id)
        {
            var AccountNature = await _context.AccountNatures.FindAsync(id);
            if (AccountNature == null)
            {
                return NotFound();
            }

            _context.AccountNatures.Remove(AccountNature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountNatureExists(int id)
        {
            return _context.AccountNatures.Any(e => e.AccountNatureId == id);
        }
    }
}
