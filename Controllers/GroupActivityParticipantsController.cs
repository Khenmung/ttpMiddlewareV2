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
    public class GroupActivityParticipantsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public GroupActivityParticipantsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/GroupActivityParticipants
        [HttpGet]
        public IQueryable<GroupActivityParticipant> GetGroupActivityParticipants()
        {
            return _context.GroupActivityParticipants.AsQueryable().AsNoTracking();
        }

        // GET: api/GroupActivityParticipants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupActivityParticipant>> GetGroupActivityParticipant(int id)
        {
            var groupActivityParticipant = await _context.GroupActivityParticipants.FindAsync(id);

            if (groupActivityParticipant == null)
            {
                return NotFound();
            }

            return groupActivityParticipant;
        }

       
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<GroupActivityParticipant> groupActivityParticipant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.GroupActivityParticipants.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            groupActivityParticipant.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupActivityParticipantExists(key))
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
        // PUT: api/GroupActivityParticipants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupActivityParticipant(int id, GroupActivityParticipant groupActivityParticipant)
        {
            if (id != groupActivityParticipant.GroupActivityParticipantId)
            {
                return BadRequest();
            }

            _context.Entry(groupActivityParticipant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupActivityParticipantExists(id))
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

        // POST: api/GroupActivityParticipants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupActivityParticipant>> PostGroupActivityParticipant([FromBody]GroupActivityParticipant groupActivityParticipant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try {
                _context.GroupActivityParticipants.Add(groupActivityParticipant);
                await _context.SaveChangesAsync();

                return Ok(groupActivityParticipant);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        // DELETE: api/GroupActivityParticipants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupActivityParticipant(int id)
        {
            var groupActivityParticipant = await _context.GroupActivityParticipants.FindAsync(id);
            if (groupActivityParticipant == null)
            {
                return NotFound();
            }

            _context.GroupActivityParticipants.Remove(groupActivityParticipant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupActivityParticipantExists(int id)
        {
            return _context.GroupActivityParticipants.Any(e => e.GroupActivityParticipantId == id);
        }
    }
}
