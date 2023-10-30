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
using ttpMiddleware.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ttpMiddleware.Data.Entities;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class EvaluationMastersController : ProtectedController
    {
        private readonly ttpauthContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        public EvaluationMastersController(ttpauthContext context,
            UserManager<ApplicationUser> userManager,
                IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;

        }

        // GET: api/EvaluationNames
        [HttpGet]
        public IQueryable<EvaluationMaster> GetEvaluationMasters()
        {
            return _context.EvaluationMasters.AsQueryable().AsNoTracking();
        }

        // GET: api/EvaluationNames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationMaster>> GetEvaluationMaster(int id)
        {
            var evaluationMaster = await _context.EvaluationMasters.FindAsync(id);

            if (evaluationMaster == null)
            {
                return NotFound();
            }

            return evaluationMaster;
        }

        // PUT: api/EvaluationNames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutevaluationMaster(int id, EvaluationMaster evaluationMaster)
        {
            if (id != evaluationMaster.EvaluationMasterId)
            {
                return BadRequest();
            }

            _context.Entry(evaluationMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationMasterExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EvaluationMaster> evaluationMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EvaluationMasters.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            evaluationMaster.Patch(entity);
            using var tran = _context.Database.BeginTransaction();
            try
            {
                
                await _context.SaveChangesAsync();
                ////////////////////
                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var _evaluationMasterTableId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "evaluation master").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_evaluationMasterTableId == 0)
                {
                    return BadRequest("Table name evaluation master not found.");
                }
                else
                {
                    var EduAppId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("EduAppId").Value);
                    commonfunctions common = new commonfunctions(_configuration, _userManager, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = entity.EvaluationName;
                    customfeature.OrgId = entity.OrgId;
                    customfeature.SubOrgId = entity.SubOrgId;
                    customfeature.Active = entity.Active;
                    customfeature.ApplicationId = (short)EduAppId;
                    customfeature.TableName = "evaluationmaster";
                    customfeature.TableRowId = (short)entity.EvaluationMasterId;
                    customfeature.TableNameId = _evaluationMasterTableId;

                    common.AddToCustomFeature(customfeature, (bool)entity.Confidential, customfeature.TableName);
                    tran.Commit();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                tran.Rollback();
                if (!EvaluationMasterExists(key))
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
        // POST: api/EvaluationNames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvaluationMaster>> PostEvaluationMaster([FromBody] EvaluationMaster evaluationMaster)
        {
            _context.EvaluationMasters.Add(evaluationMaster);
            try
            {
                using var tran = _context.Database.BeginTransaction();
                await _context.SaveChangesAsync();
                ////////////////////
                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var _evaluationMasterTableId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "evaluation master").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_evaluationMasterTableId == 0)
                {
                    return BadRequest("Table name evaluation master not found.");
                }
                else
                {
                    var EduAppId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig").GetSection("EduAppId").Value);
                    commonfunctions common = new commonfunctions(_configuration, _userManager, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = evaluationMaster.EvaluationName;
                    customfeature.OrgId = evaluationMaster.OrgId;
                    customfeature.SubOrgId = evaluationMaster.SubOrgId;
                    customfeature.Active = evaluationMaster.Active;
                    customfeature.ApplicationId = EduAppId;
                    customfeature.TableName = "evaluationmaster";
                    customfeature.TableRowId = evaluationMaster.EvaluationMasterId;
                    customfeature.TableNameId = _evaluationMasterTableId;

                    common.AddToCustomFeature(customfeature, (bool)evaluationMaster.Confidential, customfeature.TableName);
                    tran.Commit();
                }
            }
            catch (DbUpdateException)
            {
                if (EvaluationMasterExists(evaluationMaster.EvaluationMasterId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(evaluationMaster);
        }

        // DELETE: api/EvaluationNames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluationName(int id)
        {
            var evaluationName = await _context.EvaluationNames.FindAsync(id);
            if (evaluationName == null)
            {
                return NotFound();
            }

            _context.EvaluationNames.Remove(evaluationName);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluationMasterExists(int id)
        {
            return _context.EvaluationMasters.Any(e => e.EvaluationMasterId == id);
        }
    }
}
