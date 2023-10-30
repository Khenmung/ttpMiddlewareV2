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
    public class StudentFamilyNFriendsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentFamilyNFriendsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentFamilyNFriends
        [HttpGet]
        public IQueryable<StudentFamilyNFriend> GetStudentFamilyNFriends()
        {
            return _context.StudentFamilyNFriends.AsQueryable().AsNoTracking();
        }

        // GET: api/StudentFamilyNFriends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentFamilyNFriend>> GetStudentFamilyNFriend(int id)
        {
            var studentFamilyNFriend = await _context.StudentFamilyNFriends.FindAsync(id);

            if (studentFamilyNFriend == null)
            {
                return NotFound();
            }

            return studentFamilyNFriend;
        }

        // PUT: api/StudentFamilyNFriends/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentFamilyNFriend(int id, StudentFamilyNFriend studentFamilyNFriend)
        {
            if (id != studentFamilyNFriend.StudentFamilyNFriendId)
            {
                return BadRequest();
            }

            _context.Entry(studentFamilyNFriend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentFamilyNFriendExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentFamilyNFriend> studentFamilyNFriend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentFamilyNFriends.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            studentFamilyNFriend.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentFamilyNFriendExists(key))
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
        // POST: api/StudentFamilyNFriends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentFamilyNFriend>> PostStudentFamilyNFriend([FromBody]StudentFamilyNFriend studentFamilyNFriend)
        {
            _context.StudentFamilyNFriends.Add(studentFamilyNFriend);
            await _context.SaveChangesAsync();

            return Ok(studentFamilyNFriend);
        }

        // DELETE: api/StudentFamilyNFriends/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentFamilyNFriend(int id)
        {
            var studentFamilyNFriend = await _context.StudentFamilyNFriends.FindAsync(id);
            if (studentFamilyNFriend == null)
            {
                return NotFound();
            }

            _context.StudentFamilyNFriends.Remove(studentFamilyNFriend);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentFamilyNFriendExists(int id)
        {
            return _context.StudentFamilyNFriends.Any(e => e.StudentFamilyNFriendId == id);
        }
    }
}
