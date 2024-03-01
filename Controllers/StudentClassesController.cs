using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using ttpMiddleware.Models.DTOs.Requests;
using System;
using System.Collections.Generic;

using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class StudentClassesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentClassesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentClasses
        [HttpGet]
        public IQueryable<StudentClass> GetStudentClasses()
        {
            return _context.StudentClasses.AsQueryable().AsNoTracking();
        }

        // GET: api/StudentClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentClass>> GetStudentClass(int id)
        {
            var studentClass = await _context.StudentClasses.FindAsync(id);

            if (studentClass == null)
            {
                return NotFound();
            }

            return studentClass;
        }

        // PUT: api/StudentClasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentClass(int id, StudentClass studentClass)
        {
            if (id != studentClass.StudentClassId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(studentClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentClassExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentClass> studentClass)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _context.StudentClasses.FindAsync(key);

            if (entity == null)
            {
                return NotFound();
            }
            var existingSectionId = entity.SectionId;
            var existingSemesterId = entity.SemesterId;
            var existingClassId = entity.ClassId;
            var existingActive = entity.Active;
            studentClass.Patch(entity);
            var tran = _context.Database.BeginTransaction();
            try
            {
                //if (!String.IsNullOrEmpty(entity.AdmissionNo))
                //{
                //    var _admissionNo = await _context.StudentClasses.Where(x => x.StudentClassId != entity.StudentClassId
                //    && x.OrgId == entity.OrgId
                //    && x.SubOrgId == entity.SubOrgId
                //    && x.AdmissionNo == entity.AdmissionNo).Select(s => s.StudentClassId).FirstOrDefaultAsync();

                //    if (_admissionNo > 0)
                //        return BadRequest("Admission no. already exist.");

                //}
                if (!String.IsNullOrEmpty(entity.RollNo))
                {
                    var _RollNO = await _context.StudentClasses.Where(x => x.StudentClassId != entity.StudentClassId
                    && x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId
                    && x.ClassId == entity.ClassId
                    && x.SectionId == entity.SectionId
                    && x.SemesterId == entity.SemesterId
                    && x.BatchId == entity.BatchId
                    && x.RollNo == entity.RollNo).Select(s => s.StudentClassId).FirstOrDefaultAsync();
                    if (_RollNO > 0)
                        return BadRequest(entity.RollNo + "-Roll no. already exist.");
                }
                var _student = await _context.Students.Where(x => x.StudentId != entity.StudentId
                    && x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId
                    ).FirstOrDefaultAsync();
                if (_student != null)
                {
                    _student.ClassId = entity.ClassId;
                    _context.Update(_student);
                }
                if (existingSemesterId != entity.SemesterId || existingSectionId != entity.SectionId || existingActive != entity.Active)
                {
                    var studentClassSubject = await _context.StudentClassSubjects.Where(x => x.StudentClassId == entity.StudentClassId
                        && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();

                    foreach (var item in studentClassSubject)
                    {
                        item.Active = item.Active == 1 ? entity.Active : item.Active;
                        item.SemesterId = (int)entity.SemesterId;
                        item.SectionId = (int)entity.SectionId;
                        _context.Update(item);
                    }

                    var exammarkentity = await _context.ExamResultSubjectMarks.Where(x => x.StudentClassId == entity.StudentClassId
                                            && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();
                    foreach (var item in exammarkentity)
                    {
                        item.Active = !item.Active ? false : item.Active;
                        item.SemesterId = (int)entity.SemesterId;
                        item.SectionId = (int)entity.SectionId;
                        _context.Update(item);
                    }
                    var StudentEvaluationResult = await _context.StudentEvaluationResults.Where(x => x.StudentClassId == entity.StudentClassId
                                           && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();
                    foreach (var item in StudentEvaluationResult)
                    {
                        item.Active = (byte)(item.Active == 0 ? 0 : item.Active);
                        item.SemesterId = (int)entity.SemesterId;
                        item.SectionId = (int)entity.SectionId;
                        _context.Update(item);
                    }
                    var studentFeeReceipt = await _context.StudentFeeReceipts.Where(x => x.StudentClassId == entity.StudentClassId
                                           && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();
                    foreach (var item in studentFeeReceipt)
                    {
                        item.Active = (byte)(item.Active == 0 ? 0 : item.Active);
                        //item.SemesterId = (int)entity.SemesterId;
                        item.SectionId = (int)entity.SectionId;
                        _context.Update(item);
                    }
                    var sportResult = await _context.SportResults.Where(x => x.StudentClassId == entity.StudentClassId
                                           && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();
                    foreach (var item in sportResult)
                    {
                        item.Active = (byte)(item.Active == 0 ? 0 : item.Active);
                        //item.SemesterId = (int)entity.SemesterId;
                        item.SectionId = (int)entity.SectionId;
                        _context.Update(item);
                    }
                    var examStudentResult = await _context.ExamStudentResults.Where(x => x.StudentClassId == entity.StudentClassId
                                          && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();
                    foreach (var item in examStudentResult)
                    {
                        item.Active = (byte)(item.Active == 0 ? 0 : item.Active);
                        item.SemesterId = (int)entity.SemesterId;
                        item.SectionId = (int)entity.SectionId;
                        _context.Update(item);
                    }
                    var examStudentSubjectResult = await _context.ExamStudentSubjectResults.Where(x => x.StudentClassId == entity.StudentClassId
                                          && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();
                    foreach (var item in examStudentSubjectResult)
                    {
                        item.Active = (byte)(item.Active == 0 ? 0 : item.Active);
                        item.SemesterId = (int)entity.SemesterId;
                        item.SectionId = (int)entity.SectionId;
                        _context.Update(item);
                    }
                    var attendance = await _context.Attendances.Where(x => x.StudentClassId == entity.StudentClassId
                                          && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();
                    foreach (var item in attendance)
                    {
                        //item.ac = (byte)(item.Active == 0 ? 0 : item.Active);
                        item.SemesterId = (int)entity.SemesterId;
                        item.SectionId = (int)entity.SectionId;
                        _context.Update(item);
                    }
                    var accountledgers = await _context.AccountingLedgerTrialBalances.Where(x => x.StudentClassId == entity.StudentClassId
                                          && x.OrgId == entity.OrgId && x.SubOrgId == entity.SubOrgId).ToListAsync();
                    foreach (var item in accountledgers)
                    {
                        item.Active = (byte)(item.Active == 0 ? 0 : item.Active);
                        item.SemesterId = (int)entity.SemesterId;
                        item.SectionId = (int)entity.SectionId;
                        _context.Update(item);
                    }
                }
                //var studentaccount = await _context.GeneralLedgers.Where(x => x.StudentClassId == entity.StudentClassId).Select(s => s.GeneralLedgerId).ToListAsync();
                //if (studentaccount.Count == 0)
                //{
                //    var _studentAccountNatureId = await _context.AccountNatures.Where(x => x.AccountName.ToLower() == "assets" && x.OrgId == 0 && x.Active == true)
                //                        .Select(s => s.AccountNatureId).FirstOrDefaultAsync();
                //    var _studentAccountGroupId = await _context.AccountNatures.Where(x => x.AccountName.ToLower() == "current assets" && x.OrgId == entity.OrgId)
                //        .Select(s => s.AccountNatureId).FirstOrDefaultAsync();
                //    var _studentAccountSubGroupId = await _context.AccountNatures.Where(x => x.AccountName.ToLower() == "account receivables" && x.OrgId == entity.OrgId)
                //        .Select(s => s.AccountNatureId).FirstOrDefaultAsync();
                //    var _student = await _context.Students.Where(x => x.StudentId == entity.StudentId).Select(s => new { s.FirstName, s.LastName }).FirstOrDefaultAsync();

                //    var _lastName = _student.LastName == null || _student.LastName.Length == 0 ? "" : " " + _student.LastName;

                //    var _studentLedger = new GeneralLedger()
                //    {
                //        GeneralLedgerName = _student.FirstName + _lastName + "-" + entity.AdmissionNo,
                //        StudentClassId = entity.StudentClassId,
                //        OrgId = entity.OrgId,
                //        Active = 1,
                //        BatchId=entity.BatchId,
                //        AccountNatureId = _studentAccountNatureId,
                //        AccountGroupId = _studentAccountGroupId,
                //        AccountSubGroupId = _studentAccountSubGroupId,
                //        Deleted = false,
                //        CreatedDate = DateTime.Now
                //    };
                //    _context.GeneralLedgers.Add(_studentLedger);
                //}
                await _context.SaveChangesAsync();
                tran.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                tran.Rollback();
                if (!StudentClassExists(key))
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
                tran.Rollback();
                return BadRequest(ex);
            }
            return Updated(entity);
        }
        // POST: api/StudentClasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentClass>> PostStudentClass([FromBody] StudentClass studentClass)
        {
            var _existing = await _context.StudentClasses.Where(x => x.StudentId == studentClass.StudentId
            && x.ClassId == studentClass.ClassId
            && x.SemesterId == studentClass.SemesterId
            && x.BatchId == studentClass.BatchId
            && x.OrgId == studentClass.OrgId
            && x.SubOrgId == studentClass.SubOrgId
            ).ToListAsync();
            if (_existing.Count > 0)
            {
                return BadRequest(studentClass.StudentId + "-Student already exist in the same batch.");
            }
            try
            {

                if (studentClass.RollNo.Trim() != "")
                {
                    var _RollNO = await _context.StudentClasses.Where(x =>
                        x.OrgId == studentClass.OrgId
                        && x.SubOrgId == studentClass.SubOrgId
                        && x.ClassId == studentClass.ClassId
                        && x.SectionId == studentClass.SectionId
                        && x.SemesterId == studentClass.SemesterId
                        && x.BatchId == studentClass.BatchId
                        && x.RollNo == studentClass.RollNo).Select(s => s.StudentClassId).FirstOrDefaultAsync();
                    if (_RollNO > 0)
                        return BadRequest(studentClass.RollNo + "-Roll no. already exist.");
                }

                _context.StudentClasses.Add(studentClass);
                await _context.SaveChangesAsync();

                var defaultFeeTypeId = 0;

                if (studentClass.FeeTypeId !=null && studentClass.FeeTypeId > 0)
                    defaultFeeTypeId = (short)studentClass.FeeTypeId;
                else
                {
                    defaultFeeTypeId = await _context.SchoolFeeTypes.Where(x => x.DefaultType == 1
                                        && x.BatchId == studentClass.BatchId).Select(s => s.FeeTypeId).FirstOrDefaultAsync();
                }


                var studentfeetype = new StudentFeeType()
                {
                    Active = true,
                    BatchId = studentClass.BatchId,
                    CreatedBy = studentClass.CreatedBy,
                    CreatedDate = studentClass.CreatedDate,
                    IsCurrent = true,
                    Deleted = false,
                    FeeTypeId = (short)defaultFeeTypeId,
                    StudentClassId = studentClass.StudentClassId,
                    FromMonth = 0,
                    ToMonth = 0,
                    OrgId = studentClass.OrgId,
                    SubOrgId = studentClass.SubOrgId,
                    StudentFeeTypeId = 0
                };

                _context.StudentFeeTypes.Add(studentfeetype);

                var allstudentcls = await _context.StudentClasses.Where(x => x.StudentId == studentClass.StudentId
                && x.StudentClassId != studentClass.StudentClassId).ToListAsync();
                foreach (var item in allstudentcls)
                {
                    item.IsCurrent = false;
                    _context.Update(item);
                }

                //checking if the batchid is the current batchid then only update students
                var _IsCurrentBatchId = await _context.Batches.Where(x => x.CurrentBatch == 1
                && x.BatchId == studentClass.BatchId
                && x.OrgId == studentClass.OrgId).FirstOrDefaultAsync();
                if (_IsCurrentBatchId != null)
                {
                    var _student = await _context.Students.Where(x => x.StudentId == studentClass.StudentId).FirstOrDefaultAsync();
                    _student.BatchId = studentClass.BatchId;
                    _student.ClassId = studentClass.ClassId;
                    _context.Students.Update(_student);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            //var _studentAccountNatureId = await _context.AccountNatures.Where(x => x.AccountName.ToLower() == "assets" && x.OrgId == 0 && x.Active == true)
            //                        .Select(s => s.AccountNatureId).FirstOrDefaultAsync();
            //var _studentAccountGroupId = await _context.AccountNatures.Where(x => x.AccountName.ToLower() == "current assets" && x.OrgId == studentClass.OrgId)
            //    .Select(s => s.AccountNatureId).FirstOrDefaultAsync();
            //var _studentAccountSubGroupId = await _context.AccountNatures.Where(x => x.AccountName.ToLower() == "account receivables" && x.OrgId == studentClass.OrgId)
            //    .Select(s => s.AccountNatureId).FirstOrDefaultAsync();
            //var _student = await _context.Students.Where(x => x.StudentId == studentClass.StudentId).Select(s => new { s.FirstName, s.LastName }).FirstOrDefaultAsync();
            //var _lastName = _student.LastName == null || _student.LastName.Length == 0 ? "" : " " + _student.LastName;

            //var _studentLedger = new GeneralLedger()
            //{
            //    GeneralLedgerName = _student.FirstName + _lastName + "-" + studentClass.AdmissionNo,
            //    StudentClassId = studentClass.StudentClassId,
            //    OrgId = studentClass.OrgId,
            //    Active = 1,
            //    BatchId = studentClass.BatchId,
            //    AccountNatureId = _studentAccountNatureId,
            //    AccountGroupId = _studentAccountGroupId,
            //    AccountSubGroupId = _studentAccountSubGroupId,
            //    Deleted = false,
            //    CreatedDate = DateTime.Now
            //};
            //_context.GeneralLedgers.Add(_studentLedger);
            //await _context.SaveChangesAsync();
            return Ok(studentClass);

        }
        //[HttpPost]
        //public async Task<ActionResult<ExamStudentResult>> Promote([FromBody] Promote promotedata)
        //{
        //    var nextClassId = 0;
        //    int SequenceId = 0;
        //    var studentgradeId = _context.MasterItems.Where(x => x.MasterDataName.ToLower() == "student grade").Select(s => s.MasterDataId).FirstOrDefault();
        //    List<ClassMaster> classMaster = await _context.ClassMasters.Where(x => x.BatchId == promotedata.CurrentBatchId && x.Active == 1).OrderBy(o => o.Sequence).ToListAsync();
        //    StudentClass studentcls;
        //    var AllStudents = await _context.StudentClasses
        //        .Join(_context.ExamStudentResults, sc => sc.StudentClassId, ex => ex.StudentClassId, (sc, ex) => new { sc, ex })
        //        .Join(_context.StudentGrades, g => g.ex.Grade, r => r.StudentGradeId, (g, r) => new { g, r })
        //        .Where(x => x.g.sc.Active == 1
        //        && x.g.ex.ExamId == promotedata.ExamId
        //        //&& x.g.ex.Grade == Convert.ToInt32(promotedata.FailGradeId)
        //        && x.r.ParentId == studentgradeId)
        //        .Select(s => new { sc = s.g.sc, grade = s.g.ex.Grade }).ToListAsync();
        //    using var tran = _context.Database.BeginTransaction();
        //    try
        //    {
        //        foreach (var student in AllStudents)
        //        {
        //            nextClassId = 0;
        //            SequenceId = 0;
        //            var promotedstudent = await _context.StudentClasses.Where(x => x.Active == 1
        //            && x.StudentId == student.sc.StudentId
        //            && student.sc.BatchId == promotedata.NextBatchId).Select(s => new { s.StudentClassId }).ToListAsync();
        //            if (promotedstudent.Count() == 0)
        //            {
        //                SequenceId = classMaster.FindIndex(f => f.ClassId == student.sc.ClassId);
        //                nextClassId = classMaster[SequenceId + 1].ClassId;
        //                studentcls = new StudentClass();
        //                studentcls = student.sc;
        //                studentcls.StudentClassId = 0;
        //                if (student.grade != Convert.ToInt32(promotedata.FailGradeId))
        //                    studentcls.ClassId = nextClassId;
        //                studentcls.BatchId = promotedata.NextBatchId;
        //                //studentcls.AdmissionDate = 
        //                _context.StudentClasses.Add(studentcls);
        //            }
        //            _context.SaveChanges();
        //        }
        //        tran.Commit();
        //        return Ok();
        //    }
        //    catch(Exception ex)
        //    {
        //        tran.Rollback();
        //        throw;
        //    }
        //}
        // DELETE: api/StudentClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentClass(int id)
        {
            var studentClass = await _context.StudentClasses.FindAsync(id);
            if (studentClass == null)
            {
                return NotFound();
            }

            _context.StudentClasses.Remove(studentClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentClassExists(int id)
        {
            return _context.StudentClasses.Any(e => e.StudentClassId == id);
        }
    }
}
