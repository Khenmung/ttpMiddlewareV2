using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Data.Entities;
using ttpMiddleware.Models;
using ttpMiddleware.Models.DTOs.Responses;

using ttpMiddleware.CommonFunctions;
using Microsoft.Extensions.Configuration;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class CustomerPlansController : ProtectedController
    {
        private readonly ttpauthContext _context;
        private UserManager<ApplicationUser> _userManager;
        IConfiguration _configuration;
        public CustomerPlansController(ttpauthContext context,
             UserManager<ApplicationUser> userManager,
              IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        // GET: api/CustomerPlans
        [HttpGet]
        public IQueryable<CustomerPlan> GetCustomerPlans()
        {
            return _context.CustomerPlans.AsQueryable().AsNoTracking();
        }

        // GET: api/CustomerPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPlan>> GetCustomerPlan(short id)
        {
            var customerPlan = await _context.CustomerPlans.FindAsync(id);

            if (customerPlan == null)
            {
                return NotFound();
            }

            return customerPlan;
        }

        // PUT: api/CustomerPlans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerPlan(short id, CustomerPlan customerPlan)
        {
            if (id != customerPlan.CustomerPlanId)
            {
                return BadRequest();
            }

            _context.Entry(customerPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPlanExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<CustomerPlan> customerPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.CustomerPlans.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            customerPlan.Patch(entity);
            using var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();

                var ttpOrgId = await _context.Organizations.Where(x => x.OrganizationName.ToUpper() == "TTP")
                        .Select(s => s.OrganizationId).ToListAsync();

                //update all features as per the updated active value;
                var planFeatureIds = await _context.PlanFeatures.Where(x =>
                x.Active == 1
                && x.PlanId == entity.PlanId
                && x.Deleted == false)
                    .Select(s => s.PlanFeatureId).ToListAsync();

                foreach (var planfeatureId in planFeatureIds)
                {
                    var existing = await _context.ApplicationFeatureRolesPerms.Where(x => x.PlanFeatureId == planfeatureId
                    && x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId
                    && x.PlanId == entity.PlanId
                    && x.Deleted == false).ToListAsync();
                    if (existing.Count() > 0)
                    {
                        foreach (var item in existing)
                        {
                            item.Active = entity.Active;
                            _context.ApplicationFeatureRolesPerms.Update(item);
                        }
                        //_context.ApplicationFeatureRolesPerms.Add(appfeatureRolePer);
                    }
                }
                if (entity.Active == 1)
                {
                    var otherPlans = await _context.CustomerPlans.Where(x =>
                    x.CustomerPlanId != entity.CustomerPlanId
                    && x.OrgId == entity.OrgId
                    && x.Deleted == false).ToListAsync();

                    foreach (var item in otherPlans)
                    {
                        item.Active = 0;
                        _context.CustomerPlans.Update(item);
                    }
                    //await _context.SaveChangesAsync();

                    var permittedApplicationIds = await _context.PlanFeatures
                        .Join(_context.Pages, p => p.PageId, f => f.PageId, (p, f) => new { p, f })
                        .Where(x => x.p.PlanId == entity.PlanId && x.p.Active == 1 && x.p.Deleted == false)
                        .Select(s => s.p.ApplicationId)
                        .Distinct().ToListAsync();


                    var defaultMasters = await _context.MasterItems.Where(x => x.OrgId == ttpOrgId[0]
                    && x.Active == 1
                    && x.Deleted == false
                    && permittedApplicationIds.Contains(x.ApplicationId))
                        .OrderBy(o => o.ParentId)
                        .ToListAsync();

                    //foreach (var item in defaultMasters)
                    //{
                    //    var existing = await _context.MasterItems.Where(x => x.MasterDataName == item.MasterDataName
                    //    && x.OrgId == entity.OrgId && x.Deleted == false).ToListAsync();
                    //    if (existing.Count == 0)
                    //    {
                    //        item.MasterDataId = 0;
                    //        item.OrgId = entity.OrgId;
                    //        item.CreatedDate = DateTime.Now;
                    //        _context.MasterItems.Add(item);
                    //    }
                    //}
                    foreach (var item in defaultMasters)
                    {
                        var existing = await _context.MasterItems.Where(x => x.MasterDataName == item.MasterDataName
                        && x.OrgId == entity.OrgId
                        && x.SubOrgId == entity.SubOrgId
                        && x.Deleted == false).ToListAsync();

                        if (existing.Count() == 0)
                        {
                            //check if parent of current item is in orgId =1
                            var parentData = await _context.MasterItems.Where(x => x.MasterDataId == item.ParentId
                                                                            && x.OrgId == ttpOrgId[0]
                                                                            && x.Deleted == false).ToListAsync();
                            //if yes
                            if (parentData.Count() > 0)
                            {
                                //if ParentId of ParentItem is zero, that is first level of masteritem (children of parentId =0).
                                //In this case, we will insert the item as it is.  
                                //check if ParentId of Parent item is not zero.
                                if (parentData[0].ParentId > 0)
                                {
                                    //if yes, parentId should not be masterdataId of orgId=1. It should be of current organization Id.
                                    var _shouldbeParentId = await _context.MasterItems.Where(x => x.MasterDataName == parentData[0].MasterDataName
                                     && x.OrgId == entity.OrgId
                                     && x.SubOrgId == entity.SubOrgId
                                     && x.Deleted == false).ToListAsync();

                                    item.ParentId = _shouldbeParentId[0].MasterDataId;
                                }
                            }
                            item.MasterDataId = 0;
                            item.OrgId = entity.OrgId;
                            item.SubOrgId = entity.SubOrgId;
                            item.CreatedDate = DateTime.Now;
                            _context.MasterItems.Add(item);
                            //need to do savechanges since we need to reference the ids in the next insert;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var parentData = await _context.MasterItems.Where(x => x.MasterDataId == existing[0].ParentId
                                                                            && x.OrgId == ttpOrgId[0]
                                                                            && x.Deleted == false).ToListAsync();
                            if (parentData.Count() > 0)
                            {
                                if (parentData[0].ParentId > 0)
                                {
                                    var _shouldbeParentId = await _context.MasterItems.Where(x => x.MasterDataName == parentData[0].MasterDataName
                                     && x.OrgId == entity.OrgId
                                     && x.SubOrgId == entity.SubOrgId
                                     && x.Deleted == false).ToListAsync();

                                    existing[0].ParentId = _shouldbeParentId[0].MasterDataId;
                                }
                                _context.MasterItems.Update(existing[0]);
                                await _context.SaveChangesAsync();
                            }

                        }
                    }

                }
                else
                {
                    var otherPlans = await _context.CustomerPlans.Where(x => x.OrgId == entity.OrgId
                    && x.Active == 1 && x.CustomerPlanId != entity.CustomerPlanId
                    && x.Deleted == false).ToListAsync();

                    if (otherPlans.Count == 0)
                    {
                        tran.Rollback();
                        return BadRequest(new RegistrationResponse()
                        {
                            Errors = new List<string>() {
                            "There must be atleast one active plan."
                        },
                            Success = false
                        });

                    }
                }

                //adding TTP's masters for new company.
                var _classes = await _context.ClassMasters.Where(x => x.OrgId == ttpOrgId[0] && x.Active == 1).ToListAsync();
                var _existingclasses = await _context.ClassMasters.Where(x =>
                x.OrgId == entity.OrgId
                && x.SubOrgId == entity.SubOrgId
                && x.Active == 1).ToListAsync();
                if (_existingclasses.Count == 0)
                {
                    foreach (var item in _classes)
                    {
                        item.ClassId = 0;
                        item.OrgId = entity.OrgId;
                        item.SubOrgId = entity.SubOrgId;
                        item.StudyAreaId = 0;
                        item.StudyModeId = 0;
                        item.BatchId = 0;
                        item.Active = 0;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedBy = entity.CreatedBy;
                        _context.ClassMasters.Add(item);
                    }
                }

                var _feedefinitions = await _context.FeeDefinitions.Where(x => x.OrgId == ttpOrgId[0] && x.Active == 1).ToListAsync();
                var _existingfeedefinitions = await _context.FeeDefinitions.Where(x =>
                x.OrgId == entity.OrgId
                && x.SubOrgId == entity.SubOrgId
                && x.Active == 1).ToListAsync();
                if (_existingfeedefinitions.Count == 0)
                {
                    foreach (var item in _feedefinitions)
                    {
                        item.FeeDefinitionId = 0;
                        item.OrgId = entity.OrgId;
                        item.SubOrgId = entity.SubOrgId;
                        item.FeeCategoryId = 0;
                        item.BatchId = 0;
                        item.Active = 0;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedBy = entity.CreatedBy;
                        _context.FeeDefinitions.Add(item);
                    }
                }
                var _feetype = await _context.SchoolFeeTypes.Where(x => x.OrgId == ttpOrgId[0] && x.Active == 1).ToListAsync();
                var _existingfeetype = await _context.SchoolFeeTypes.Where(x =>
                x.OrgId == entity.OrgId
                && x.SubOrgId == entity.SubOrgId
                && x.Active == 1).ToListAsync();
                if (_existingfeetype.Count == 0)
                {
                    foreach (var item in _feetype)
                    {
                        item.FeeTypeId = 0;
                        item.OrgId = entity.OrgId;
                        item.SubOrgId = entity.SubOrgId;
                        item.BatchId = 0;
                        item.Active = 0;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedBy = entity.CreatedBy;
                        _context.SchoolFeeTypes.Add(item);
                    }
                }
                //var _classFee= await _context.ClassFees.Where(x => x.OrgId == ttpOrgId[0] && x.Active == 1).ToListAsync();
                //foreach (var item in _feeDefinition)
                //{
                //    item.OrgId = entity.OrgId;
                //    _context.FeeDefinitions.Add(item);
                //}

                await _context.SaveChangesAsync();
                tran.Commit();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                tran.Rollback();

                return BadRequest(ex);
            }

            return Updated(entity);
        }
        // POST: api/CustomerPlans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerPlan>> PostCustomerPlan([FromBody] CustomerPlan customerPlan)
        {
            var subOrg = await _context.MasterItems.Where(x => x.MasterDataId == customerPlan.SubOrgId).FirstOrDefaultAsync();
            // .Select(s => new { ParentId = s.ParentId, MasterDataId = s.MasterDataId, s.MasterDataName }).FirstOrDefaultAsync();
            if (subOrg == null) return BadRequest("Invalid SubOrgId");


            using var tran = _context.Database.BeginTransaction();
            try
            {


                if (subOrg.MasterDataName.ToLower() == "primary")
                    _context.CustomerPlans.Add(customerPlan);
                _context.SaveChanges();

                subOrg.CustomerPlanId = customerPlan.CustomerPlanId;
                _context.Update(subOrg);

                var permittedApplicationIds = await _context.PlanFeatures.Join(_context.Pages, p => p.PageId, f => f.PageId, (p, f) => new { p, f })
                    .Where(x => x.p.PlanId == customerPlan.PlanId && x.p.Active == 1 && x.p.Deleted == false)
                    .Select(s => s.p.ApplicationId)
                    .Distinct().ToListAsync();

                var ttpOrgId = await _context.Organizations.Where(x => x.OrganizationName.ToUpper() == "TTP")
                    .Select(s => new { OrganizationId = s.OrganizationId, SubOrgId = s.SubOrgId }).FirstOrDefaultAsync();
                //batches to be common for all company.
                var _batchName = DateTime.Now.Year.ToString();
                var existingbatch = await _context.Batches.Where(x => x.BatchName == _batchName
                && x.OrgId == customerPlan.OrgId
                //&& x.SubOrgId == customerPlan.SubOrgId 
                && x.Deleted == false).ToListAsync();
                if (existingbatch.Count == 0)
                {
                    var batch = new Batch();
                    batch.BatchId = 0;
                    batch.BatchName = _batchName;
                    batch.StartDate = DateTime.Now;
                    batch.EndDate = DateTime.Now.AddMonths(12);
                    batch.CreatedDate = DateTime.Now;
                    batch.OrgId = customerPlan.OrgId;
                    batch.SubOrgId = customerPlan.SubOrgId;
                    batch.Deleted = false;
                    batch.Active = 1;
                    _context.Batches.Add(batch);
                }

                //var defaultMasters = await _context.MasterItems.Where(x => x.OrgId == ttpOrgId[0]
                //&& x.Active == 1 && permittedApplicationIds.Contains(x.ApplicationId))
                //    .OrderBy(o=>o.ParentId).ToListAsync();
                var defaultMasters = await _context.MasterItems.Where(x =>
                    x.OrgId == ttpOrgId.OrganizationId
                    && x.SubOrgId == ttpOrgId.SubOrgId
                    && x.ParentId != subOrg.ParentId//company not to be created again
                    && x.Active == 1
                    && x.Deleted == false
                    && permittedApplicationIds.Contains(x.ApplicationId)
                    )
                        .OrderBy(o => o.MasterDataId)
                        .ToListAsync();

                foreach (var item in defaultMasters)
                {
                    var existing = await _context.MasterItems.Where(x => x.MasterDataName == item.MasterDataName
                    && x.OrgId == customerPlan.OrgId
                    && x.SubOrgId == customerPlan.SubOrgId
                    && x.Deleted == false).ToListAsync();

                    if (existing.Count() == 0)
                    {
                        //check if parent of current item is in orgId =1
                        var parentData = await _context.MasterItems.Where(x => x.MasterDataId == item.ParentId
                                                                        && x.OrgId == ttpOrgId.OrganizationId
                                                                        && x.SubOrgId == ttpOrgId.SubOrgId
                                                                        && x.Deleted == false).ToListAsync();
                        //if yes
                        if (parentData.Count() > 0)
                        {
                            //if ParentId of ParentItem is zero, that is first level of masteritem (children of parentId =0).
                            //In this case, we will insert the item as it is.  
                            //check if ParentId of Parent item is not zero.
                            if (parentData[0].ParentId > 0)
                            {
                                //if yes, parentId should not be masterdataId of orgId=1. It should be of current organization Id.
                                var _shouldbeParentId = await _context.MasterItems.Where(x => x.MasterDataName == parentData[0].MasterDataName
                                 && x.OrgId == customerPlan.OrgId
                                 && x.SubOrgId == customerPlan.SubOrgId
                                 && x.Deleted == false).ToListAsync();
                                if (_shouldbeParentId.Count() > 0)
                                    item.ParentId = _shouldbeParentId[0].MasterDataId;
                                else
                                    item.ParentId = parentData[0].ParentId;
                            }
                        }

                        item.MasterDataId = 0;
                        item.OrgId = customerPlan.OrgId;
                        item.SubOrgId = customerPlan.SubOrgId;
                        item.CustomerPlanId = customerPlan.CustomerPlanId;
                        item.CreatedDate = DateTime.Now;
                        _context.MasterItems.Add(item);
                        // await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //var parentData = await _context.MasterItems.Where(x => x.MasterDataId == existing[0].ParentId
                        //                                                && x.OrgId == ttpOrgId.OrganizationId
                        //                                                && x.SubOrgId == ttpOrgId.SubOrgId
                        //                                                && x.Deleted == false).ToListAsync();
                        //if (parentData.Count() > 0)
                        //{
                        //    if (parentData[0].ParentId > 0)
                        //    {
                        //        var _shouldbeParentId = await _context.MasterItems.Where(x => x.MasterDataName == parentData[0].MasterDataName
                        //         && x.OrgId == customerPlan.OrgId
                        //         && x.SubOrgId == customerPlan.SubOrgId
                        //         && x.CustomerPlanId == customerPlan.CustomerPlanId
                        //         && x.Deleted == false).ToListAsync();

                        existing[0].CustomerPlanId = customerPlan.CustomerPlanId;
                        //  }
                        _context.MasterItems.Update(existing[0]);
                        //    await _context.SaveChangesAsync();
                        //}

                    }
                }
                await _context.SaveChangesAsync();

                var RoleMasterDataId = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == "role"
                && x.ParentId == 0).Select(s => s.MasterDataId).ToListAsync();

                var orgAdminRoleId = await _context.MasterItems.Where(x => x.ParentId == RoleMasterDataId[0]
                && x.MasterDataName.ToLower() == "admin"
                && x.OrgId == customerPlan.OrgId
                && x.SubOrgId == customerPlan.SubOrgId
                && x.Active == 1)
                    .Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (orgAdminRoleId == 0)
                {
                    var _commonPanelId = Convert.ToInt16(_configuration.GetSection("ApplicationConfig").GetSection("CommonAppId").Value);
                    var adminRole = new MasterItem();
                    adminRole.OrgId = customerPlan.OrgId;
                    adminRole.SubOrgId = customerPlan.SubOrgId;
                    adminRole.ApplicationId = _commonPanelId;
                    adminRole.MasterDataName = "Admin";
                    adminRole.Active = 1;
                    _context.MasterItems.Add(adminRole);
                    _context.SaveChanges();
                    orgAdminRoleId = adminRole.MasterDataId;
                }
                #region "this action is done so that admin of the customer need not assign features again to their members after changing plan."
                //disable all previous selected plan's features. (if any)
                var AllExistingFeatures = await _context.ApplicationFeatureRolesPerms.Where(x =>
                x.OrgId == customerPlan.OrgId
                && x.SubOrgId == customerPlan.SubOrgId
                && x.Active == 1)
                    .Select(s => s).ToListAsync();
                foreach (var item in AllExistingFeatures)
                {
                    item.Active = 0;
                    _context.ApplicationFeatureRolesPerms.Update(item);
                }
                /////

                //enable feature again in admin role if it is in new plan feature.
                //if the new plan feature is not there in existingadminrole feature, add it.
                var newPlanFeatureIds = await _context.PlanFeatures.Where(x => x.Active == 1
                && x.PlanId == customerPlan.PlanId
                && x.Deleted == false)
                    .Select(s => s.PlanFeatureId).ToListAsync();

                foreach (var planfeatureId in newPlanFeatureIds)
                {
                    var existing = AllExistingFeatures.FindAll(f => f.PlanFeatureId == planfeatureId
                    && f.PlanId == customerPlan.PlanId);

                    if (existing.Count() == 0)
                    {
                        var appfeatureRolePer = new ApplicationFeatureRolesPerm()
                        {
                            PlanFeatureId = planfeatureId,
                            RoleId = orgAdminRoleId,
                            OrgId = customerPlan.OrgId,
                            PlanId = customerPlan.PlanId,
                            SubOrgId = customerPlan.SubOrgId,
                            PermissionId = 1,
                            Active = 1,
                            CreatedBy = customerPlan.CreatedBy,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        };
                        _context.ApplicationFeatureRolesPerms.Add(appfeatureRolePer);
                    }
                    else
                    {
                        foreach (var item in existing)
                        {
                            item.Active = 1;
                            item.UpdatedDate = DateTime.Now;
                            item.UpdatedBy = customerPlan.UpdatedBy;
                            item.SubOrgId = customerPlan.SubOrgId;
                            _context.ApplicationFeatureRolesPerms.Update(item);
                            AllExistingFeatures.Where(x => x.ApplicationFeatureRoleId == item.ApplicationFeatureRoleId).ToList().ForEach(f => f.Active = 1);
                        }
                    }
                }
                var toDeleteFeature = AllExistingFeatures.FindAll(f => f.Active == 0);
                foreach (var feature in toDeleteFeature)
                {
                    feature.Active = 0;
                    feature.Deleted = true;
                    _context.ApplicationFeatureRolesPerms.Update(feature);
                }

                //adding some masters for new company
                var _classes = await _context.ClassMasters.Where(x =>
                x.OrgId == ttpOrgId.OrganizationId
                && x.SubOrgId == ttpOrgId.SubOrgId
                && x.Active == 1).ToListAsync();
                var _existingclasses = await _context.ClassMasters.Where(x =>
                x.OrgId == customerPlan.OrgId
                && x.SubOrgId == customerPlan.SubOrgId
                && x.Active == 1).ToListAsync();
                if (_existingclasses.Count == 0)
                {
                    foreach (var item in _classes)
                    {
                        item.ClassId = 0;
                        item.OrgId = customerPlan.OrgId;
                        item.SubOrgId = customerPlan.SubOrgId;
                        item.StudyAreaId = 0;
                        item.StudyModeId = 0;
                        item.BatchId = 0;
                        item.StartDate = null;
                        item.EndDate = null;
                        item.Active = 1;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedBy = customerPlan.CreatedBy;
                        _context.ClassMasters.Add(item);
                    }
                }

                var _feedefinitions = await _context.FeeDefinitions.Where(x =>
                x.OrgId == ttpOrgId.OrganizationId
                && x.SubOrgId == ttpOrgId.SubOrgId
                && x.Active == 1).ToListAsync();
                var _existingfeedefinitions = await _context.FeeDefinitions.Where(x =>
                x.OrgId == customerPlan.OrgId
                && x.SubOrgId == customerPlan.SubOrgId
                && x.Active == 1).ToListAsync();
                if (_existingfeedefinitions.Count == 0)
                {
                    foreach (var item in _feedefinitions)
                    {
                        item.FeeDefinitionId = 0;
                        item.OrgId = customerPlan.OrgId;
                        item.SubOrgId = customerPlan.SubOrgId;
                        item.FeeCategoryId = 0;
                        item.BatchId = 0;
                        item.Active = 1;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedBy = customerPlan.CreatedBy;
                        _context.FeeDefinitions.Add(item);
                    }
                }
                var _feetype = await _context.SchoolFeeTypes.Where(x => x.OrgId == ttpOrgId.OrganizationId
                && x.SubOrgId == ttpOrgId.SubOrgId
                && x.Active == 1).ToListAsync();
                var _existingfeetype = await _context.SchoolFeeTypes.Where(x =>
                x.OrgId == customerPlan.OrgId
                && x.SubOrgId == customerPlan.SubOrgId
                && x.Active == 1).ToListAsync();
                if (_existingfeetype.Count == 0)
                {
                    foreach (var item in _feetype)
                    {
                        item.FeeTypeId = 0;
                        item.OrgId = customerPlan.OrgId;
                        item.SubOrgId = customerPlan.SubOrgId;
                        item.BatchId = 0;
                        item.Active = 1;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedBy = customerPlan.CreatedBy;
                        _context.SchoolFeeTypes.Add(item);
                    }
                }
                var _accountNature = await _context.AccountNatures.Where(x => x.OrgId == ttpOrgId.OrganizationId
                && x.SubOrgId == ttpOrgId.SubOrgId
                && x.Active == true).ToListAsync();
                var _existingAccountNature = await _context.AccountNatures.Where(x =>
                x.OrgId == customerPlan.OrgId
                && x.SubOrgId == customerPlan.SubOrgId
                && x.Active == true).ToListAsync();

                if (_existingAccountNature.Count == 0)
                {
                    foreach (var item in _accountNature)
                    {
                        item.AccountNatureId = 0;
                        item.OrgId = customerPlan.OrgId;
                        item.SubOrgId = customerPlan.SubOrgId;
                        item.Active = true;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedBy = customerPlan.CreatedBy;
                        _context.AccountNatures.Add(item);
                    }
                    await _context.SaveChangesAsync();

                }
                var _GeneralAccount = await _context.GeneralLedgers.Where(x =>
                x.OrgId == ttpOrgId.OrganizationId
                && x.SubOrgId == ttpOrgId.SubOrgId
                && x.Active == 1
                && (x.EmployeeId == null || x.EmployeeId == 0)
                && (x.StudentClassId == null || x.StudentClassId == 0)).ToListAsync();

                foreach (var item in _GeneralAccount)
                {
                    var _existing = await _context.GeneralLedgers.Where(x =>
                     x.OrgId == customerPlan.OrgId
                     && x.SubOrgId == customerPlan.SubOrgId
                     && x.GeneralLedgerName.ToLower() == item.GeneralLedgerName.ToLower()
                     && x.Active == 1).ToListAsync();
                    var _accountName = await _context.AccountNatures.Where(x => x.AccountNatureId == item.AccountGroupId).Select(s => s.AccountName).FirstOrDefaultAsync();
                    var _accountGroupId = await _context.AccountNatures.Where(x => x.AccountName == _accountName
                                                                                && x.OrgId == customerPlan.OrgId
                                                                                && x.SubOrgId == customerPlan.SubOrgId
                                                                                ).Select(s => s.AccountNatureId).FirstOrDefaultAsync();
                    if (_existing.Count == 0)
                    {
                        var _newgeneralaccount = new GeneralLedger()
                        {
                            AccountNatureId = item.AccountNatureId,
                            AccountGroupId = _accountGroupId,
                            Active = 1,
                            Deleted = false,
                            OrgId = customerPlan.OrgId,
                            SubOrgId = customerPlan.SubOrgId,
                            CreatedDate = DateTime.Now,
                            StudentClassId = 0,
                            EmployeeId = 0,
                            GeneralLedgerName = item.GeneralLedgerName
                        };
                        _context.GeneralLedgers.Add(_newgeneralaccount);
                    }
                }

                var _studentGrade = await _context.StudentGrades.Where(x =>
                x.OrgId == ttpOrgId.OrganizationId
                && x.SubOrgId == ttpOrgId.SubOrgId
                && x.Active == 1).ToListAsync();
                var _existingStudentGrade = await _context.StudentGrades.Where(x =>
                x.OrgId == customerPlan.OrgId
                && x.SubOrgId == customerPlan.SubOrgId
                && x.Active == 1).ToListAsync();

                if (_existingStudentGrade.Count == 0)
                {
                    foreach (var item in _studentGrade)
                    {
                        item.StudentGradeId = 0;
                        item.OrgId = customerPlan.OrgId;
                        item.SubOrgId = customerPlan.SubOrgId;
                        item.ExamId = 0;
                        item.Active = 1;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedBy = customerPlan.CreatedBy;
                        _context.StudentGrades.Add(item);
                    }
                    //await _context.SaveChangesAsync();
                }

                #endregion

                await _context.SaveChangesAsync();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex);

            }
            return Ok(customerPlan);
        }

        // DELETE: api/CustomerPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerPlan(short id)
        {
            var customerPlan = await _context.CustomerPlans.FindAsync(id);
            if (customerPlan == null)
            {
                return NotFound();
            }

            _context.CustomerPlans.Remove(customerPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPlanExists(short id)
        {
            return _context.CustomerPlans.Any(e => e.CustomerPlanId == id);
        }
    }
}
