using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using ttpMiddleware.CommonFunctions;
using System;
using ttpMiddleware.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class LeaveEmployeeLeavesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public LeaveEmployeeLeavesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/LeaveEmployeeLeaves
        [HttpGet]
        public IQueryable<LeaveEmployeeLeaf> GetLeaveEmployeeLeaves()
        {
            return _context.LeaveEmployeeLeaves.AsQueryable().AsNoTracking();
        }

        // GET: api/LeaveEmployeeLeaves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveEmployeeLeaf>> GetLeaveEmployeeLeaf(int id)
        {
            var leaveEmployeeLeaf = await _context.LeaveEmployeeLeaves.FindAsync(id);

            if (leaveEmployeeLeaf == null)
            {
                return NotFound();
            }

            return leaveEmployeeLeaf;
        }

        // PUT: api/LeaveEmployeeLeaves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveEmployeeLeaf(int id, LeaveEmployeeLeaf leaveEmployeeLeaf)
        {
            if (id != leaveEmployeeLeaf.EmployeeLeaveId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(leaveEmployeeLeaf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveEmployeeLeafExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] JObject jsonWrapper)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //var entity = await _context.LeaveEmployeeLeaves.FindAsync(key);
            //if (entity == null)
            //{
            //    return NotFound();
            //}
            //leaveEmployeeLeaf.Patch(entity);
            // var tran = _context.Database.BeginTransaction();
            //try
            //{
            //   await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!LeaveEmployeeLeafExists(key))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}


            JToken jsonValues = jsonWrapper;
            LeaveEmployeeLeaf _employeeLeave = new LeaveEmployeeLeaf();
            List<LeaveBalance> _leaveBalances = new List<LeaveBalance>();
            // _employeeAttendance = new EmployeeAttendance();

            using var tran = _context.Database.BeginTransaction();
            try
            {

                foreach (JProperty x in jsonValues)
                {
                    if (x.Name == "LeaveBalance")
                        _leaveBalances = x.Value.ToObject<List<LeaveBalance>>();
                    else if (x.Name == "EmployeeLeave")
                        _employeeLeave = x.Value.ToObject<LeaveEmployeeLeaf>();
                    //else if (x.Name == "EmployeeAttendance")
                    //    _employeeAttendance = x.ToObject<EmployeeAttendance>();
                }
                if (_employeeLeave == null)
                {
                    tran.Rollback();
                    return BadRequest("employee leave null");
                }
                var _leaveParentStatusId = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == "leave status").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                var _leaveStatus = await _context.MasterItems.Where(x => x.MasterDataId == _employeeLeave.LeaveStatusId
                && x.ParentId == _leaveParentStatusId
                && x.OrgId == _employeeLeave.OrgId
                && x.SubOrgId == _employeeLeave.SubOrgId).Select(s => s.MasterDataName).FirstOrDefaultAsync();

                var _localEmpLeave = await _context.LeaveEmployeeLeaves.Where(x => x.EmployeeLeaveId == _employeeLeave.EmployeeLeaveId).FirstOrDefaultAsync();
                if (_localEmpLeave != null)
                {
                    _localEmpLeave.LeaveFrom= _employeeLeave.LeaveFrom;
                    _localEmpLeave.LeaveTo= _employeeLeave.LeaveTo;
                    _localEmpLeave.LeaveStatusId =_employeeLeave.LeaveStatusId;
                    _localEmpLeave.LeaveReason= _employeeLeave.LeaveReason;
                    _localEmpLeave.LeaveTypeId= _employeeLeave.LeaveTypeId;
                    _localEmpLeave.NoOfDays = _employeeLeave.NoOfDays;
                    _localEmpLeave.BatchId= _employeeLeave.BatchId;
                    _localEmpLeave.UpdatedBy= _employeeLeave.UpdatedBy;
                    _localEmpLeave.Active= _employeeLeave.Active;
                    if (_leaveStatus.ToLower() == "approved")
                    {
                        _localEmpLeave.ApprovedBy = _employeeLeave.ApprovedBy;
                        _localEmpLeave.ApproveRejectedDate = _employeeLeave.ApproveRejectedDate;
                    }
                    _context.Update(_localEmpLeave);
                }
                //_context.LeaveEmployeeLeaves.Add(_employeeLeave);
                

                if (_leaveStatus.ToLower() == "approved" && _employeeLeave.Active==1)
                {
                    var employeeAttendance = await _context.EmployeeAttendances.Where(x => x.EmployeeId == _employeeLeave.EmployeeId
                                           && x.AttendanceDate >= _employeeLeave.LeaveFrom
                                           && x.AttendanceDate <= _employeeLeave.LeaveTo
                                           && x.OrgId == _employeeLeave.OrgId
                                           && x.SubOrgId == _employeeLeave.SubOrgId
                                           && x.BatchId == _employeeLeave.BatchId).ToListAsync();

                    var _attParentStatusId = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == "attendance status")
                                               .Select(s => s.MasterDataId).FirstOrDefaultAsync();
                   
                    var _attStatusId = await _context.MasterItems.Where(x => x.MasterDataName.ToLower() == "l"
                    && x.ParentId == _attParentStatusId
                    && x.OrgId == _employeeLeave.OrgId
                    && x.SubOrgId == _employeeLeave.SubOrgId).Select(s => s.MasterDataId).FirstOrDefaultAsync();
                    if (employeeAttendance.Count() > 0)
                    {
                        foreach (var empattendance in employeeAttendance)
                        {
                            //empattendance.AttendanceStatusId = _attStatusId;
                            empattendance.Approved = true;
                            _context.Update(empattendance);
                        }
                    }
                    else
                    {
                        DateTime start = _employeeLeave.LeaveFrom;
                        DateTime end = _employeeLeave.LeaveTo.AddDays(1);
                        while (DateTime.Compare(start, end) <0)
                        {
                            var newAttendance = new EmployeeAttendance()
                            {
                                EmployeeAttendanceId = 0,
                                Approved = true,
                                Active = true,
                                AttendanceDate = start,
                                EmployeeId= (short)_employeeLeave.EmployeeId,
                                BatchId= _employeeLeave.BatchId,
                                //AttendanceStatusId=_attStatusId,
                                Deleted = false,
                                OrgId=_employeeLeave.OrgId,
                                SubOrgId = _employeeLeave.SubOrgId,
                                CreatedDate=DateTime.Now                                
                            };
                            _context.EmployeeAttendances.Add(newAttendance);
                            start = start.AddDays(1);
                        }
                    }
                    foreach (var _leaveBalance in _leaveBalances)
                    {
                        if (_leaveBalance.LeaveBalanceId == 0)
                        {
                            var _leave = await _context.LeaveBalances.Where(x => x.EmployeeId == _leaveBalance.EmployeeId
                            && x.LeavePolicyId == _leaveBalance.LeavePolicyId
                            && x.BatchId == _leaveBalance.BatchId
                            && x.OrgId == _leaveBalance.OrgId
                            && x.SubOrgId == _leaveBalance.SubOrgId).ToListAsync();
                            if (!_leave.Any())
                            {
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
                }
                await _context.SaveChangesAsync();
                tran.Commit();

                return Updated(_employeeLeave);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(ex);
            }



        }
        // POST: api/LeaveEmployeeLeaves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveEmployeeLeaf>> PostLeaveEmployeeLeaf([FromBody] LeaveEmployeeLeaf leaveEmployeeLeaf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.LeaveEmployeeLeaves.Add(leaveEmployeeLeaf);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


            return Ok(leaveEmployeeLeaf);
        }

        // DELETE: api/LeaveEmployeeLeaves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveEmployeeLeaf(int id)
        {
            var leaveEmployeeLeaf = await _context.LeaveEmployeeLeaves.FindAsync(id);
            if (leaveEmployeeLeaf == null)
            {
                return NotFound();
            }

            _context.LeaveEmployeeLeaves.Remove(leaveEmployeeLeaf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveEmployeeLeafExists(int id)
        {
            return _context.LeaveEmployeeLeaves.Any(e => e.EmployeeLeaveId == id);
        }
    }
}
