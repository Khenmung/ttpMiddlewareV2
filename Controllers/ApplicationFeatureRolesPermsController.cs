using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using System;
using Microsoft.Extensions.Configuration;

using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("ApplicationFeatureRolesPerms")]
    [EnableQuery]
    public class ApplicationFeatureRolesPermsController : ProtectedController
    {
        private readonly ttpauthContext _context;
        private readonly IConfiguration _configuration;


        public ApplicationFeatureRolesPermsController(ttpauthContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/ApplicationFeatureRolesPerms
        [HttpGet]
        public IQueryable<ApplicationFeatureRolesPerm> GetApplicationFeatureRolesPerms()
        {
            return _context.ApplicationFeatureRolesPerms.AsQueryable().AsNoTracking();
        }


        /// <summary>
        /// Patch data
        /// </summary>
        /// <param name="key"></param>
        /// <param name="applicationFeatureRolesPerm"></param>
        /// <returns></returns>
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ApplicationFeatureRolesPerm> applicationFeatureRolesPerm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entity = await _context.ApplicationFeatureRolesPerms.FindAsync(key);
                if (entity == null)
                {
                    return NotFound();
                }
                applicationFeatureRolesPerm.Patch(entity);
                var _adminRoleId = _context.MasterItems.Where(x => x.MasterDataName == "Admin"
                && x.OrgId == entity.OrgId
                && x.SubOrgId == entity.SubOrgId).Select(s => s.MasterDataId).FirstOrDefault();

                // if the setting is for admin, replicate for all users in that organization.
                if (_adminRoleId == entity.RoleId)
                {
                    var allperm = await _context.ApplicationFeatureRolesPerms.Where(x => x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId
                    && x.PlanFeatureId == entity.PlanFeatureId).ToListAsync();
                    if (allperm.Count() > 0)
                    {
                        foreach (var perm in allperm)
                        {
                            perm.Active = entity.Active;
                            _context.Update(perm);
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return Updated(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationFeatureRolesPermExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        // PUT: api/ApplicationFeatureRolesPerms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationFeatureRolesPerm>> PutApplicationFeatureRolesPerm(int id, ApplicationFeatureRolesPerm applicationFeatureRolesPerm)
        {
            if (id != applicationFeatureRolesPerm.ApplicationFeatureRoleId)
            {
                return BadRequest();
            }

            _context.Entry(applicationFeatureRolesPerm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationFeatureRolesPermExists(id))
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
        private async void AddCommonAppPermission(ApplicationFeatureRolesPerm currentPerm)
        {
            var commonappId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("CommonAppId").Value);
            var readonlyPermissionId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig").GetSection("read").Value);
            var CommonAppInitialSettingId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig")
                .GetSection("CommonAppInitialSettingId").Value);

            var _commonInitialSettingPlanFeatureId = await _context.PlanFeatures.Where(x => x.ApplicationId == commonappId
            && x.PageId == CommonAppInitialSettingId
            && x.PlanId == currentPerm.PlanId
            && x.Active == 1).Select(s => s.PlanFeatureId).FirstOrDefaultAsync();

            if (_commonInitialSettingPlanFeatureId > 0)
            {
                //var _planFeatureId = await _context.PlanFeatures.Where(x => x.PageId == _commonInitialSettingPlanFeatureId && x.Active == 1)
                //    .Select(s => s.PlanFeatureId)
                //    .FirstOrDefaultAsync();

                var check = await _context.ApplicationFeatureRolesPerms.Where(x => x.PlanFeatureId == _commonInitialSettingPlanFeatureId
                && x.RoleId == currentPerm.RoleId
                && x.OrgId == currentPerm.OrgId
                && x.SubOrgId == currentPerm.SubOrgId
                && x.PlanId == currentPerm.PlanId
                && x.Active == 1).ToListAsync();
                if (check.Count == 0)
                {
                    var newcommonAppPerm = new ApplicationFeatureRolesPerm()
                    {
                        ApplicationFeatureRoleId = 0,
                        Active = 1,
                        PlanId = currentPerm.PlanId,
                        OrgId = currentPerm.OrgId,
                        SubOrgId = currentPerm.SubOrgId,
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        PermissionId = readonlyPermissionId,
                        PlanFeatureId = _commonInitialSettingPlanFeatureId,
                        RoleId = currentPerm.RoleId

                    };
                    _context.ApplicationFeatureRolesPerms.Add(newcommonAppPerm);
                }
            }
        }
        // POST: api/ApplicationFeatureRolesPerms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationFeatureRolesPerm>> PostApplicationFeatureRolesPerm([FromBody] ApplicationFeatureRolesPerm applicationFeatureRolesPerm)
        {
            var tran = _context.Database.BeginTransaction();
            try
            {


                _context.ApplicationFeatureRolesPerms.Add(applicationFeatureRolesPerm);
                //this.AddCommonAppPermission(applicationFeatureRolesPerm);

                //////////////
                var commonappId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("CommonAppId").Value);
                var readonlyPermissionId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig").GetSection("read").Value);
                var CommonAppInitialSettingId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig")
                    .GetSection("CommonAppInitialSettingId").Value);

                var _commonInitialSettingPlanFeatureId = await _context.PlanFeatures.Where(x => x.ApplicationId == commonappId
                && x.PageId == CommonAppInitialSettingId
                && x.PlanId == applicationFeatureRolesPerm.PlanId
                && x.Active == 1).Select(s => s.PlanFeatureId).FirstOrDefaultAsync();

                if (_commonInitialSettingPlanFeatureId > 0)
                {
                    //var _planFeatureId = await _context.PlanFeatures.Where(x => x.PageId == _commonInitialSettingPlanFeatureId && x.Active == 1)
                    //    .Select(s => s.PlanFeatureId)
                    //    .FirstOrDefaultAsync();

                    var check = await _context.ApplicationFeatureRolesPerms.Where(x => x.PlanFeatureId == _commonInitialSettingPlanFeatureId
                    && x.RoleId == applicationFeatureRolesPerm.RoleId
                    && x.OrgId == applicationFeatureRolesPerm.OrgId
                    && x.SubOrgId == applicationFeatureRolesPerm.SubOrgId).FirstOrDefaultAsync();
                    //&& x.Active == 1
                    if (check == null) 
                    {
                        var newcommonAppPerm = new ApplicationFeatureRolesPerm()
                        {
                            ApplicationFeatureRoleId = 0,
                            Active = 1,
                            OrgId = applicationFeatureRolesPerm.OrgId,
                            SubOrgId = applicationFeatureRolesPerm.SubOrgId,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            PlanId= applicationFeatureRolesPerm.PlanId,
                            CreatedBy= applicationFeatureRolesPerm.CreatedBy,
                            Deleted = false,
                            PermissionId = readonlyPermissionId,
                            PlanFeatureId = _commonInitialSettingPlanFeatureId,
                            RoleId = applicationFeatureRolesPerm.RoleId

                        };
                        _context.ApplicationFeatureRolesPerms.Add(newcommonAppPerm);
                    }
                    else
                    {
                        check.Active = 1;
                        _context.Update(check);
                    }
                }
                ////////////////////////

                await _context.SaveChangesAsync();
                tran.Commit();
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(applicationFeatureRolesPerm);
        }

        // DELETE: api/ApplicationFeatureRolesPerms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationFeatureRolesPerm(int id)
        {
            var applicationFeatureRolesPerm = await _context.ApplicationFeatureRolesPerms.FindAsync(id);
            if (applicationFeatureRolesPerm == null)
            {
                return NotFound();
            }

            _context.ApplicationFeatureRolesPerms.Remove(applicationFeatureRolesPerm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationFeatureRolesPermExists(int id)
        {
            return _context.ApplicationFeatureRolesPerms.Any(e => e.ApplicationFeatureRoleId == id);
        }
    }
}
