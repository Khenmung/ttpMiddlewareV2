using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class ReportConfigItemsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ReportConfigItemsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ReportConfigItems
        [HttpGet]
        public IQueryable<ReportConfigItem> GetReportConfigItems()
        {
            return _context.ReportConfigItems.AsQueryable().AsNoTracking();
        }

        // GET: api/ReportConfigItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportConfigItem>> GetReportConfigItem(int id)
        {
            var reportConfigItem = await _context.ReportConfigItems.FindAsync(id);

            if (reportConfigItem == null)
            {
                return NotFound();
            }

            return reportConfigItem;
        }

        // PUT: api/ReportConfigItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportConfigItem(int id, ReportConfigItem reportConfigItem)
        {
            if (id != reportConfigItem.ReportConfigItemId)
            {
                return BadRequest();
            }

            _context.Entry(reportConfigItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportConfigItemExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ReportConfigItem> reportConfigItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ReportConfigItems.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            reportConfigItem.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportConfigItemExists(key))
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
        // POST: api/ReportConfigItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportConfigItem>> PostReportConfigItem([FromBody]ReportConfigItem reportConfigItem)
        {
            _context.ReportConfigItems.Add(reportConfigItem);
            await _context.SaveChangesAsync();

            return Ok(reportConfigItem);
        }

        // DELETE: api/ReportConfigItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportConfigItem(int id)
        {
            var reportConfigItem = await _context.ReportConfigItems.FindAsync(id);
            if (reportConfigItem == null)
            {
                return NotFound();
            }

            _context.ReportConfigItems.Remove(reportConfigItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportConfigItemExists(int id)
        {
            return _context.ReportConfigItems.Any(e => e.ReportConfigItemId == id);
        }
    }
}
