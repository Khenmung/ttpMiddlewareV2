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
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class ConfigTablesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ConfigTablesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ConfigTables
        [HttpGet]
        public IQueryable<ConfigTable> GetConfigTables()
        {
            return _context.ConfigTables.AsQueryable().AsNoTracking();
        }

        // GET: api/ConfigTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfigTable>> GetConfigTable(int id)
        {
            var configTable = await _context.ConfigTables.FindAsync(id);

            if (configTable == null)
            {
                return NotFound();
            }

            return configTable;
        }

        // PUT: api/ConfigTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfigTable(int id, ConfigTable configTable)
        {
            if (id != configTable.ConfigTableId)
            {
                return BadRequest();
            }

            _context.Entry(configTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigTableExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ConfigTable> configTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ConfigTables.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            configTable.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                //tran.Rollback();
                if (!ConfigTableExists(key))
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
        // POST: api/ConfigTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConfigTable>> PostConfigTable([FromBody]ConfigTable configTable)
        {
            _context.ConfigTables.Add(configTable);
            await _context.SaveChangesAsync();

            return Ok(configTable);
        }

        // DELETE: api/ConfigTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfigTable(int id)
        {
            var configTable = await _context.ConfigTables.FindAsync(id);
            if (configTable == null)
            {
                return NotFound();
            }

            _context.ConfigTables.Remove(configTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfigTableExists(int id)
        {
            return _context.ConfigTables.Any(e => e.ConfigTableId == id);
        }
    }
}
