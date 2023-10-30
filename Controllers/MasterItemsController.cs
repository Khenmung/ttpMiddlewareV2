using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using System;
using System.Collections.Generic;
using ttpMiddleware.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using ttpMiddleware.Data.Entities;

using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("MasterItems")]
    //[Authorize]
    [EnableQuery]
    public class MasterItemsController : ProtectedController
    {
        private readonly ttpauthContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        //private readonly commonfunctions _common;

        public MasterItemsController(
                ttpauthContext context,
                UserManager<ApplicationUser> userManager,
                IConfiguration configuration

            //commonfunctions common
            )
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            //_common = common;
        }

        //GET: api/MasterItems
        [HttpGet]
        //[ExtendedEnableQuery]
        public IQueryable<MasterItem> Get()
        {
            return _context.MasterItems.AsNoTracking().AsQueryable();
        }

        // GET: api/MasterItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterItem>> Get(int id)
        {
            var masterItem = await _context.MasterItems.FindAsync(id);

            if (masterItem == null)
            {
                return NotFound();
            }

            return masterItem;
        }
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<MasterItem> masterItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.MasterItems.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            masterItem.Patch(entity);
            using var tran = _context.Database.BeginTransaction();
            try
            {
                entity.Confidential = entity.Confidential == null ? false : entity.Confidential;
                await _context.SaveChangesAsync();
                //var planAndMaster = await _context.PlanAndMasterItems.Where(x => x.MasterDataId == entity.MasterDataId).ToListAsync();
                var subjectParent = await _context.MasterItems.Where(x => x.MasterDataId == entity.ParentId
                && x.MasterDataName.ToLower() == "subject").ToListAsync();

                if (subjectParent.Count > 0)
                {
                    var _classSubject = await _context.ClassSubjects.Where(x => x.SubjectId == entity.MasterDataId).ToListAsync();
                    foreach (var item in _classSubject)
                    {
                        item.Deleted = entity.Deleted;
                        item.Active = (byte)entity.Active;
                        _context.ClassSubjects.Update(item);
                    }
                    //_context.SaveChanges();
                }

                var examParent = await _context.MasterItems.Where(x => x.MasterDataId == entity.ParentId
                && x.MasterDataName.ToLower() == "exam name").ToListAsync();

                if (examParent.Count > 0)
                {
                    var _exams = await _context.Exams.Where(x => x.ExamId == entity.MasterDataId).ToListAsync();
                    foreach (var item in _exams)
                    {
                        item.Deleted = entity.Deleted;
                        item.Active = (byte)entity.Active;
                        _context.Exams.Update(item);
                    }
                    _context.SaveChanges();
                }
                var childrenItem = await _context.MasterItems.Where(x => x.ParentId == entity.MasterDataId).ToListAsync();
                await recursiveMasterItemUpdate(childrenItem, entity);

                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var _essentialDataId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "essential data").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_essentialDataId == 0)
                {
                    return BadRequest("Table name Essential Data not found.");
                }
                else
                {

                    ////////////////////
                    commonfunctions common = new commonfunctions(_configuration, _userManager, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = entity.MasterDataName;
                    customfeature.OrgId = entity.OrgId;
                    customfeature.SubOrgId = entity.SubOrgId;
                    customfeature.Active = entity.Active == 1 ? true : false;
                    customfeature.ApplicationId = (short)entity.ApplicationId;
                    customfeature.TableName = "masteritems";
                    customfeature.TableRowId = (short)entity.MasterDataId;
                    customfeature.TableNameId = _essentialDataId;

                    common.AddToCustomFeature(customfeature, (bool)entity.Confidential, "masteritems");
                    ////////////////////
                }
                await _context.SaveChangesAsync();
                tran.Commit();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                tran.Rollback();
                //var res =_common.LogError("masteritem update", ex.InnerException.Message, entity.UpdatedBy, entity.OrgId);
                if (!MasterItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Updated(entity);
        }
        public void AddToCustomFeature(CustomFeature customFeature, bool pConfidential, string tableName)
        {
            try
            {
                var existingCustomerFeature = _context.CustomFeatures.Where(x => x.TableName.ToLower() == tableName.ToLower()
                   && x.TableRowId == customFeature.TableRowId
                   && x.OrgId == customFeature.OrgId
                   && x.SubOrgId == customFeature.SubOrgId
                   ).Select(s => s).ToList();
                if (existingCustomerFeature.Count > 0)
                {
                    foreach (var item in existingCustomerFeature)
                    {
                        item.CustomFeatureName = customFeature.CustomFeatureName;
                        item.Active = pConfidential;
                        _context.CustomFeatures.Update(item);

                    }
                    _context.SaveChanges();
                }
                else if (pConfidential == true)
                {
                    var dupCustomerFeature = _context.CustomFeatures.Where(x => x.CustomFeatureName.ToLower() == customFeature.CustomFeatureName.ToLower()
                            && x.OrgId == customFeature.OrgId
                            && x.SubOrgId == customFeature.SubOrgId
                            //&& x.TableId == tableName
                            && x.ApplicationId == customFeature.ApplicationId).Select(s => s).ToList();
                    if (dupCustomerFeature.Count == 0)
                    {
                        var _customerFeature = new CustomFeature()
                        {
                            CustomFeatureId = 0,
                            CustomFeatureName = customFeature.CustomFeatureName,
                            TableName = tableName,
                            TableRowId = customFeature.TableRowId,
                            OrgId = customFeature.OrgId,
                            SubOrgId = customFeature.SubOrgId,
                            Active = customFeature.Active,
                            Deleted = false,
                            CreatedDate = DateTime.Now
                        };
                        _context.CustomFeatures.Add(_customerFeature);
                        _context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("duplicate custom feature found: " + customFeature.CustomFeatureName);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //private async Task recursivePlanAndMasterUpdate(List<PlanAndMasterItem> planAndMaster, MasterItem parent)
        //{
        //    //if not exist add it and active will be as parent.
        //    if (planAndMaster.Count == 0)
        //    {
        //        var newPlanMasterItem = new PlanAndMasterItem()
        //        {
        //            PlanAndMasterDataId = 0,
        //            PlanId = parent.Pl
        //            MasterDataId = parent.MasterDataId,
        //            Active = (byte)parent.Active,
        //            ApplicationId = (short)parent.ApplicationId
        //        };
        //        _context.PlanAndMasterItems.Add(newPlanMasterItem);
        //    }
        //    else
        //    {
        //        foreach (var item in planAndMaster)
        //        {
        //            item.Active = (byte)parent.Active;
        //            _context.PlanAndMasterItems.Update(item);
        //            var childrenItem = await _context.MasterItems.Where(x => x.ParentId == parent.MasterDataId).ToListAsync();
        //            foreach (var child in childrenItem)
        //            {
        //                child.Active = parent.Active;
        //                var childrenPlanAndMaster = await _context.PlanAndMasterItems.Where(x => x.MasterDataId == child.MasterDataId).ToListAsync();
        //                await recursivePlanAndMasterUpdate(childrenPlanAndMaster, child);
        //            }
        //        }
        //    }
        //    await _context.SaveChangesAsync();
        //}
        private async Task recursiveMasterItemUpdate(List<MasterItem> masterItem, MasterItem parent)
        {

            foreach (var item in masterItem)
            {
                item.Active = (byte)parent.Active;
                _context.MasterItems.Update(item);
                var childrenPlanAndMaster = await _context.PlanAndMasterItems.Where(x => x.MasterDataId == item.MasterDataId).ToListAsync();

                foreach (var planmaster in childrenPlanAndMaster)
                {
                    planmaster.UpdatedDate = DateTime.Now;
                    planmaster.Active = (byte)item.Active;
                    _context.PlanAndMasterItems.Update(planmaster);
                }

                var childrenItem = await _context.MasterItems.Where(x => x.ParentId == item.MasterDataId).ToListAsync();

                await recursiveMasterItemUpdate(childrenItem, item);

            }
            await _context.SaveChangesAsync();

        }
        // PUT: api/MasterItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MasterItem masterItem)
        {
            if (id != masterItem.MasterDataId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(masterItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterItemExists(id))
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

        // POST: api/MasterItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MasterItem>> Post([FromBody] MasterItem masterItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Incomplete data");
            }
            if (masterItem.OrgId == 0)
            {
                return BadRequest("Org Id must be defined");
            }
            using var tran = _context.Database.BeginTransaction();
            try
            {
                var dup = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == masterItem.MasterDataName.ToLower()
                 && x.OrgId == masterItem.OrgId
                 && x.SubOrgId == masterItem.SubOrgId
                 && x.ParentId == masterItem.ParentId).ToListAsync();
                if (dup.Count > 0)
                {
                    return BadRequest("Duplicate Data");
                }
                _context.MasterItems.Add(masterItem);
                await _context.SaveChangesAsync();

                ////////////////////
                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var _essentialDataId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "essential data").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_essentialDataId == 0)
                {
                    return BadRequest("Table name Essential Data not found.");
                }
                else
                {
                    commonfunctions common = new commonfunctions(_configuration, _userManager, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = masterItem.MasterDataName;
                    customfeature.OrgId = masterItem.OrgId;
                    customfeature.SubOrgId = masterItem.SubOrgId;
                    customfeature.Active = masterItem.Active == 1 ? true : false;
                    customfeature.ApplicationId = (int)masterItem.ApplicationId;
                    customfeature.TableName = "masteritems";
                    customfeature.TableRowId = masterItem.MasterDataId;
                    customfeature.TableNameId = _essentialDataId;

                    common.AddToCustomFeature(customfeature, (bool)masterItem.Confidential, "masteritems");
                }
                ////////////////////



                //when a new item is added, that item is added to planandmasteritem with the same planId as its parent.
                var planAndMaster = await _context.PlanAndMasterItems.Where(x => x.MasterDataId == masterItem.ParentId).ToListAsync();
                foreach (var item in planAndMaster)
                {
                    var newPlanAndMaster = new PlanAndMasterItem()
                    {
                        PlanAndMasterDataId = 0,
                        MasterDataId = masterItem.MasterDataId,
                        PlanId = item.PlanId,
                        Active = 1,
                        ApplicationId = (short)masterItem.ApplicationId,
                        CreatedDate = DateTime.Now
                    };
                    _context.PlanAndMasterItems.Add(newPlanAndMaster);
                    await _context.SaveChangesAsync();
                }

                tran.Commit();
                return Ok(masterItem);

            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw;
            }
        }

        // DELETE: api/MasterItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var masterItem = await _context.MasterItems.FindAsync(id);
            if (masterItem == null)
            {
                return NotFound();
            }

            _context.MasterItems.Remove(masterItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MasterItemExists(int id)
        {
            return _context.MasterItems.Any(e => e.MasterDataId == id);
        }
    }
}
