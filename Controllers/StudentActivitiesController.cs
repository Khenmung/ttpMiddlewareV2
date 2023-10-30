
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ttpMiddleware.Models;
//using Microsoft.AspNet.OData;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNet.OData.Routing;
//using Newtonsoft.Json.Linq;
//using System.Reflection;

//using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
//{
//    [ODataRoutePrefix("[controller]")]
//    [EnableQuery]
//    public class StudentActivitiesController : ProtectedController
//    {
//        private readonly ttpauthContext _context;

//        public StudentActivitiesController(ttpauthContext context)
//        {
//            _context = context;
//        }

//        // GET: api/StudentActivities
//        [HttpGet]
//        public IQueryable<StudentActivity> GetStudentActivities()
//        {
//            return _context.StudentActivities.AsQueryable().AsNoTracking();
//        }

//        // GET: api/StudentActivities/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<StudentActivity>> GetStudentActivity(short id)
//        {
//            var studentActivity = await _context.StudentActivities.FindAsync(id);

//            if (studentActivity == null)
//            {
//                return NotFound();
//            }

//            return studentActivity;
//        }

//        // PUT: api/StudentActivities/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutStudentActivity(short id, StudentActivity studentActivity)
//        {
//            if (id != studentActivity.StudentActivityId)
//            {
//                return (IActionResult)BadRequest();
//            }

//            _context.Entry(studentActivity).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!StudentActivityExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }
//        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<StudentActivity> studentActivity)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var entity = await _context.StudentActivities.FindAsync(key);
//            if (entity == null)
//            {
//                return NotFound();
//            }
//            studentActivity.Patch(entity);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!StudentActivityExists(key))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return Updated(entity);
//        }
//        public async Task<ActionResult<StudentActivity>> Post([FromBody] JArray jsonWrapper)
//        {
//            using var tran = _context.Database.BeginTransaction();
//            try
//            {
//                JToken jsonValues = jsonWrapper;
//                StudentActivity _studentActivity = null ;
                
//                foreach (var x in jsonValues)
//                {
//                    _studentActivity = x.ToObject<StudentActivity>();
//                    if (_studentActivity.StudentActivityId > 0)
//                    {
//                        var existingstudent = await _context.StudentActivities.Where(x => x.StudentActivityId == _studentActivity.StudentActivityId).FirstOrDefaultAsync();

//                        foreach (PropertyInfo prop in existingstudent.GetType().GetProperties())
//                        {
//                            if (prop.GetValue(_studentActivity, null) != null)
//                                prop.SetValue(existingstudent, prop.GetValue(_studentActivity, null));
//                        }

//                        _context.StudentActivities.Update(existingstudent);
//                        _context.SaveChanges();

//                    }
//                    else
//                    {                        
//                        _context.StudentActivities.Add(_studentActivity);
//                        await _context.SaveChangesAsync();
//                    }                    
//                }
//                tran.Commit();

//                return Ok(_studentActivity);
               
//            }
//            catch (Exception ex)
//            {
//                tran.Rollback();
//                throw;
//            }
//        }
//        //// POST: api/StudentActivities
//        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        //[HttpPost]
//        //public async Task<ActionResult<StudentActivity>> PostStudentActivity([FromBody]StudentActivity studentActivity)
//        //{
//        //    _context.StudentActivities.Add(studentActivity);
//        //    await _context.SaveChangesAsync();

//        //    return Ok(studentActivity);
//        //}

//        // DELETE: api/StudentActivities/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteStudentActivity(short id)
//        {
//            var studentActivity = await _context.StudentActivities.FindAsync(id);
//            if (studentActivity == null)
//            {
//                return NotFound();
//            }

//            _context.StudentActivities.Remove(studentActivity);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool StudentActivityExists(int id)
//        {
//            return _context.StudentActivities.Any(e => e.StudentActivityId == id);
//        }
//    }
//}
