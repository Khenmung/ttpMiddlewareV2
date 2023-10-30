using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseYearSemestersController : ControllerBase
    {
        private readonly ttpauthContext _context;

        public CourseYearSemestersController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/CourseYearSemesters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseYearSemester>>> GetCourseYearSemesters()
        {
            return await _context.CourseYearSemesters.ToListAsync();
        }

        // GET: api/CourseYearSemesters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseYearSemester>> GetCourseYearSemester(int id)
        {
            var courseYearSemester = await _context.CourseYearSemesters.FindAsync(id);

            if (courseYearSemester == null)
            {
                return NotFound();
            }

            return courseYearSemester;
        }

        // PUT: api/CourseYearSemesters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseYearSemester(int id, CourseYearSemester courseYearSemester)
        {
            if (id != courseYearSemester.CourseYearSemesterId)
            {
                return BadRequest();
            }

            _context.Entry(courseYearSemester).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseYearSemesterExists(id))
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

        // POST: api/CourseYearSemesters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseYearSemester>> PostCourseYearSemester(CourseYearSemester courseYearSemester)
        {
            _context.CourseYearSemesters.Add(courseYearSemester);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseYearSemesterExists(courseYearSemester.CourseYearSemesterId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourseYearSemester", new { id = courseYearSemester.CourseYearSemesterId }, courseYearSemester);
        }

        // DELETE: api/CourseYearSemesters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseYearSemester(int id)
        {
            var courseYearSemester = await _context.CourseYearSemesters.FindAsync(id);
            if (courseYearSemester == null)
            {
                return NotFound();
            }

            _context.CourseYearSemesters.Remove(courseYearSemester);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseYearSemesterExists(int id)
        {
            return _context.CourseYearSemesters.Any(e => e.CourseYearSemesterId == id);
        }
    }
}
