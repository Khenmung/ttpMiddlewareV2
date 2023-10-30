//using System.Linq;
//using System.Threading.Tasks;
//using ttpMiddleware.Models;
//using Microsoft.AspNet.OData;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using ttpMiddleware.CommonFunctions;

//namespace ttpMiddlewareV2.Controllers
//{
//    [Route("[controller]")]
//    [EnableQuery]
//    public class FeePaymentAndStatusController : ProtectedController
//    {
//        private readonly ttpauthContext _context;

//        public FeePaymentAndStatusController(ttpauthContext context)
//        {
//            _context = context;
//        }

//        // GET: api/FeePaymentAndStatus
//        [HttpGet]
//        public IQueryable<FeePaymentAndStatus> GetFeePaymentAndStatuses()
//        {
//            return _context.FeePaymentAn.AsNoTracking().AsQueryable();
//        }

//        // GET: api/FeePaymentAndStatus/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<FeePaymentAndStatus>> GetFeePaymentAndStatus(int id)
//        {
//            var feePaymentAndStatus = await _context.FeePaymentAndStatuses.FindAsync(id);

//            if (feePaymentAndStatus == null)
//            {
//                return NotFound();
//            }

//            return feePaymentAndStatus;
//        }

//        // PUT: api/FeePaymentAndStatus/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutFeePaymentAndStatus(int id, FeePaymentAndStatus feePaymentAndStatus)
//        {
//            if (id != feePaymentAndStatus.FeepaymentAndStatusId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(feePaymentAndStatus).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!FeePaymentAndStatusExists(id))
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
//        [HttpPatch]
//        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<FeePaymentAndStatus> feePaymentAndStatus)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var entity = await _context.FeePaymentAndStatuses.FindAsync(key);
//            if (entity == null)
//            {
//                return NotFound();
//            }
//            feePaymentAndStatus.Patch(entity);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!FeePaymentAndStatusExists(key))
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
//        // POST: api/FeePaymentAndStatus
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<FeePaymentAndStatus>> PostFeePaymentAndStatus([FromBody] FeePaymentAndStatus feePaymentAndStatus)
//        {
//            _context.FeePaymentAndStatuses.Add(feePaymentAndStatus);
//            await _context.SaveChangesAsync();

//            return Ok(feePaymentAndStatus);
//        }

//        // DELETE: api/FeePaymentAndStatus/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteFeePaymentAndStatus(int id)
//        {
//            var feePaymentAndStatus = await _context.FeePaymentAndStatuses.FindAsync(id);
//            if (feePaymentAndStatus == null)
//            {
//                return NotFound();
//            }

//            _context.FeePaymentAndStatuses.Remove(feePaymentAndStatus);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool FeePaymentAndStatusExists(int id)
//        {
//            return _context.FeePaymentAndStatuses.Any(e => e.FeepaymentAndStatusId == id);
//        }
//    }
//}
