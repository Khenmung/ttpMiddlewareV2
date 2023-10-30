using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;
using ttpMiddleware.CommonFunctions;
using Microsoft.AspNetCore.Builder;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class SubjectComponentsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public SubjectComponentsController(ttpauthContext context)
        {
            _context = context;
        }

        //GET: api/SubjectComponents
       [HttpGet]
        public IQueryable<SubjectComponent> GetSubjectComponents()
        {
            return _context.SubjectComponents.AsQueryable().AsNoTracking();
        }

        //GET: api/SubjectComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectComponent>> GetSubjectComponent(int id)
        {
            var subjectComponent = await _context.SubjectComponents.FindAsync(id);

            if (subjectComponent == null)
            {
                return NotFound();
            }

            return subjectComponent;
        }

        //PUT: api/SubjectComponents/5
         //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjectComponent(int id, SubjectComponent subjectComponent)
        {
            if (id != subjectComponent.SubjectComponentId)
            {
                return BadRequest();
            }

            _context.Entry(subjectComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectComponentExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<SubjectComponent> subjectComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.SubjectComponents.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            subjectComponent.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();
                //if (entity.CurrentBatch == 1)
                //{
                //    var _otherbatches = await _context.Batches.Where(x => x.BatchId != entity.BatchId
                //     && x.OrgId == entity.OrgId).ToListAsync();
                //    foreach (var item in _otherbatches)
                //    {
                //        item.CurrentBatch = 0;
                //        _context.Batches.Update(item);
                //    }
                //}
                //else
                //{
                //    throw new Exception("There must be atlest one current batch.");
                //}
                //tran.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                //tran.Rollback();
                if (!SubjectComponentExists(key))
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
        //POST: api/SubjectComponents
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubjectComponent>> PostSubjectComponent([FromBody] SubjectComponent subjectComponent)
        {
            try
            {
                _context.SubjectComponents.Add(subjectComponent);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(subjectComponent);
        }

        //DELETE: api/SubjectComponents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjectComponent(int id)
        {
            var subjectComponent = await _context.SubjectComponents.FindAsync(id);
            if (subjectComponent == null)
            {
                return NotFound();
            }

            _context.SubjectComponents.Remove(subjectComponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectComponentExists(int id)
        {
            return _context.SubjectComponents.Any(e => e.SubjectComponentId == id);
        }
    }
}
