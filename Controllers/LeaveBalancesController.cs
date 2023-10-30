using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;
using Newtonsoft.Json.Linq;
using System;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class LeaveBalancesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public LeaveBalancesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/LeaveBalances
        [HttpGet]
        public IQueryable<LeaveBalance> GetLeaveBalances()
        {
            try
            {
                return _context.LeaveBalances.AsQueryable().AsNoTracking();
            }
            catch(Exception ex)
            {
                return (IQueryable<LeaveBalance>)BadRequest(ex);
            }
        }

        // GET: api/LeaveBalances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveBalance>> GetLeaveBalance(int id)
        {
            var leaveBalance = await _context.LeaveBalances.FindAsync(id);

            if (leaveBalance == null)
            {
                return NotFound();
            }

            return leaveBalance;
        }

        // PUT: api/LeaveBalances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveBalance(int id, LeaveBalance leaveBalance)
        {
            if (id != leaveBalance.LeaveBalanceId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(leaveBalance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveBalanceExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<LeaveBalance> leaveBalance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.LeaveBalances.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            leaveBalance.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveBalanceExists(key))
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
        // POST: api/LeaveBalances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveBalance>> PostLeaveBalance([FromBody] JArray jsonWrapper)
        {
            //var _errormessage = "";


            JToken jsonValues = jsonWrapper;
            LeaveBalance _leaveBalance = new LeaveBalance();
            using var tran = _context.Database.BeginTransaction();
            try
            {

                foreach (var x in jsonValues)
                {
                    _leaveBalance = x.ToObject<LeaveBalance>();
                    if (_leaveBalance.LeaveBalanceId == 0)
                    {
                        var _leave = await _context.LeaveBalances.Where(x => x.EmployeeId == _leaveBalance.EmployeeId
                        && x.LeavePolicyId == _leaveBalance.LeavePolicyId
                        && x.BatchId == _leaveBalance.BatchId
                        && x.OrgId == _leaveBalance.OrgId
                        && x.SubOrgId == _leaveBalance.SubOrgId).ToListAsync();
                        if (!_leave.Any())
                        {
                            //var _BatchId = await _context.Batches.Where(x=>x.BatchId < _leaveBalance.BatchId).Select(s=>s.BatchId).ToListAsync();

                            //var _latestLeaveBalance = await _context.LeaveBalances.Where(x => x.EmployeeId == _leaveBalance.EmployeeId
                            //                       && x.LeavePolicyId == _leaveBalance.LeavePolicyId
                            //                       //&& x.BatchId == _leaveBalance.BatchId
                            //                       && x.OrgId == _leaveBalance.OrgId
                            //                       && x.SubOrgId == _leaveBalance.SubOrgId).OrderByDescending(x=>x.LeaveBalanceId).FirstOrDefaultAsync();
                            //if (_latestLeaveBalance != null)
                            //    _leaveBalance.OB += _latestLeaveBalance.CB;
                            _context.LeaveBalances.Add(_leaveBalance);
                        }

                    }
                    else if (_leaveBalance.LeaveBalanceId > 0)
                    {
                        var _leave = await _context.LeaveBalances.Where(x => x.LeaveBalanceId == _leaveBalance.LeaveBalanceId).FirstOrDefaultAsync();
                        if (_leave != null)
                        {
                            _leave.EmployeeId = _leaveBalance.EmployeeId;
                            _leave.DepartmentId = _leaveBalance.DepartmentId;
                            _leave.LeavePolicyId = _leaveBalance.LeavePolicyId;
                            _leave.OB = _leaveBalance.OB;
                            _leave.Adjusted = _leaveBalance.Adjusted;
                            _leave.CB = _leaveBalance.CB;
                            _leave.BatchId = _leaveBalance.BatchId;
                            _leave.OrgId = _leaveBalance.OrgId;
                            _leave.SubOrgId = _leaveBalance.SubOrgId;
                            _leave.Active = _leaveBalance.Active;
                            _context.Update(_leave);
                        }
                    }
                }
                await _context.SaveChangesAsync();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex);
            }
            return Ok(_leaveBalance);
        }

        // DELETE: api/LeaveBalances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveBalance(int id)
        {
            var leaveBalance = await _context.LeaveBalances.FindAsync(id);
            if (leaveBalance == null)
            {
                return NotFound();
            }

            _context.LeaveBalances.Remove(leaveBalance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveBalanceExists(int id)
        {
            return _context.LeaveBalances.Any(e => e.LeaveBalanceId == id);
        }
    }
}
