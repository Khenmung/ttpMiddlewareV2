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
    public class ReportOrgReportColumnsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ReportOrgReportColumnsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ReportOrgReportColumns
        [HttpGet]
        public IQueryable<ReportOrgReportColumn> GetReportOrgReportColumns()
        {
            return _context.ReportOrgReportColumns.AsQueryable().AsNoTracking();
        }

        // GET: api/ReportOrgReportColumns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportOrgReportColumn>> GetReportOrgReportColumn(short id)
        {
            var reportOrgReportColumn = await _context.ReportOrgReportColumns.FindAsync(id);

            if (reportOrgReportColumn == null)
            {
                return NotFound();
            }

            return reportOrgReportColumn;
        }

        // PUT: api/ReportOrgReportColumns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportOrgReportColumn(short id, ReportOrgReportColumn reportOrgReportColumn)
        {
            if (id != reportOrgReportColumn.ReportOrgReportColumnId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(reportOrgReportColumn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportOrgReportColumnExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ReportOrgReportColumn> reportOrgReportColumn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ReportOrgReportColumns.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            reportOrgReportColumn.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportOrgReportColumnExists(key))
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
        // POST: api/ReportOrgReportColumns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportOrgReportColumn>> PostReportOrgReportColumn([FromBody]ReportOrgReportColumn reportOrgReportColumn)
        {
            _context.ReportOrgReportColumns.Add(reportOrgReportColumn);
            await _context.SaveChangesAsync();

            return Ok(reportOrgReportColumn);
        }

        // DELETE: api/ReportOrgReportColumns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportOrgReportColumn(short id)
        {
            var reportOrgReportColumn = await _context.ReportOrgReportColumns.FindAsync(id);
            if (reportOrgReportColumn == null)
            {
                return NotFound();
            }

            _context.ReportOrgReportColumns.Remove(reportOrgReportColumn);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportOrgReportColumnExists(short id)
        {
            return _context.ReportOrgReportColumns.Any(e => e.ReportOrgReportColumnId == id);
        }
    }
}
