using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class Config_tableController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public Config_tableController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/Config_table
        [HttpGet]
        public IQueryable<Config_table> GetConfig_table()
        {
            return _context.Config_tables.AsQueryable().AsNoTracking();
        }

        // GET: api/Config_table/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Config_table>> GetConfig_table(int id)
        {
            var config_table = await _context.Config_tables.FindAsync(id);

            if (config_table == null)
            {
                return NotFound();
            }

            return config_table;
        }

        // PUT: api/Config_table/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfig_table(int id, Config_table config_table)
        {
            if (id != config_table.Id)
            {
                return BadRequest();
            }

            _context.Entry(config_table).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Config_tableExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<Config_table> batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.Config_tables.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            batch.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                //tran.Rollback();
                if (!Config_tableExists(key))
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
        // POST: api/Config_table
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Config_table>> PostConfig_table([FromBody]Config_table config_table)
        {
            try
            {
                _context.Config_tables.Add(config_table);
                await _context.SaveChangesAsync();

                return Ok(config_table);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
           
        }

        // DELETE: api/Config_table/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig_table(int id)
        {
            var config_table = await _context.Config_tables.FindAsync(id);
            if (config_table == null)
            {
                return NotFound();
            }

            _context.Config_tables.Remove(config_table);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Config_tableExists(int id)
        {
            return _context.Config_tables.Any(e => e.Id == id);
        }
    }
}
