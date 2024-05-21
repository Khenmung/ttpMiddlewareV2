using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ttpMiddleware.Models;

using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class StudentEvaluationResultsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentEvaluationResultsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentEvaluationResults
        [HttpGet]
        public IQueryable<StudentEvaluationResult> GetStudentEvaluationResults()
        {
            return _context.StudentEvaluationResults.AsQueryable().AsNoTracking();
        }

        // GET: api/StudentEvaluationResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentEvaluationResult>> GetStudentEvaluationResult(int id)
        {
            var studentEvaluationResult = await _context.StudentEvaluationResults.FindAsync(id);

            if (studentEvaluationResult == null)
            {
                return NotFound();
            }

            return studentEvaluationResult;
        }

        // PUT: api/StudentEvaluationResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentEvaluationResult(int id, StudentEvaluationResult studentEvaluationResult)
        {
            if (id != studentEvaluationResult.StudentEvaluationResultId)
            {
                return BadRequest();
            }

            _context.Entry(studentEvaluationResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentEvaluationResultExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentEvaluationResult> studentEvaluationResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentEvaluationResults.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            studentEvaluationResult.Patch(entity);
            try
            {
                var resultMark = await _context.EvaluationResultMarks.Where(x => x.StudentClassId == entity.StudentClassId &&
                                        x.EvaluationExamMapId == entity.EvaluationExamMapId).FirstOrDefaultAsync();


                if (resultMark == null)
                {
                    var result = new EvaluationResultMark();
                    result.StudentClassId = entity.StudentClassId;
                    result.EvaluationExamMapId = entity.EvaluationExamMapId;
                    result.ClassId = entity.ClassId;
                    result.SectionId = entity.SectionId;
                    result.SemesterId = entity.SemesterId;
                    result.EvaluationResultMarkId = 0;
                    result.TotalMark = 0;
                    result.Rank = 0;
                    result.OrgId = entity.OrgId;
                    result.SubOrgId = entity.SubOrgId;
                    result.Active = true;
                    result.Comments = "";
                    result.CreatedDate = DateTime.Now;
                    result.CreatedBy = entity.CreatedBy;
                    _context.EvaluationResultMarks.Add(result);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentEvaluationResultExists(key))
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
        // POST: api/StudentEvaluationResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentEvaluationResult>> PostStudentEvaluationResult([FromBody] JArray studentEvaluationResult)
        {
            StudentEvaluationResult _result = new StudentEvaluationResult();
            //var ResultDetail="";
            int TotalMarks = 0;

            var temp = studentEvaluationResult[0].ToObject<StudentEvaluationResult>();

            var ResultDetail = await _context.EvaluationExamMaps
                    .Join(_context.EvaluationMasters,
                    eval => eval.EvaluationMasterId,
                    master => master.EvaluationMasterId,
                    (eval, master) => new { eval.EvaluationExamMapId, master.PassMark, master.FullMark, master.DisplayResult, master.Duration })
                    .Where(x => x.EvaluationExamMapId == temp.EvaluationExamMapId).ToListAsync();
            try
            {

                foreach (var x in studentEvaluationResult)
                {
                    _result = x.ToObject<StudentEvaluationResult>();
                    //if(TotalMarks==0)

                    var studentevaluationObject = new StudentEvaluationResult()
                    {
                        StudentEvaluationResultId = _result.StudentEvaluationResultId,
                        ClassId = _result.ClassId,
                        SectionId = _result.SectionId,
                        SemesterId = _result.SemesterId,
                        Active = _result.Active,
                        ClassEvaluationId = _result.ClassEvaluationId,
                        AnswerText = _result.AnswerText,
                        HistoryText = _result.HistoryText,
                        EvaluationExamMapId = _result.EvaluationExamMapId,
                        OrgId = _result.OrgId,
                        SubOrgId = _result.SubOrgId,
                        StudentClassId = _result.StudentClassId,
                        StudentId = _result.StudentId,
                        Deleted = false,
                        Submitted = _result.Submitted,
                        CreatedDate = System.DateTime.Now,
                        CreatedBy = _result.CreatedBy
                    };
                    if (studentevaluationObject.StudentEvaluationResultId == 0)
                        _context.StudentEvaluationResults.Add(studentevaluationObject);
                    else
                        _context.StudentEvaluationResults.Update(studentevaluationObject);

                    await _context.SaveChangesAsync();

                    foreach (var item in _result.StudentEvaluationAnswers)
                    {
                        var answers = new StudentEvaluationAnswer()
                        {
                            StudentEvaluationAnswerId = item.StudentEvaluationAnswerId,
                            StudentEvaluationResultId = studentevaluationObject.StudentEvaluationResultId,
                            ClassEvaluationAnswerOptionsId = item.ClassEvaluationAnswerOptionsId,
                            Active = (byte)item.Active,
                            OrgId = studentevaluationObject.OrgId,
                            SubOrgId = studentevaluationObject.SubOrgId,
                            CreatedBy = studentevaluationObject.CreatedBy,
                            CreatedDate = studentevaluationObject.CreatedDate,
                            Deleted = false,
                        };
                        if (answers.StudentEvaluationAnswerId == 0)
                            _context.StudentEvaluationAnswers.Add(answers);
                        else
                            _context.StudentEvaluationAnswers.Update(answers);

                        //var _ClassEvaluationOptionList = await _context.ClassEvaluationOptions.Where(x => x.OrgId == temp.OrgId
                        //                     && x.ParentId == item.ClassEvaluationAnswerOptionsId).ToListAsync();


                        if (ResultDetail.Count > 0 && ResultDetail[0].DisplayResult == true)
                        {
                            //foreach (var option in _ClassEvaluationOptionList)
                            //{
                            var _optionValue = await _context.ClassEvaluationOptions.Where(x =>
                               x.Correct == 1
                              && x.Active == 1
                              && x.ClassEvaluationAnswerOptionsId == item.ClassEvaluationAnswerOptionsId).Select(s => s.Point).FirstOrDefaultAsync();

                            if (_optionValue != null)
                                TotalMarks += _optionValue == null ? 0 : (int)_optionValue;

                            //}
                        }
                    }
                }
                //studentEvaluationResult.Count()>0 means if it is not single question save.
                //if (studentEvaluationResult.Count()>0 &&
                //    ResultDetail.Count > 0 && ResultDetail[0].DisplayResult == true)
                if (temp.Submitted)
                {
                    var resultMark = await _context.EvaluationResultMarks.Where(x => x.StudentClassId == temp.StudentClassId &&
                                        x.EvaluationExamMapId == temp.EvaluationExamMapId).FirstOrDefaultAsync();
                    if (resultMark == null)
                    {
                        var result = new EvaluationResultMark();
                        result.StudentClassId = temp.StudentClassId;
                        result.EvaluationExamMapId = temp.EvaluationExamMapId;
                        result.ClassId = temp.ClassId;
                        result.SectionId = temp.SectionId;
                        result.SemesterId = temp.SemesterId;
                        result.EvaluationResultMarkId = 0;
                        result.TotalMark = 0;
                        result.Rank = 0;
                        result.OrgId = temp.OrgId;
                        result.SubOrgId = temp.SubOrgId;
                        result.Active = true;
                        result.Comments = "";
                        result.CreatedDate = DateTime.Now;
                        result.CreatedBy = temp.CreatedBy;
                        _context.EvaluationResultMarks.Add(result);
                    }
                    else
                    {
                        resultMark.Active = true;
                        _context.Update(resultMark);
                    }
                        
                    //var _examStatusId = 0;
                    //if (TotalMarks > 0)
                    //{
                    //    var _percent = (TotalMarks / ResultDetail[0].FullMark) * 100;
                    //    var _parentId = await _context.MasterItems.Where(x => x.MasterDataName == "exam status" && x.OrgId == 0 && x.ParentId == 0).ToListAsync();
                    //    var status = "";
                    //    if (_percent >= ResultDetail[0].PassMark)
                    //        status = "pass";
                    //    else
                    //        status = "fail";

                    //    _examStatusId = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == status
                    //        && x.OrgId == _result.OrgId
                    //        && x.SubOrgId == _result.SubOrgId
                    //        && x.ParentId == _parentId[0].MasterDataId).Select(s => s.MasterDataId).FirstOrDefaultAsync();

                    //    var _examId = await _context.EvaluationExamMaps.Where(x => x.EvaluationExamMapId == _result.EvaluationExamMapId
                    //    && x.OrgId == _result.OrgId
                    //    && x.SubOrgId == _result.SubOrgId
                    //    && x.Active == true)
                    //    .Select(s => s.ExamId).FirstOrDefaultAsync();

                    //    var _examStudentResult = new ExamStudentResult()
                    //    {
                    //        ExamId = (short)_examId,
                    //        EvaluationExamMapId = _result.EvaluationExamMapId,
                    //        StudentClassId = _result.StudentClassId,
                    //        SectionId= _result.SectionId,
                    //        SemesterId= _result.SemesterId,
                    //        OrgId = _result.OrgId,
                    //        SubOrgId = _result.SubOrgId,
                    //        Active = 1,
                    //        MarkPercent = (decimal?)_percent,
                    //        TotalMarks = TotalMarks,
                    //        Deleted = false,
                    //        ExamStatusId = _examStatusId,
                    //        BatchId = (short)_result.BatchId
                    //    };

                    //    _context.ExamStudentResults.Add(_examStudentResult);
                    //}
                }

                await _context.SaveChangesAsync();
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }



        }

        // DELETE: api/StudentEvaluationResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentEvaluationResult(int id)
        {
            var studentEvaluationResult = await _context.StudentEvaluationResults.FindAsync(id);
            if (studentEvaluationResult == null)
            {
                return NotFound();
            }

            _context.StudentEvaluationResults.Remove(studentEvaluationResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentEvaluationResultExists(int id)
        {
            return _context.StudentEvaluationResults.Any(e => e.StudentEvaluationResultId == id);
        }
    }
}
