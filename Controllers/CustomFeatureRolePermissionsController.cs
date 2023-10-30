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

using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class CustomFeatureRolePermissionsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public CustomFeatureRolePermissionsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/CustomFeatureRolePermissions
        [HttpGet]
        public IQueryable<CustomFeatureRolePermission> GetCustomFeatureRolePermissions()
        {
            return _context.CustomFeatureRolePermissions.AsQueryable().AsNoTracking();
        }

        // GET: api/CustomFeatureRolePermissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomFeatureRolePermission>> GetCustomFeatureRolePermission(int id)
        {
            var customFeatureRolePermission = await _context.CustomFeatureRolePermissions.FindAsync(id);

            if (customFeatureRolePermission == null)
            {
                return NotFound();
            }

            return customFeatureRolePermission;
        }

        // PUT: api/CustomFeatureRolePermissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomFeatureRolePermission(int id, CustomFeatureRolePermission customFeatureRolePermission)
        {
            if (id != customFeatureRolePermission.CustomFeatureRolePermissionId)
            {
                return BadRequest();
            }

            _context.Entry(customFeatureRolePermission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomFeatureRolePermissionExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<CustomFeatureRolePermission> customFeatureRolePermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Entry(customFeature).State = EntityState.Modified;
            var entity = await _context.CustomFeatureRolePermissions.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            customFeatureRolePermission.Patch(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CustomFeatureRolePermissionExists(key))
                {
                    return BadRequest("Key not found");
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return Updated(entity);
        }
        // POST: api/CustomFeatureRolePermissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomFeatureRolePermission>> PostCustomFeatureRolePermission([FromBody] CustomFeatureRolePermission customFeatureRolePermission)
        {
            try
            {
                _context.CustomFeatureRolePermissions.Add(customFeatureRolePermission);
                await _context.SaveChangesAsync();

                return Ok(customFeatureRolePermission);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // DELETE: api/CustomFeatureRolePermissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomFeatureRolePermission(int id)
        {
            var customFeatureRolePermission = await _context.CustomFeatureRolePermissions.FindAsync(id);
            if (customFeatureRolePermission == null)
            {
                return NotFound();
            }

            _context.CustomFeatureRolePermissions.Remove(customFeatureRolePermission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomFeatureRolePermissionExists(int id)
        {
            return _context.CustomFeatureRolePermissions.Any(e => e.CustomFeatureRolePermissionId == id);
        }
    }
}
