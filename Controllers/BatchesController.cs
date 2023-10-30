using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("Batches")]
    [EnableQuery]
    public class BatchesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public BatchesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/Batches
        [HttpGet]
        public IQueryable<Batch> Get()
        {
            return _context.Batches.AsNoTracking().AsQueryable();
        }

        // GET: api/Batches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Batch>> Get(short id)
        {
            var batch = await _context.Batches.FindAsync(id);

            if (batch == null)
            {
                return NotFound();
            }

            return batch;
        }

        // PUT: api/Batches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatch(short id, Batch batch)
        {
            if (id != batch.BatchId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(batch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<Batch> batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.Batches.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            batch.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                if (entity.CurrentBatch == 1)
                {
                    var oldbatch = await _context.StudentClasses.Where(x => x.BatchId != entity.BatchId
                   && x.OrgId == entity.OrgId
                   //&& x.SubOrgId == entity.SubOrgId
                   ).ToListAsync();
                    foreach (var x in oldbatch)
                    {
                        x.IsCurrent = false;
                        _context.Update(x);
                    }

                    var newBatch = await _context.StudentClasses.Where(x => x.BatchId == entity.BatchId
                   && x.OrgId == entity.OrgId
                   //&& x.SubOrgId == entity.SubOrgId
                   ).ToListAsync();
                    foreach (var y in newBatch)
                    {
                        y.IsCurrent = true;
                        _context.Update(y);
                    }
                }
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                //tran.Rollback();
                if (!BatchExists(key))
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
                //tran.Rollback();
                throw;
            }

            return Updated(entity);
        }
        // POST: api/Batches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Batch>> Post([FromBody] Batch batch)
        {

            _context.Batches.Add(batch);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                //if (batch.CurrentBatch == 1)
                //{
                //var otherbatches = await _context.Batches.Where(x => x.OrgId == batch.OrgId).ToListAsync();
                //foreach (var item in otherbatches)
                //{
                //    item.CurrentBatch = 0;
                //    _context.Batches.Update(item);
                //}
                //}
                //else
                //{
                //    var otherbatches = await _context.Batches.Where(x => x.OrgId == batch.OrgId 
                //    && x.CurrentBatch == 1
                //    && x.BatchId != batch.BatchId).ToListAsync();

                //    throw new Exception("There must be atleast one current batch.");

                //}
                await _context.SaveChangesAsync();
                //   tran.Commit();
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                throw;
            }
            return Ok(batch);
        }

        // DELETE: api/Batches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch(short id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatchExists(short id)
        {
            return _context.Batches.Any(e => e.BatchId == id);
        }
    }
}
