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
    public class GroupPointsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public GroupPointsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/GroupPoints
        [HttpGet]
        public IQueryable<GroupPoint> GetGroupPoints()
        {
            return _context.GroupPoints.AsQueryable().AsNoTracking() ;
        }

        // GET: api/GroupPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupPoint>> GetGroupPoint(int id)
        {
            var groupPoint = await _context.GroupPoints.FindAsync(id);

            if (groupPoint == null)
            {
                return NotFound();
            }

            return groupPoint;
        }

        // PUT: api/GroupPoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupPoint(int id, GroupPoint groupPoint)
        {
            if (id != groupPoint.GroupPointId)
            {
                return BadRequest();
            }

            _context.Entry(groupPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupPointExists(id))
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

        // POST: api/GroupPoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupPoint>> PostGroupPoint([FromBody]GroupPoint groupPoint)
        {
            _context.GroupPoints.Add(groupPoint);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GroupPointExists(groupPoint.GroupPointId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(groupPoint);
        }
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<GroupPoint> groupPoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.GroupPoints.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            groupPoint.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupPointExists(key))
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
        // DELETE: api/GroupPoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupPoint(int id)
        {
            var groupPoint = await _context.GroupPoints.FindAsync(id);
            if (groupPoint == null)
            {
                return NotFound();
            }

            _context.GroupPoints.Remove(groupPoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupPointExists(int id)
        {
            return _context.GroupPoints.Any(e => e.GroupPointId == id);
        }
    }
}
