using System;
using System.Collections;
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
    public class PlanAndMasterItemsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public PlanAndMasterItemsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/PlanAndMasterItems
        [HttpGet]
        public IQueryable<PlanAndMasterItem> GetPlanAndMasterItems()
        {
            return _context.PlanAndMasterItems.AsQueryable().AsNoTracking();
        }

        // GET: api/PlanAndMasterItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanAndMasterItem>> GetPlanAndMasterItem(short id)
        {
            var planAndMasterItem = await _context.PlanAndMasterItems.FindAsync(id);

            if (planAndMasterItem == null)
            {
                return NotFound();
            }

            return planAndMasterItem;
        }

        // PUT: api/PlanAndMasterItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanAndMasterItem(short id, PlanAndMasterItem planAndMasterItem)
        {
            if (id != planAndMasterItem.PlanAndMasterDataId)
            {
                return BadRequest();
            }

            _context.Entry(planAndMasterItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanAndMasterItemExists(id))
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="album"></param>
        /// <returns></returns>
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<PlanAndMasterItem> planAndMasterItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.PlanAndMasterItems.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            using var tran = _context.Database.BeginTransaction();
            planAndMasterItem.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
                //var updates = await (from m in _context.MasterItems.Where(x => x.ParentId == entity.MasterDataId)
                //                     join p in _context.PlanAndMasterItems.Where(x => x.PlanId == entity.PlanId)
                //                     on m.MasterDataId equals p.MasterDataId into pGroup
                //                     from plan in pGroup.DefaultIfEmpty()
                //                     select new
                //                     {
                //                         MasterDataId = m.MasterDataId,
                //                         ParentId = m.ParentId,
                //                         PlanAndMasterDataId = plan == null ? 0 : plan.PlanAndMasterDataId
                //                     }).ToListAsync();
                var childitems = await _context.MasterItems.Where(x => x.ParentId == entity.MasterDataId).ToListAsync();

                await recursiveUpdate(childitems, entity);
                tran.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                //tran.Rollback();
                if (!PlanAndMasterItemExists(key))
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
        // POST: api/PlanAndMasterItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlanAndMasterItem>> PostPlanAndMasterItem([FromBody] PlanAndMasterItem planAndMasterItem)
        {
            using var tran = _context.Database.BeginTransaction();
            try
            {
                _context.PlanAndMasterItems.Add(planAndMasterItem);
                await _context.SaveChangesAsync();
                var childitems =await _context.MasterItems.Where(x => x.ParentId == planAndMasterItem.MasterDataId).ToListAsync();
                await recursiveUpdate(childitems,planAndMasterItem);       
                tran.Commit();
                return Ok(planAndMasterItem);
            }
            catch(Exception ex)
            {
                //tran.Rollback();
                throw;

            }
            
        }

        // DELETE: api/PlanAndMasterItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanAndMasterItem(short id)
        {
            var planAndMasterItem = await _context.PlanAndMasterItems.FindAsync(id);
            if (planAndMasterItem == null)
            {
                return NotFound();
            }

            _context.PlanAndMasterItems.Remove(planAndMasterItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanAndMasterItemExists(short id)
        {
            return _context.PlanAndMasterItems.Any(e => e.PlanAndMasterDataId == id);
        }
        private async Task recursiveUpdate(List<MasterItem> childitems, PlanAndMasterItem parent)
        {
            foreach (var item in childitems)
            {
                var childplanandmasters = await _context.PlanAndMasterItems.Where(x => x.MasterDataId == item.MasterDataId).ToListAsync();
                if (childplanandmasters.Count == 0)
                {
                    var _planNMasteritem = new PlanAndMasterItem()
                    {
                        PlanAndMasterDataId = 0,
                        PlanId = parent.PlanId,
                        MasterDataId = item.MasterDataId,
                        Active = parent.Active,
                        ApplicationId = parent.ApplicationId,
                        CreatedDate = DateTime.Now,

                    };
                    _context.PlanAndMasterItems.Add(_planNMasteritem);                    
                }
                else
                {
                    //var toupdate = await _context.PlanAndMasterItems.Where(x => x.PlanAndMasterDataId == item.PlanAndMasterDataId).ToListAsync();
                    foreach (var plannmaster in childplanandmasters)
                    {
                        plannmaster.UpdatedDate = DateTime.Now;
                        plannmaster.Active = parent.Active;
                        _context.PlanAndMasterItems.Update(plannmaster);
                    }
                }
                var nestedchilditems = await _context.MasterItems.Where(x => x.ParentId == item.MasterDataId).ToListAsync();
                await recursiveUpdate(nestedchilditems, parent);
            }
            await _context.SaveChangesAsync();
        }
    }
}
