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
//    public class EmployeeGeneratedCertificatesController : ProtectedController
//    {
//        private readonly ttpauthContext _context;

//        public EmployeeGeneratedCertificatesController(ttpauthContext context)
//        {
//            _context = context;
//        }

//        // GET: api/EmployeeGeneratedCertificates
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<EmployeeGeneratedCertificate>>> GetEmployeeGeneratedCertificates()
//        {
//            return await _context.EmployeeGeneratedCertificates.ToListAsync();
//        }

//        // GET: api/EmployeeGeneratedCertificates/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<EmployeeGeneratedCertificate>> GetEmployeeGeneratedCertificate(int id)
//        {
//            var employeeGeneratedCertificate = await _context.EmployeeGeneratedCertificates.FindAsync(id);

//            if (employeeGeneratedCertificate == null)
//            {
//                return NotFound();
//            }

//            return employeeGeneratedCertificate;
//        }

//        // PUT: api/EmployeeGeneratedCertificates/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutEmployeeGeneratedCertificate(int id, EmployeeGeneratedCertificate employeeGeneratedCertificate)
//        {
//            if (id != employeeGeneratedCertificate.EmployeeGeneratedCertificateId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(employeeGeneratedCertificate).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!EmployeeGeneratedCertificateExists(id))
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
//        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmployeeGeneratedCertificate> employeeGeneratedCertificate)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var entity = await _context.EmployeeGeneratedCertificates.FindAsync(key);
//            if (entity == null)
//            {
//                return NotFound();
//            }
//            employeeGeneratedCertificate.Patch(entity);
//            try
//            {
//                await _context.SaveChangesAsync();
//                return Updated(entity);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }
//        // POST: api/EmployeeGeneratedCertificates
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<EmployeeGeneratedCertificate>> PostEmployeeGeneratedCertificate([FromBody]EmployeeGeneratedCertificate employeeGeneratedCertificate)
//        {
//            if(!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            try
//            {
//                _context.EmployeeGeneratedCertificates.Add(employeeGeneratedCertificate);
//                await _context.SaveChangesAsync();

//                return Ok(employeeGeneratedCertificate);
//            }
//            catch(Exception ex)
//            {
//                return BadRequest(ex);
//            }
            
//        }

//        // DELETE: api/EmployeeGeneratedCertificates/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteEmployeeGeneratedCertificate(int id)
//        {
//            var employeeGeneratedCertificate = await _context.EmployeeGeneratedCertificates.FindAsync(id);
//            if (employeeGeneratedCertificate == null)
//            {
//                return NotFound();
//            }

//            _context.EmployeeGeneratedCertificates.Remove(employeeGeneratedCertificate);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool EmployeeGeneratedCertificateExists(int id)
//        {
//            return _context.EmployeeGeneratedCertificates.Any(e => e.EmployeeGeneratedCertificateId == id);
//        }
//    }
//}
