using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("Students")]
    [EnableQuery]
    public class DataSyncsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public DataSyncsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/DataSyncs
        [HttpGet]
        public IQueryable<DataSync> GetDataSyncs()
        {
            return _context.DataSyncs.AsQueryable().AsNoTracking();
        }

        // GET: api/DataSyncs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataSync>> GetDataSync(int id)
        {
            var dataSync = await _context.DataSyncs.FindAsync(id);

            if (dataSync == null)
            {
                return NotFound();
            }

            return dataSync;
        }

        // PUT: api/DataSyncs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataSync(int id, DataSync dataSync)
        {
            if (id != dataSync.DataSyncId)
            {
                return BadRequest();
            }

            _context.Entry(dataSync).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataSyncExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<DataSync> dataSync)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.DataSyncs.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            dataSync.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataSyncExists(key))
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
        // POST: api/DataSyncs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataSync>> PostDataSync([FromBody] DataSync dataSync)
        {
            try
            {
                _context.DataSyncs.Add(dataSync);
                await _context.SaveChangesAsync();

                return Ok(dataSync);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/DataSyncs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataSync(int id)
        {
            var dataSync = await _context.DataSyncs.FindAsync(id);
            if (dataSync == null)
            {
                return NotFound();
            }

            _context.DataSyncs.Remove(dataSync);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DataSyncExists(int id)
        {
            return _context.DataSyncs.Any(e => e.DataSyncId == id);
        }
    }
}
