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
using ttpMiddleware.Models.DTOs.Responses;

using ttpMiddleware.CommonFunctions;
using Microsoft.Extensions.Configuration;
using ttpMiddleware.Common;
using Microsoft.AspNetCore.Identity;
using ttpMiddleware.Data.Entities;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class SchoolFeeTypesController : ProtectedController
    {
        private readonly ttpauthContext _context;
        private readonly IConfiguration _configuration;
        private UserManager<ApplicationUser> _userManager;

        public SchoolFeeTypesController(ttpauthContext context,IConfiguration configuration,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager; 
        }

        // GET: api/SchoolFeeTypes
        [HttpGet]
        public IQueryable<SchoolFeeType> GetSchoolFeeTypes()
        {
            return _context.SchoolFeeTypes.AsQueryable().AsNoTracking();
        }

        // GET: api/SchoolFeeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolFeeType>> GetSchoolFeeType(short id)
        {
            var schoolFeeType = await _context.SchoolFeeTypes.FindAsync(id);

            if (schoolFeeType == null)
            {
                return NotFound();
            }

            return schoolFeeType;
        }

        // PUT: api/SchoolFeeTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolFeeType(short id, SchoolFeeType schoolFeeType)
        {
            if (id != schoolFeeType.FeeTypeId)
            {
                return BadRequest();
            }

            _context.Entry(schoolFeeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolFeeTypeExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<SchoolFeeType> schoolFeeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.SchoolFeeTypes.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            schoolFeeType.Patch(entity);
            var tran = _context.Database.BeginTransaction();
            try
            {
                if (entity.DefaultType == 1)
                {
                    //making all others defaulttype =false
                    var feetypes = await _context.SchoolFeeTypes.Where(x => x.FeeTypeId != entity.FeeTypeId
                    && x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId
                    ).ToListAsync();
                    foreach (var item in feetypes)
                    {
                        item.DefaultType = 0;
                        _context.SchoolFeeTypes.Update(item);
                    }
                }
                else
                {
                    var feetypes = await _context.SchoolFeeTypes.Where(x => x.DefaultType == 1
                    && x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId
                    ).ToListAsync();
                    if (feetypes.Count == 0)
                    {
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                                "There must be atleast one default type."
                            },
                            Success = false
                        });
                    }
                }
                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var EduAppId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("EduAppId").Value);
                var _feeTypeId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "fee type").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_feeTypeId == 0)
                {
                    return BadRequest("Table name fee type not found.");
                }
                else
                {

                    ////////////////////
                    commonfunctions common = new commonfunctions(_configuration, _userManager, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = entity.FeeTypeName;
                    customfeature.OrgId = entity.OrgId;
                    customfeature.SubOrgId = entity.SubOrgId;
                    customfeature.Active = entity.Active == 1 ? true : false;
                    customfeature.ApplicationId = (short)EduAppId;
                    customfeature.TableName = "feetype";
                    customfeature.TableRowId = entity.FeeTypeId;
                    customfeature.TableNameId = _feeTypeId;

                    common.AddToCustomFeature(customfeature, entity.Confidential, "feetype");
                    ////////////////////
                    //await _context.SaveChangesAsync();
                   
                }
                await _context.SaveChangesAsync();
                tran.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                tran.Rollback();
                if (!SchoolFeeTypeExists(key))
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
        
        // POST: api/SchoolFeeTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SchoolFeeType>> PostSchoolFeeType([FromBody] SchoolFeeType schoolFeeType)
        {
            var tran = _context.Database.BeginTransaction();
            try
            {
                _context.SchoolFeeTypes.Add(schoolFeeType);

                if(schoolFeeType.DefaultType==1)
                {
                    //making all others defaulttype =false
                    var feetypes = await _context.SchoolFeeTypes.Where(x => x.FeeTypeId != schoolFeeType.FeeTypeId
                    && x.OrgId == schoolFeeType.OrgId
                    && x.SubOrgId == schoolFeeType.SubOrgId
                    ).ToListAsync();
                    foreach(var item in feetypes)
                    {
                        item.DefaultType = 0;
                        _context.SchoolFeeTypes.Update(item);
                    }
                }
                else
                {
                    var feetypes = await _context.SchoolFeeTypes.Where(x => x.DefaultType==1
                    && x.OrgId == schoolFeeType.OrgId
                    && x.SubOrgId == schoolFeeType.SubOrgId
                    ).ToListAsync();
                    if(feetypes.Count==0)
                    {
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                                "There must be atleast one default type."
                            },
                            Success = false
                        });
                    }
                }
                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var EduAppId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("EduAppId").Value);
                var _feeTypeId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "fee type").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_feeTypeId == 0)
                {
                    return BadRequest("Table name fee type not found.");
                }
                else
                {
                    ////////////////////
                    commonfunctions common = new commonfunctions(_configuration, _userManager, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = schoolFeeType.FeeTypeName;
                    customfeature.OrgId = schoolFeeType.OrgId;
                    customfeature.SubOrgId = schoolFeeType.SubOrgId;
                    customfeature.Active = schoolFeeType.Active == 1 ? true : false;
                    customfeature.ApplicationId = (short)EduAppId;
                    customfeature.TableName = "feetype";
                    customfeature.TableRowId = schoolFeeType.FeeTypeId;
                    customfeature.TableNameId = _feeTypeId;

                    common.AddToCustomFeature(customfeature, schoolFeeType.Confidential, "feetype");
                    await _context.SaveChangesAsync();
                }
                tran.Commit();
                return Ok(schoolFeeType);
            }
            catch(Exception ex)
            {
                tran.Rollback();
                throw;
            }
               
        }

        // DELETE: api/SchoolFeeTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolFeeType(short id)
        {
            var schoolFeeType = await _context.SchoolFeeTypes.FindAsync(id);
            if (schoolFeeType == null)
            {
                return NotFound();
            }

            _context.SchoolFeeTypes.Remove(schoolFeeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolFeeTypeExists(short id)
        {
            return _context.SchoolFeeTypes.Any(e => e.FeeTypeId == id);
        }
    }
}
