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
    public class PageHistoriesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public PageHistoriesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/PageHistories
        [HttpGet]
        public IQueryable<PageHistory> GetPageHistories()
        {
            return _context.PageHistories.AsQueryable().AsNoTracking();
        }

        // GET: api/PageHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PageHistory>> GetPageHistory(short id)
        {
            var pageHistory = await _context.PageHistories.FindAsync(id);

            if (pageHistory == null)
            {
                return NotFound();
            }

            return pageHistory;
        }

        // PUT: api/PageHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPageHistory(short id, PageHistory pageHistory)
        {
            if (id != pageHistory.PageHistoryId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(pageHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageHistoryExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<PageHistory> pageHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.PageHistories.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            pageHistory.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageHistoryExists(key))
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
        // POST: api/PageHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PageHistory>> PostPageHistory([FromBody]PageHistory pageHistory)
        {
            _context.PageHistories.Add(pageHistory);
            await _context.SaveChangesAsync();

            return Ok(pageHistory);
        }

        // DELETE: api/PageHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePageHistory(short id)
        {
            var pageHistory = await _context.PageHistories.FindAsync(id);
            if (pageHistory == null)
            {
                return NotFound();
            }

            _context.PageHistories.Remove(pageHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PageHistoryExists(short id)
        {
            return _context.PageHistories.Any(e => e.PageHistoryId == id);
        }
    }
}
