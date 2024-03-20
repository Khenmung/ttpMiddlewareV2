using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using Newtonsoft.Json.Linq;
using System;
using ttpMiddleware.Models.DTOs.Requests;

using ttpMiddleware.CommonFunctions;
using System.Collections.Generic;
using System.Diagnostics;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("ExamStudentResults")]
    [EnableQuery]
    public class ExamStudentResultsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ExamStudentResultsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ExamStudentResults
        [HttpGet]
        public IQueryable<ExamStudentResult> GetExamStudentResults()
        {
            return _context.ExamStudentResults.AsQueryable().AsNoTracking();
        }

        // GET: api/ExamStudentResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamStudentResult>> GetExamStudentResult(short id)
        {
            var examStudentResult = await _context.ExamStudentResults.FindAsync(id);

            if (examStudentResult == null)
            {
                return NotFound();
            }

            return examStudentResult;
        }

        // PUT: api/ExamStudentResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamStudentResult(short id, ExamStudentResult examStudentResult)
        {
            if (id != examStudentResult.ExamStudentResultId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(examStudentResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentResultExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ExamStudentResult> examStudentResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ExamStudentResults.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            examStudentResult.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentResultExists(key))
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


        // POST: api/ExamStudentResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamStudentResult>> PostExamStudentResult([FromBody] JObject jsonWrapper)
        {

            //JToken jsonValues = jsonWrapper;
            //ExamStudentResult _examStudentResult;
            JToken jsonValues = jsonWrapper;
            List<ExamStudentResult> _ExamStudentResult = new List<ExamStudentResult>();
            List<ExamResultSubjectMark> _ExamResultSubjectMark = new List<ExamResultSubjectMark>();

            foreach (JProperty x in jsonValues)
            {
                if (x.Name == "ExamStudentResult")
                    _ExamStudentResult = x.Value.ToObject<List<ExamStudentResult>>();
                else if (x.Name == "ExamResultSubjectMark")
                    _ExamResultSubjectMark = x.Value.ToObject<List<ExamResultSubjectMark>>();
            }
            using var tran = _context.Database.BeginTransaction();

            try
            {
                foreach (var _examStudentResult in _ExamStudentResult)
                {
                    //_examStudentResult = x.ToObject<ExamStudentResult>();
                    var studentresult = await _context.ExamStudentResults.Where(x => x.ExamId == _examStudentResult.ExamId
                    && x.StudentClassId == _examStudentResult.StudentClassId).Select(s => s).ToListAsync();
                    //_context.Entry(studentresult).State = EntityState.Detached;
                    if (studentresult.Count > 0)
                    {
                        foreach (var item in studentresult)
                        {
                            //var studentresult = _context.ExamStudentResults.Where(x => x.ExamId == _examStudentResult.ExamId && x.StudentClassId == _examStudentResult.StudentClassId).;
                            var Id = item.ExamStudentResultId;

                            item.Active = _examStudentResult.Active;
                            item.ExamId = _examStudentResult.ExamId;
                            item.OrgId = _examStudentResult.OrgId;
                            item.SubOrgId = _examStudentResult.SubOrgId;
                            item.ClassId = _examStudentResult.ClassId;
                            item.SectionId = _examStudentResult.SectionId;
                            item.Rank = _examStudentResult.Rank;
                            item.MarkPercent = _examStudentResult.MarkPercent;
                            item.StudentClassId = _examStudentResult.StudentClassId;
                            item.StudentId = _examStudentResult.StudentId;
                            item.TotalMarks = _examStudentResult.TotalMarks;
                            item.Division= _examStudentResult.Division;
                            item.ClassStrength = _examStudentResult.ClassStrength;
                            item.Attendance = _examStudentResult.Attendance;
                            item.PassCount = _examStudentResult.PassCount;
                            item.FailCount = _examStudentResult.FailCount;
                            item.ExamStudentResultId = Id;
                            item.UpdatedDate = DateTime.Now;
                            _context.ExamStudentResults.Update(item);

                        }
                    }
                    else
                    {
                        _context.ExamStudentResults.Add(_examStudentResult);
                    }
                }
                foreach(var _markdetail in _ExamResultSubjectMark)
                {
                    //if(_markdetail.StudentClassId== 4744)
                    //{
                        
                    //}
                    var resultmarks = await _context.ExamResultSubjectMarks.Where(x => x.ExamId == _markdetail.ExamId
                    && x.StudentClassId == _markdetail.StudentClassId 
                    && x.StudentClassSubjectId == _markdetail.StudentClassSubjectId
                    && x.SubOrgId == _markdetail.SubOrgId
                    && x.OrgId== _markdetail.OrgId).Select(s => s).ToListAsync();
                    //_context.Entry(studentresult).State = EntityState.Detached;
                    if (resultmarks.Count > 0)
                    {
                        foreach (var item in resultmarks)
                        {
                            //var studentresult = _context.ExamStudentResults.Where(x => x.ExamId == _examStudentResult.ExamId && x.StudentClassId == _examStudentResult.StudentClassId).;
                            var Id = item.ExamResultSubjectMarkId;

                            item.Active = _markdetail.Active;
                            item.ExamId = _markdetail.ExamId;
                            item.OrgId = _markdetail.OrgId;
                            item.SubOrgId = _markdetail.SubOrgId;
                            item.StudentClassId = _markdetail.StudentClassId;
                            item.Grade = _markdetail.Grade;
                            item.Marks = _markdetail.Marks;
                            item.ActualMarks = _markdetail.ActualMarks;
                            item.ExamResultSubjectMarkId = Id;
                            item.StudentClassSubjectId = _markdetail.StudentClassSubjectId;
                            item.BatchId = _markdetail.BatchId;
                            item.Deleted = false;
                            item.UpdatedDate = DateTime.Now;
                            _context.ExamResultSubjectMarks.Update(item);

                        }
                    }
                    else
                    {
                        _context.ExamResultSubjectMarks.Add(_markdetail);
                    }
                }
                await _context.SaveChangesAsync();
                tran.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/ExamStudentResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamStudentResult(short id)
        {
            var examStudentResult = await _context.ExamStudentResults.FindAsync(id);
            if (examStudentResult == null)
            {
                return NotFound();
            }

            _context.ExamStudentResults.Remove(examStudentResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamStudentResultExists(short id)
        {
            return _context.ExamStudentResults.Any(e => e.ExamStudentResultId == id);
        }
    }
}
