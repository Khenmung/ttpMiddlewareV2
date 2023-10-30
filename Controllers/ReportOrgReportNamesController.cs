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
    public class ReportOrgReportNamesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ReportOrgReportNamesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ReportOrgReportNames
        [HttpGet]
        public IQueryable<ReportOrgReportName> GetReportOrgReportNames()
        {
            return _context.ReportOrgReportNames.AsQueryable().AsNoTracking();
        }

        // GET: api/ReportOrgReportNames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportOrgReportName>> GetReportOrgReportName(short id)
        {
            var reportOrgReportName = await _context.ReportOrgReportNames.FindAsync(id);

            if (reportOrgReportName == null)
            {
                return NotFound();
            }

            return reportOrgReportName;
        }

        // PUT: api/ReportOrgReportNames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportOrgReportName(short id, ReportOrgReportName reportOrgReportName)
        {
            if (id != reportOrgReportName.ReportOrgReportNameId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(reportOrgReportName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportOrgReportNameExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ReportOrgReportName> reportOrgReportName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ReportOrgReportNames.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            reportOrgReportName.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportOrgReportNameExists(key))
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
        // POST: api/ReportOrgReportNames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportOrgReportName>> PostReportOrgReportName([FromBody]ReportOrgReportName reportOrgReportName)
        {
            _context.ReportOrgReportNames.Add(reportOrgReportName);
            await _context.SaveChangesAsync();

            return Ok(reportOrgReportName);
        }

        // DELETE: api/ReportOrgReportNames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportOrgReportName(short id)
        {
            var reportOrgReportName = await _context.ReportOrgReportNames.FindAsync(id);
            if (reportOrgReportName == null)
            {
                return NotFound();
            }

            _context.ReportOrgReportNames.Remove(reportOrgReportName);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportOrgReportNameExists(short id)
        {
            return _context.ReportOrgReportNames.Any(e => e.ReportOrgReportNameId == id);
        }
    }
}
