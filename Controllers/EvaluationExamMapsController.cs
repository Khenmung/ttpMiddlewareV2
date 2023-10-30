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
    [EnableQuery(MaxExpansionDepth = 3)]
    public class EvaluationExamMapsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EvaluationExamMapsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EvaluationClassSubjectMaps
        [HttpGet]
        public IQueryable<EvaluationExamMap> GetEvaluationExamMaps()
        {
            try
            {
                return _context.EvaluationExamMaps.AsQueryable().AsNoTracking();
            }
            catch(Exception ex)
            {
                return (IQueryable<EvaluationExamMap>)BadRequest(ex);
            }
            
        }

        // GET: api/EvaluationClassSubjectMaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationExamMap>> GetEvaluationExamMap(int id)
        {
            var evaluationExamMap = await _context.EvaluationExamMaps.FindAsync(id);

            if (evaluationExamMap == null)
            {
                return NotFound();
            }

            return evaluationExamMap;
        }

        // PUT: api/EvaluationClassSubjectMaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluationClassSubjectMap(int id, EvaluationExamMap evaluationExamMap)
        {
            if (id != evaluationExamMap.EvaluationExamMapId)
            {
                return BadRequest();
            }

            _context.Entry(evaluationExamMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationExamMapExists(id))
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
        [HttpPatch("{key}")]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EvaluationExamMap> evaluationExamMap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EvaluationExamMaps.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            evaluationExamMap.Patch(entity);
            try
            {
                //var existing = await _context.StudentEvaluationResults.Where(x => x.EvaluationExamMapId == key
                //&& x.OrgId == entity.OrgId
                //&& x.SubOrgId == entity.SubOrgId
                //&& x.Active == 1).FirstOrDefaultAsync();
                //if (existing != null)
                //{
                //    return BadRequest("Map Id already in use. Cannot update.");
                //}
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationExamMapExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Updated(entity);
        }
        // POST: api/EvaluationClassSubjectMaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvaluationExamMap>> Post([FromBody]EvaluationExamMap evaluationExamMap)
        {
            try
            {

                _context.EvaluationExamMaps.Add(evaluationExamMap);
                await _context.SaveChangesAsync();

                return Ok(evaluationExamMap);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // DELETE: api/EvaluationClassSubjectMaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluationClassSubjectMap(int id)
        {
            var evaluationExamMap = await _context.EvaluationExamMaps.FindAsync(id);
            if (evaluationExamMap == null)
            {
                return NotFound();
            }

            _context.EvaluationExamMaps.Remove(evaluationExamMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluationExamMapExists(int id)
        {
            return _context.EvaluationExamMaps.Any(e => e.EvaluationExamMapId == id);
        }
    }
}
