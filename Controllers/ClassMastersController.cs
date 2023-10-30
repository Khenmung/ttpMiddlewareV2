using System;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using Microsoft.AspNetCore.Identity;
using ttpMiddleware.Common;
using Microsoft.Extensions.Configuration;
using ttpMiddleware.Data.Entities;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("ClassMasters")]
    [EnableQuery]
    
    public class ClassMastersController : ProtectedController
    {
        private readonly ttpauthContext _context;
        private readonly IConfiguration _configuration;
        private UserManager<ApplicationUser> _userManager;
        public ClassMastersController(ttpauthContext context,
            IConfiguration config,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _configuration =config;
            _userManager =userManager;
        }

        // GET: api/ClassMasters
        [HttpGet]
        public IQueryable<ClassMaster> GetClassMasters()
        {
            return _context.ClassMasters.AsQueryable().AsNoTracking();
        }
        //[EnableNestedPaths]
        // GET: api/ClassMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassMaster>> GetClassMaster(int id)
        {
            var classMaster = await _context.ClassMasters.FindAsync(id);

            if (classMaster == null)
            {
                return NotFound();
            }

            return classMaster;
        }


        //[HttpPost]    
        //[ODataRoute("dos")]
        //public int DoSth()
        //{
        //    return 22;
        //}
        // PUT: api/ClassMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{key}")]
        public async Task<IActionResult> PatchClassMaster([FromODataUri]int key,[FromBody] ClassMaster classMaster)
        {
            if (key != classMaster.ClassId)
            {
                return BadRequest();
            }

            _context.Entry(classMaster).State = EntityState.Modified;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var EduAppId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("EduAppId").Value);
                var _essentialDataId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "class master").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_essentialDataId == 0)
                {
                    return BadRequest("Table name class master not found.");
                }
                else
                {

                    ////////////////////
                    commonfunctions common = new commonfunctions(_configuration, _userManager, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = classMaster.ClassName;
                    customfeature.OrgId = classMaster.OrgId;
                    customfeature.SubOrgId = classMaster.SubOrgId;
                    customfeature.Active = classMaster.Active == 1 ? true : false;
                    customfeature.ApplicationId = (short)EduAppId;
                    customfeature.TableName = "classmaster";
                    customfeature.TableRowId = (short)classMaster.ClassId;
                    customfeature.TableNameId = _essentialDataId;

                    common.AddToCustomFeature(customfeature, (bool)classMaster.Confidential, "classmaster");
                    ////////////////////
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassMasterExists(key))
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

        // POST: api/ClassMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassMaster>> PostClassMaster([FromBody] ClassMaster classMaster)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.ClassMasters.Add(classMaster);               
                //await _context.SaveChangesAsync();
                
                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var EduAppId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("EduAppId").Value);
                var _essentialDataId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "class master").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_essentialDataId == 0)
                {
                    return BadRequest("Table name class master not found.");
                }
                else
                {

                    ////////////////////
                    commonfunctions common = new commonfunctions(_configuration, _userManager, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = classMaster.ClassName;
                    customfeature.OrgId = classMaster.OrgId;
                    customfeature.SubOrgId = classMaster.SubOrgId;
                    customfeature.Active = classMaster.Active == 1 ? true : false;
                    customfeature.ApplicationId = EduAppId;
                    customfeature.TableName = "classmaster";
                    customfeature.TableRowId = classMaster.ClassId;
                    customfeature.TableNameId = _essentialDataId;

                    common.AddToCustomFeature(customfeature, (bool)classMaster.Confidential, "classmaster");
                    ////////////////////
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                return Ok(classMaster);
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();
                throw;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        // DELETE: api/ClassMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassMaster(int id)
        {
            var classMaster = await _context.ClassMasters.FindAsync(id);
            if (classMaster == null)
            {
                return NotFound();
            }

            _context.ClassMasters.Remove(classMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassMasterExists(int id)
        {
            return _context.ClassMasters.Any(e => e.ClassId == id);
        }
    }
}
