using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Newtonsoft.Json.Linq;
using System.Reflection;
using ttpMiddleware.Models.DTOs.Responses;

using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class SportResultsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public SportResultsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/SportResults
        [HttpGet]
        public IQueryable<SportResult> GetSportResults()
        {
            return _context.SportResults.AsQueryable().AsNoTracking();
        }

        // GET: api/SportResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SportResult>> GetSportResult(int id)
        {
            var sportResult = await _context.SportResults.FindAsync(id);

            if (sportResult == null)
            {
                return NotFound();
            }

            return sportResult;
        }

        // PUT: api/SportResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSportResult(int id, SportResult sportResult)
        {
            if (id != sportResult.SportResultId)
            {
                return BadRequest();
            }

            _context.Entry(sportResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportResultExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<SportResult> sportResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.SportResults.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            sportResult.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportResultExists(key))
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
        // POST: api/SportResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SportResult>> PostSportResult([FromBody] JArray jsonWrapper)
        {
            var _errormessage = new List<string>();
            JToken jsonValues = jsonWrapper;
            SportResult _sportResult = new SportResult();

            foreach (var x in jsonValues)
            {
                _sportResult = x.ToObject<SportResult>();
                try
                {
                    if (_sportResult.SportResultId > 0)
                    {
                        var existingsportresult = await _context.SportResults.Where(x => x.SportResultId == _sportResult.SportResultId)
                            .ToListAsync();
                        if (existingsportresult.Count == 0)
                        {
                            _errormessage.Add("No sports result with Id : " + _sportResult.SportResultId + " found");
                        }
                        else
                        {
                            foreach (var res in existingsportresult)
                            {
                                foreach (PropertyInfo prop in res.GetType().GetProperties())
                                {
                                    if (prop.GetValue(_sportResult, null) != null)
                                        prop.SetValue(res, prop.GetValue(_sportResult, null));
                                }

                                _context.SportResults.Update(res);
                            }
                            //   await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        //    existingresult = await _context.SportResults.Where(x =>
                        //                               x.SportsNameId == _sportResult.SportsNameId
                        //                               && x.GroupId == _sportResult.GroupId
                        //                               && x.CategoryId == _sportResult.CategoryId
                        //                               && x.SubCategoryId == _sportResult.SubCategoryId
                        //                            //&& x.StudentClassId == _sportResult.StudentClassId
                        //                            && x.SessionId == _sportResult.SessionId).ToListAsync();
                        //if (existingresult.Count > 0)
                        //    _errormessage.Add("Activity for StudentClassId: " + _sportResult.StudentClassId + " already exists.");//throw new Exception("Activity for this person already exists.");
                        //else
                        //{
                            _context.SportResults.Add(_sportResult);
                            // await _context.SaveChangesAsync();
                        //}
                    }
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _errormessage.Add(ex.Message);
                }

            }
            if (_errormessage.Count > 0)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = _errormessage,
                    Success = false
                });
            }
            return Ok(_sportResult);

        }

        // DELETE: api/SportResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSportResult(int id)
        {
            var sportResult = await _context.SportResults.FindAsync(id);
            if (sportResult == null)
            {
                return NotFound();
            }

            _context.SportResults.Remove(sportResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SportResultExists(int id)
        {
            return _context.SportResults.Any(e => e.SportResultId == id);
        }
    }
}
