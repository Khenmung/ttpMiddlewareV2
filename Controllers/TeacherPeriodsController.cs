//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNet.OData;
//using Microsoft.AspNet.OData.Routing;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ttpMiddleware.CommonFunctions;
//using ttpMiddleware.Models;

//namespace ttpMiddleware.Controllers
//{
//    [ODataRoutePrefix("[controller]")]
//    [EnableQuery]
//    public class TeacherPeriodsController : ProtectedController
//    {
//        private readonly ttpauthContext _context;

//        public TeacherPeriodsController(ttpauthContext context)
//        {
//            _context = context;
//        }

//        // GET: api/TeacherPeriods
//        [HttpGet]
//        public IQueryable<TeacherPeriod> GetTeacherPeriods()
//        {
//            return _context.TeacherPeriods.AsQueryable().AsNoTracking();
//        }

//        // GET: api/TeacherPeriods/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<TeacherPeriod>> GetTeacherPeriod(int id)
//        {
//            var teacherPeriod = await _context.TeacherPeriods.FindAsync(id);

//            if (teacherPeriod == null)
//            {
//                return NotFound();
//            }

//            return teacherPeriod;
//        }

//        // PUT: api/TeacherPeriods/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutTeacherPeriod(int id, TeacherPeriod teacherPeriod)
//        {
//            if (id != teacherPeriod.TeacherPeriodId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(teacherPeriod).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!TeacherPeriodExists(id))
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
//        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<TeacherPeriod> teacherPeriod)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var entity = await _context.TeacherPeriods.FindAsync(key);
//            if (entity == null)
//            {
//                return NotFound();
//            }
//            teacherPeriod.Patch(entity);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                if (!TeacherPeriodExists(key))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    return BadRequest(ex);
//                }
//            }

//            return Updated(entity);
//        }
//        // POST: api/TeacherPeriods
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<TeacherPeriod>> PostTeacherPeriod([FromBody] TeacherPeriod teacherPeriod)
//        {
//            try
//            {
//                _context.TeacherPeriods.Add(teacherPeriod);
//                await _context.SaveChangesAsync();

//                return Ok(teacherPeriod);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }

//        }

//        // DELETE: api/TeacherPeriods/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTeacherPeriod(int id)
//        {
//            var teacherPeriod = await _context.TeacherPeriods.FindAsync(id);
//            if (teacherPeriod == null)
//            {
//                return NotFound();
//            }

//            _context.TeacherPeriods.Remove(teacherPeriod);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool TeacherPeriodExists(int id)
//        {
//            return _context.TeacherPeriods.Any(e => e.TeacherPeriodId == id);
//        }
//    }
//}
