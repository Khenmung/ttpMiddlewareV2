using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    /// <summary>
    /// Student controller
    /// </summary>
    [ODataRoutePrefix("Bulk")]
    public class BulkController : ProtectedController
    {
        private readonly ttpauthContext _context;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        public BulkController(ttpauthContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] JObject jsonWrapper)
        {
            try
            {
                JToken jsonValues = jsonWrapper;
                List<Student> _StudentList = new List<Student>();

                foreach (JProperty x in jsonValues)
                {
                    _StudentList.Add(x.Value.ToObject<Student>());
                }

                //_context.Students.raw
                //_context.Students.Add(student);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
