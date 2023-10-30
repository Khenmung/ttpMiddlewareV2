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
    public class RoleUsersController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public RoleUsersController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/RoleUsers
        [HttpGet]
        public IQueryable<RoleUser> GetRoleUsers()
        {
            return _context.RoleUsers.AsQueryable().AsNoTracking();
        }

        // GET: api/RoleUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleUser>> GetRoleUser(short id)
        {
            var roleUser = await _context.RoleUsers.FindAsync(id);

            if (roleUser == null)
            {
                return NotFound();
            }

            return roleUser;
        }

        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody]Delta<RoleUser> roleUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.RoleUsers.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            roleUser.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleUserExists(key))
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
        // PUT: api/RoleUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleUser(short id, RoleUser roleUser)
        {
            if (id != roleUser.RoleUserId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(roleUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleUserExists(id))
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

        // POST: api/RoleUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleUser>> PostRoleUser([FromBody]RoleUser roleUser)
        {
            var _roleusers = await _context.RoleUsers.Where(x => x.RoleId == roleUser.RoleId 
            && x.UserId == roleUser.UserId 
            && x.OrgId == roleUser.OrgId
            && x.SubOrgId == roleUser.SubOrgId
            ).ToListAsync();
            if (_roleusers.Count == 0)
            {
                _context.RoleUsers.Add(roleUser);
                await _context.SaveChangesAsync();
            }
            return Ok(roleUser);
        }

        // DELETE: api/RoleUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleUser(short id)
        {
            var roleUser = await _context.RoleUsers.FindAsync(id);
            if (roleUser == null)
            {
                return NotFound();
            }

            _context.RoleUsers.Remove(roleUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleUserExists(short id)
        {
            return _context.RoleUsers.Any(e => e.RoleUserId == id);
        }
    }
}
