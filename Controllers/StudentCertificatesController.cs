using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class StudentCertificatesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentCertificatesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentCertificates
        [HttpGet]
        public IQueryable<StudentCertificate> GetStudentCertificates()
        {
            return _context.StudentCertificates.AsQueryable().AsNoTracking();
        }

        // GET: api/StudentCertificates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentCertificate>> GetStudentCertificate(int id)
        {
            var studentCertificate = await _context.StudentCertificates.FindAsync(id);

            if (studentCertificate == null)
            {
                return NotFound();
            }

            return studentCertificate;
        }

        // PUT: api/StudentCertificates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentCertificate(int id, StudentCertificate studentCertificate)
        {
            if (id != studentCertificate.StudentCertificateId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(studentCertificate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCertificateExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentCertificate> studentCertificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentCertificates.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            studentCertificate.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCertificateExists(key))
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
        // POST: api/StudentCertificates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentCertificate>> PostStudentCertificate([FromBody]StudentCertificate studentCertificate)
        {
            _context.StudentCertificates.Add(studentCertificate);
            await _context.SaveChangesAsync();

            return Ok(studentCertificate);
        }

        // DELETE: api/StudentCertificates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentCertificate(int id)
        {
            var studentCertificate = await _context.StudentCertificates.FindAsync(id);
            if (studentCertificate == null)
            {
                return NotFound();
            }

            _context.StudentCertificates.Remove(studentCertificate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentCertificateExists(int id)
        {
            return _context.StudentCertificates.Any(e => e.StudentCertificateId == id);
        }
    }
}
