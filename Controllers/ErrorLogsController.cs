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

    public class ErrorLogsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ErrorLogsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ErrorLogs
        [HttpGet]
        public IQueryable<ErrorLog> GetErrorLogs()
        {
            return _context.ErrorLogs.AsQueryable().AsNoTracking() ;
        }

        // GET: api/ErrorLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrorLog>> GetErrorLog(int id)
        {
            var errorLog = await _context.ErrorLogs.FindAsync(id);

            if (errorLog == null)
            {
                return NotFound();
            }

            return errorLog;
        }

        // PUT: api/ErrorLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErrorLog(int id, ErrorLog errorLog)
        {
            if (id != errorLog.ErrorId)
            {
                return BadRequest();
            }

            _context.Entry(errorLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorLogExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ErrorLog> errorLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ErrorLogs.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            errorLog.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorLogExists(key))
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
        // POST: api/ErrorLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ErrorLog>> PostErrorLog([FromBody]ErrorLog errorLog)
        {
            _context.ErrorLogs.Add(errorLog);
            await _context.SaveChangesAsync();

            return Ok(errorLog);
        }

        // DELETE: api/ErrorLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErrorLog(int id)
        {
            var errorLog = await _context.ErrorLogs.FindAsync(id);
            if (errorLog == null)
            {
                return NotFound();
            }

            _context.ErrorLogs.Remove(errorLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrorLogExists(int id)
        {
            return _context.ErrorLogs.Any(e => e.ErrorId == id);
        }
    }
}
