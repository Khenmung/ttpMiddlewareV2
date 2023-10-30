using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Data.Entities;
using ttpMiddleware.Models;
using ttpMiddleware.Models.DTOs.Responses;

using ttpMiddleware.CommonFunctions;
using Newtonsoft.Json.Linq;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class OrganizationPaymentsController : ProtectedController
    {
        private readonly ttpauthContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrganizationPaymentsController(ttpauthContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/OrganizationPayments
        [HttpGet]
        public IQueryable<OrganizationPayment> GetOrganizationPayments()
        {
            return _context.OrganizationPayments.AsQueryable().AsNoTracking();
        }

        // GET: api/OrganizationPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationPayment>> GetOrganizationPayment(int id)
        {
            var organizationPayment = await _context.OrganizationPayments.FindAsync(id);

            if (organizationPayment == null)
            {
                return NotFound();
            }

            return organizationPayment;
        }

        // PUT: api/OrganizationPayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationPayment(int id, OrganizationPayment organizationPayment)
        {
            if (id != organizationPayment.OrganizationPaymentId)
            {
                return BadRequest();
            }

            _context.Entry(organizationPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationPaymentExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<OrganizationPayment> organizationPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.OrganizationPayments.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            organizationPayment.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationPaymentExists(key))
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
        // POST: api/OrganizationPayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrganizationPayment>> PostOrganizationPayment([FromBody] OrganizationPayment organizationPayment)
        {
            //JToken jsonValues = jsonWrapper;
            //OrganizationPayment _organizationPayment = new OrganizationPayment();
            //List<OrgPaymentDetail> _OrgPaymentDetail = new List<OrgPaymentDetail>();

            //foreach (JProperty x in jsonValues)
            //{
            //    if (x.Name == "OrganizationPayment")
            //        _organizationPayment = x.Value.ToObject<OrganizationPayment>();
            //    else if (x.Name == "OrgPaymentDetail")
            //        _OrgPaymentDetail = x.Value.ToObject<List<OrgPaymentDetail>>();
            //}

            using var tran = _context.Database.BeginTransaction();
            try
            {

                _context.OrganizationPayments.Add(organizationPayment);
                await _context.SaveChangesAsync();
                var NoOfMonth = (int)organizationPayment.PaidMonths;

                var org = _context.Organizations.Where(x => x.OrganizationId == organizationPayment.OrgId).Select(s => s).FirstOrDefault();
                org.ValidTo = Convert.ToDateTime(org.ValidTo).AddMonths(NoOfMonth);
                _context.Organizations.Update(org);

                _context.SaveChanges();

                var _admins = await _context.RoleUsers.Join(_context.MasterItems,
                    roleuser => roleuser.RoleId,
                    master => master.MasterDataId,
                    (roleuser, master) => new { roleuser.Active, roleuser.OrgId, roleuser.UserId, master.MasterDataName })
                    .Where(x => x.MasterDataName == "Admin"
                    && x.OrgId == organizationPayment.OrgId
                    && x.Active == 1).Select(s => s).ToListAsync();

                foreach (var item in _admins)
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);
                    user.ValidTo = Convert.ToDateTime(user.ValidTo).AddMonths(NoOfMonth);
                    await _userManager.UpdateAsync(user);
                }
                
                _context.SaveChanges();
                //_userManager.fin
                tran.Commit();
                return Ok(organizationPayment);

            }
            catch (Exception ex)
            {
                tran.Rollback();
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                                ex.Message
                            },
                    Success = false

                });
            }
        }

        // DELETE: api/OrganizationPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationPayment(int id)
        {
            var organizationPayment = await _context.OrganizationPayments.FindAsync(id);
            if (organizationPayment == null)
            {
                return NotFound();
            }

            _context.OrganizationPayments.Remove(organizationPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationPaymentExists(int id)
        {
            return _context.OrganizationPayments.Any(e => e.OrganizationPaymentId == id);
        }
    }
}
