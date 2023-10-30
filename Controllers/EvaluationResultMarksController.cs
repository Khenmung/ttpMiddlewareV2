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
using Newtonsoft.Json.Linq;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class EvaluationResultMarksController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EvaluationResultMarksController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EvaluationResultMarks
        [HttpGet]
        public IQueryable<EvaluationResultMark> GetEvaluationResultMarks()
        {
            return _context.EvaluationResultMarks.AsNoTracking().AsQueryable();
        }

        // GET: api/EvaluationResultMarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationResultMark>> GetEvaluationResultMark(int id)
        {
            var evaluationResultMark = await _context.EvaluationResultMarks.FindAsync(id);

            if (evaluationResultMark == null)
            {
                return NotFound();
            }

            return evaluationResultMark;
        }

        // PUT: api/EvaluationResultMarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluationResultMark(int id, EvaluationResultMark evaluationResultMark)
        {
            if (id != evaluationResultMark.EvaluationResultMarkId)
            {
                return BadRequest();
            }

            _context.Entry(evaluationResultMark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationResultMarkExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EvaluationResultMark> evaluationResultMark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EvaluationResultMarks.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            evaluationResultMark.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                //tran.Rollback();
                if (!EvaluationResultMarkExists(key))
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
                return BadRequest(ex);
            }

            return Updated(entity);
        }
        // POST: api/EvaluationResultMarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvaluationResultMark>> PostEvaluationResultMark([FromBody] JArray resultArray)//[FromBody] EvaluationResultMark evaluationResultMark)
        {
            try
            {
                JToken jsonValues = resultArray;
                EvaluationResultMark _EvaluationResultMark;

                foreach (var x in jsonValues)
                {
                    _EvaluationResultMark = x.ToObject<EvaluationResultMark>();
                    if (_EvaluationResultMark != null)
                    {
                        //var existing = await _context.EvaluationResultMarks.Where(y => y.StudentClassId == _EvaluationResultMark.StudentClassId
                        //&& y.EvaluationExamMapId == _EvaluationResultMark.EvaluationExamMapId
                        //&& y.OrgId == _EvaluationResultMark.OrgId
                        //&& y.SubOrgId == _EvaluationResultMark.SubOrgId).FirstOrDefaultAsync();
                        if (_EvaluationResultMark.EvaluationResultMarkId >0)
                        {
                            var result = new EvaluationResultMark();
                            result.EvaluationResultMarkId = _EvaluationResultMark.EvaluationResultMarkId;
                            result.StudentClassId = _EvaluationResultMark.StudentClassId;
                            result.EvaluationExamMapId = _EvaluationResultMark.EvaluationExamMapId;
                            result.ClassId = _EvaluationResultMark.ClassId;
                            result.SectionId = _EvaluationResultMark.SectionId;
                            result.SemesterId = _EvaluationResultMark.SemesterId;
                            result.TotalMark = _EvaluationResultMark.TotalMark;
                            result.Rank = _EvaluationResultMark.Rank;
                            result.OrgId = _EvaluationResultMark.OrgId;
                            result.SubOrgId = _EvaluationResultMark.SubOrgId;
                            result.Active = true;
                            result.Comments = _EvaluationResultMark.Comments;                            
                            _context.Update(result);
                        }
                        else
                        {
                            _context.EvaluationResultMarks.Add(_EvaluationResultMark);
                        }
                    }

                }


                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/EvaluationResultMarks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluationResultMark(int id)
        {
            var evaluationResultMark = await _context.EvaluationResultMarks.FindAsync(id);
            if (evaluationResultMark == null)
            {
                return NotFound();
            }

            _context.EvaluationResultMarks.Remove(evaluationResultMark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluationResultMarkExists(int id)
        {
            return _context.EvaluationResultMarks.Any(e => e.EvaluationResultMarkId == id);
        }
    }
}
