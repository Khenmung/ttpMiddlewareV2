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

    public class AchievementAndPointsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public AchievementAndPointsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/AchievementAndPoints
        [HttpGet]
        public IQueryable<AchievementAndPoint> GetAchievementAndPoints()
        {
            return _context.AchievementAndPoints.AsQueryable().AsNoTracking();
        }

        // GET: api/AchievementAndPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AchievementAndPoint>> GetAchievementAndPoint(int id)
        {
            var achievementAndPoint = await _context.AchievementAndPoints.FindAsync(id);

            if (achievementAndPoint == null)
            {
                return NotFound();
            }

            return achievementAndPoint;
        }

        // PUT: api/AchievementAndPoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAchievementAndPoint(int id, AchievementAndPoint achievementAndPoint)
        {
            if (id != achievementAndPoint.AchievementAndPointId)
            {
                return BadRequest();
            }

            _context.Entry(achievementAndPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievementAndPointExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<AchievementAndPoint> achievementAndPoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.AchievementAndPoints.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            achievementAndPoint.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievementAndPointExists(key))
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
        // POST: api/AchievementAndPoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AchievementAndPoint>> PostAchievementAndPoint([FromBody] AchievementAndPoint achievementAndPoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.AchievementAndPoints.Add(achievementAndPoint);
                await _context.SaveChangesAsync();

                return Ok(achievementAndPoint);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/AchievementAndPoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievementAndPoint(int id)
        {
            var achievementAndPoint = await _context.AchievementAndPoints.FindAsync(id);
            if (achievementAndPoint == null)
            {
                return NotFound();
            }

            _context.AchievementAndPoints.Remove(achievementAndPoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AchievementAndPointExists(int id)
        {
            return _context.AchievementAndPoints.Any(e => e.AchievementAndPointId == id);
        }
    }
}
