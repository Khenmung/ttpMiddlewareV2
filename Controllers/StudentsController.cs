using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using System;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using ttpMiddleware.Data.Entities;
using Microsoft.Extensions.Configuration;
using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    /// <summary>
    /// Student controller
    /// </summary>
    [ODataRoutePrefix("Students")]

    public class StudentsController : ProtectedController
    {
        private IConfiguration _configuration;
        private readonly ttpauthContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        public StudentsController(
            IConfiguration config,
            ttpauthContext context,
          UserManager<ApplicationUser> userManager)
        {
            _configuration = config;
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// query student
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        public IQueryable<Student> Get()
        {
            return _context.Students.AsNoTracking().AsQueryable();
        }
        /// <summary>
        /// Add student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<ActionResult<Student>> Post([FromBody] JArray jsonWrapper)
        {
            //var _errormessage = "";


            JToken jsonValues = jsonWrapper;
            Student _student = new Student();
            StudentClass _studentclass = new StudentClass();
            int _ClassId = 0;
            //List<Student> _StudentList = new List<Student>();
            try
            {
                short defaultFeeTypeId = 0;
                bool isThisCurrentBatch = false;
                var _batchName = "";
                short _feetypeId = 0;
                int _admittedStatusId = 0;
                using var tran = _context.Database.BeginTransaction();
                int StudentFeeTypeId = 0;
                foreach (var x in jsonValues)
                {
                    _student = x.ToObject<Student>();
                    if (_batchName == "")
                    {
                        var _batchobj = await _context.Batches.Where(x => x.BatchId == _student.BatchId
                        && x.OrgId == _student.OrgId
                        //&& x.SubOrgId == _student.SubOrgId //suborgid not required with batches table.
                        ).FirstOrDefaultAsync();
                        if (_batchobj != null)
                        {
                            _batchName = _batchobj.BatchName;
                            isThisCurrentBatch = _batchobj.CurrentBatch == 1 ? true : false;
                        }
                    }
                    //var _feetypeId = _context.SchoolFeeTypes.Where(x => x.DefaultType == 1
                    //&& x.OrgId == _student.OrgId
                    //&& x.SubOrgId == _student.SubOrgId
                    //).Select(s => s.FeeTypeId).FirstOrDefault();

                    if (_student.FeeTypeId > 0)
                    {
                        _feetypeId = _student.FeeTypeId;
                    }
                    else
                    {
                        if (defaultFeeTypeId > 0)
                            _feetypeId = defaultFeeTypeId;
                        else
                        {
                            defaultFeeTypeId = _context.SchoolFeeTypes.Where(x => x.DefaultType == 1
                            && x.OrgId == _student.OrgId
                            && x.SubOrgId == _student.SubOrgId).Select(s => s.FeeTypeId).FirstOrDefault();
                            _feetypeId = defaultFeeTypeId;
                        }
                    }
                    try
                    {
                        var _lastName = _student.LastName == null || _student.LastName.Length == 0 ? "" : " " + _student.LastName;
                        if (_student.StudentId > 0)
                        {
                            var existingstudent = await _context.Students.Where(x => x.StudentId == _student.StudentId).ToListAsync();
                            if (existingstudent.Count == 0)
                            {
                                return BadRequest("No student with Id : " + _student.StudentId + " found");
                            }
                            else
                            {
                                foreach (var stud in existingstudent)
                                {
                                    foreach (PropertyInfo prop in stud.GetType().GetProperties())
                                    {
                                        if (prop.GetValue(_student, null) != null)
                                            prop.SetValue(stud, prop.GetValue(_student, null));
                                    }

                                    _context.Students.Update(stud);
                                }
                                await _context.SaveChangesAsync();
                            }
                        }
                        else //new student
                        {
                            var existingstudent = await _context.Students.Where(x => x.FirstName == _student.FirstName
                            && x.LastName == _student.LastName
                            && x.FatherName == _student.FatherName
                            && x.OrgId == _student.OrgId
                            && x.SubOrgId == _student.SubOrgId
                            ).ToListAsync();
                            if (existingstudent.Count > 0)
                                return BadRequest("Same student name and father's name already exists. firstname: " + _student.FirstName + ", fathername: " + _student.FatherName);
                            else
                            {

                                _context.Students.Add(_student);
                                await _context.SaveChangesAsync();

                                _ClassId = Convert.ToInt32(_student.ClassAdmissionSought);
                                if (_admittedStatusId == 0)
                                {
                                    _admittedStatusId = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == "admitted"
                                                             && x.OrgId == _student.OrgId
                                                             && x.SubOrgId == _student.SubOrgId
                                                             ).Select(s => s.MasterDataId).FirstOrDefaultAsync();
                                }

                                var _duplicateId = await _context.StudentClasses.Where(x => x.ClassId == _ClassId
                                && x.StudentId == _student.StudentId && x.BatchId == _student.BatchId).FirstOrDefaultAsync();//.Select(x => x.StudentId).FirstOrDefault();

                                bool _isAdmitted = _admittedStatusId == _student.AdmissionStatusId;

                                if (_duplicateId == null && _ClassId != 0)
                                {
                                    //var _studentcount = await _context.StudentClasses.Where(x =>
                                    //x.OrgId == _student.OrgId
                                    //&& x.SubOrgId == _student.SubOrgId
                                    //&& x.BatchId == _student.BatchId)
                                    //    .Select(x => x.StudentId).ToListAsync();
                                    //var _count = _studentcount.Count + 1;
                                    //var _admissionno = _batchName.Split('-')[0] + _count.ToString();
                                    _studentclass = new StudentClass()
                                    {
                                        AdmissionNo = "",// _admissionno,
                                        StudentId = _student.StudentId,
                                        ClassId = _ClassId,
                                        Active = 1,
                                        Admitted = _isAdmitted,
                                        FeeTypeId = _feetypeId,
                                        IsCurrent = isThisCurrentBatch,
                                        BatchId = (short)_student.BatchId,
                                        SemesterId = _student.SemesterId,
                                        SectionId = _student.SectionId,
                                        RollNo = _student.RollNo,
                                        OrgId = _student.OrgId,
                                        SubOrgId = _student.SubOrgId,
                                        Remarks = _student.Notes,
                                        AdmissionDate = _student.AdmissionDate == null ? DateTime.Now : _student.AdmissionDate,
                                        CreatedDate = DateTime.Now
                                    };

                                    _context.StudentClasses.Add(_studentclass);

                                    await _context.SaveChangesAsync();

                                    
                                    var studentfeetype = new StudentFeeType()
                                    {
                                        Active = true,
                                        BatchId = (short)_student.BatchId,
                                        CreatedBy = _student.CreatedBy,
                                        CreatedDate = _student.CreatedDate,
                                        IsCurrent = true,
                                        Deleted = false,
                                        FeeTypeId = _feetypeId,
                                        StudentClassId = _studentclass.StudentClassId,
                                        FromMonth = 0,
                                        ToMonth = 0,
                                        OrgId = _student.OrgId,
                                        SubOrgId = _student.SubOrgId,
                                        StudentFeeTypeId = 0
                                    };

                                    _context.StudentFeeTypes.Add(studentfeetype);
                                    await _context.SaveChangesAsync();
                                    StudentFeeTypeId = studentfeetype.StudentFeeTypeId;
                                    ////////////////////

                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //tran.Rollback();
                        return BadRequest(ex);
                    }
                }
                tran.Commit();
                var studwithCls = new
                {
                    PID = _student.PID,
                    StudentId = _student.StudentId,
                    StudentClassId = _studentclass.StudentClassId,
                    ClassId = _studentclass.ClassId,
                    Active = _studentclass.Active,
                    Admitted = _studentclass.Admitted,
                    FeeTypeId = _studentclass.FeeTypeId,
                    BatchId = _studentclass.BatchId,
                    SemesterId = _student.SemesterId,
                    SectionId = _student.SectionId,
                    RollNo = _student.RollNo,
                    OrgId = _student.OrgId,
                    SubOrgId = _student.SubOrgId,
                    Remarks = _student.Notes,
                    StudentFeeTypeId = StudentFeeTypeId,
                    AdmissionDate = _student.AdmissionDate,
                    CreatedDate = DateTime.Now
                };
                return Ok(studwithCls);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        /// <summary>
        /// update specific properties
        /// </summary>
        /// <param name="key"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Student> student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.Students.FindAsync(key);
            var _existingemail = entity.EmailAddress;
            if (entity == null)
            {
                return NotFound();
            }
            student.Patch(entity);
            using var tran = _context.Database.BeginTransaction();

            try
            {
                var _lastName = entity.LastName == null || entity.LastName.Length == 0 ? "" : " " + entity.LastName;
                short _feetypeId = 0;
                if (entity.FeeTypeId > 0)
                {
                    _feetypeId = entity.FeeTypeId;
                }
                else
                {
                    _feetypeId = _context.SchoolFeeTypes.Where(x => x.DefaultType == 1
                    && x.OrgId == entity.OrgId
                    && x.SubOrgId == entity.SubOrgId
                    ).Select(s => s.FeeTypeId).FirstOrDefault();
                }
                var studcls = _context.StudentClasses.Where(x => x.StudentId == entity.StudentId
                && x.BatchId == entity.BatchId
                && x.OrgId == entity.OrgId
                && x.SubOrgId == entity.SubOrgId
                ).FirstOrDefault();

                var _masterAdmittedStatusId = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == "admitted"
                                                     && x.OrgId == entity.OrgId
                                                     && x.SubOrgId == entity.SubOrgId
                                                     ).Select(s => s.MasterDataId).FirstOrDefaultAsync();

                var _admissionStatusId = _masterAdmittedStatusId == entity.AdmissionStatusId ? true : false;
                var _active = 0;// entity.Active == 0 ? 0 : _admissionStatusId;
                if (studcls != null)
                {
                    studcls.Active = (byte)entity.Active;

                    //cant say student is admitted even though he is active
                    if (entity.Active == 1)
                        studcls.IsCurrent = true;
                    else
                    {
                        //if inactive, admitted should be false;
                        studcls.Admitted = false;
                    }


                    //studcls.AdmissionDate = entity.AdmissionDate;
                    _context.StudentClasses.Update(studcls);


                    //only if previous email id is empty //if (userexist != null)
                    if (entity.EmailAddress != null && entity.EmailAddress.Length > 0)
                    {
                        var userexist = await _userManager.FindByIdAsync(entity.UserId);
                        if (userexist != null)
                        {
                            userexist.Email = entity.EmailAddress;
                            await _userManager.UpdateAsync(userexist);
                        }

                    }
                }
                else
                {

                    //new student
                    if (entity.ClassAdmissionSought > 0)// && _admissionStatusId == entity.AdmissionStatusId)
                    {
                        var _studentcount = _context.StudentClasses.Where(x =>
                        x.OrgId == entity.OrgId
                        && x.SubOrgId == entity.SubOrgId
                        && x.BatchId == entity.BatchId).Count();//.Select(x => x.StudentId).ToListAsync();
                        var _count = _studentcount + 1;

                        var _batchName = await _context.Batches.Where(x => x.BatchId == entity.BatchId
                        && x.OrgId == entity.OrgId
                        //&& x.SubOrgId == entity.SubOrgId // batch is not related to suborgid
                        ).Select(s => s.BatchName).FirstOrDefaultAsync();
                        var _admissionno = _batchName.Split('-')[0] + _count.ToString();
                        var _studentclass = new StudentClass()
                        {
                            AdmissionNo = _admissionno,
                            StudentId = entity.StudentId,
                            ClassId = (int)entity.ClassAdmissionSought,
                            SectionId = entity.SectionId,
                            SemesterId = entity.SemesterId,
                            Active = (byte)_active,//(byte)_admissionStatusId,
                            Admitted = _admissionStatusId,
                            FeeTypeId = _feetypeId,
                            BatchId = (short)entity.BatchId,
                            OrgId = entity.OrgId,
                            SubOrgId = entity.SubOrgId,
                            AdmissionDate = entity.AdmissionDate,
                            CreatedDate = DateTime.Now
                        };
                        _context.StudentClasses.Add(_studentclass);
                        await _context.SaveChangesAsync();

                        var defaultFeeTypeId = await _context.SchoolFeeTypes.Where(x => x.DefaultType == 1
                        && x.BatchId == entity.BatchId).Select(s => s.FeeTypeId).FirstOrDefaultAsync();

                        var studentfeetype = new StudentFeeType()
                        {
                            Active = true,
                            BatchId = (short)entity.BatchId,
                            CreatedBy = entity.CreatedBy,
                            CreatedDate = entity.CreatedDate,
                            IsCurrent = true,
                            Deleted = false,
                            FeeTypeId = defaultFeeTypeId,
                            StudentClassId = _studentclass.StudentClassId,
                            FromMonth = 0,
                            ToMonth = 0,
                            OrgId = entity.OrgId,
                            SubOrgId = entity.SubOrgId,
                            StudentFeeTypeId = 0
                        };

                        _context.StudentFeeTypes.Add(studentfeetype);

                    }
                }
                if (entity.Active == 0)
                    entity.BatchId = 0;


                await _context.SaveChangesAsync();
                tran.Commit();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                tran.Rollback();
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex);
            }
            return Updated(entity);
        }
        /// <summary>
        /// to update all properties
        /// </summary>
        /// <param name="key"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<IActionResult> Put([FromODataUri] int key, Student update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.StudentId)
            {
                return (IActionResult)BadRequest();
            }
            _context.Entry(update).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(update);
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var student = await _context.Students.FindAsync(key);
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
