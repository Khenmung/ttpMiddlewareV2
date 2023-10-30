using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;

using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Common;
using Microsoft.Extensions.Configuration;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    //[Authorize]
    public class ClassSubjectsController : ProtectedController
    {
        private readonly ttpauthContext _context;
        private readonly IConfiguration _configuration;

        public ClassSubjectsController(ttpauthContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        // GET: api/ClassSubjects
        [HttpGet]
        public IQueryable<ClassSubject> GetClassSubjects()
        {
            try
            {
                return _context.ClassSubjects.AsQueryable().AsNoTracking();
            }
            catch (Exception ex)
            {
                return (IQueryable<ClassSubject>)BadRequest(ex);
            }

        }

        // GET: api/ClassSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassSubject>> GetClassSubject(int id)
        {
            var classSubject = await _context.ClassSubjects.FindAsync(id);

            if (classSubject == null)
            {
                return NotFound();
            }

            return classSubject;
        }

        // PUT: api/ClassSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ClassSubject> classSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ClassSubjects.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            classSubject.Patch(entity);
            try
            {
                //var _studentClassSubject = await _context.StudentClassSubjects.Where(x => x.ClassSubjectId == entity.ClassSubjectId
                //&& x.OrgId == entity.OrgId
                //&& x.SubOrgId == entity.SubOrgId).ToListAsync();

                //foreach (var item in _studentClassSubject)
                //{
                //    item.Active = entity.Active;
                //    item.SectionId = entity.SectionId;
                //    item.SemesterId = entity.SemesterId;
                //    _context.Update(item);
                //}
                var _slotNClassSubject = await _context.SlotAndClassSubjects.Where(x => x.ClassSubjectId == entity.ClassSubjectId
                && x.OrgId == entity.OrgId
                && x.SubOrgId == entity.SubOrgId).ToListAsync();

                foreach (var item in _slotNClassSubject)
                {
                    item.Active = entity.Active;
                    item.SectionId = entity.SectionId;
                    item.SemesterId = entity.SemesterId;
                    _context.Update(item);
                }

                var _clsSubjComponents = await _context.ClassSubjectMarkComponents.Where(x => x.ClassSubjectId == entity.ClassSubjectId
                && x.OrgId == entity.OrgId
                && x.SubOrgId == entity.SubOrgId).ToListAsync();

                foreach (var item in _clsSubjComponents)
                {
                    item.Active = entity.Active;
                    item.SectionId = entity.SectionId;
                    item.SemesterId = entity.SemesterId;
                    _context.Update(item);
                }

                await _context.SaveChangesAsync();
                var classsubject = await _context.ClassSubjects.Join(_context.ClassMasters, clssubj => clssubj.ClassId, clsmaster => clsmaster.ClassId, (clssubj, clsmaster) => new { clsmaster.ClassName, clssubj.ClassSubjectId, clssubj.SubjectId })
                    .Join(_context.MasterItems, subj => subj.SubjectId, master => master.MasterDataId, (subj, master) => new { ClassName = subj.ClassName, ClassSubjectId = subj.ClassSubjectId, Subject = master.MasterDataName, ApplicationId = master.ApplicationId })
                    .Where(x => x.ClassSubjectId == entity.ClassSubjectId)
                    .Select(s => new { s.ClassName, s.Subject, s.ApplicationId })
                    .ToListAsync();

                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var _classSubjectId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "class subject").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_classSubjectId == 0)
                {
                    return BadRequest("Table name class subject not found.");
                }
                else
                {
                    commonfunctions common = new commonfunctions(null, null, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = classsubject[0].ClassName + "-" + classsubject[0].Subject;
                    customfeature.OrgId = entity.OrgId;
                    customfeature.SubOrgId = entity.SubOrgId;
                    customfeature.Active = entity.Active == 1 ? true : false;
                    customfeature.ApplicationId = (short)classsubject[0].ApplicationId;
                    customfeature.TableName = "classsubject";
                    customfeature.TableRowId = (short)entity.ClassSubjectId;
                    customfeature.TableNameId = _classSubjectId;

                    common.AddToCustomFeature(customfeature, (bool)entity.Confidential, "classsubject");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                
                    throw;                
                
            }
            return Updated(entity);
        }

        // POST: api/ClassSubjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassSubject>> Post([FromBody] ClassSubject classSubject)
        {
            try
            {

                _context.ClassSubjects.Add(classSubject);
                await _context.SaveChangesAsync();
                var _TableNameParentId = Convert.ToInt32(_configuration.GetSection("ApplicationConfig").GetSection("TableNameParentId").Value);
                var _classSubjectId = await _context.MasterItems.Where(x => x.ParentId == _TableNameParentId && x.MasterDataName.ToLower() == "class subject").Select(s => s.MasterDataId).FirstOrDefaultAsync();
                if (_classSubjectId == 0)
                {
                    return BadRequest("Table name class subject not found.");
                }
                else
                {
                    var classsubject = await _context.ClassSubjects.Join(_context.ClassMasters, clssubj => clssubj.ClassId, clsmaster => clsmaster.ClassId, (clssubj, clsmaster) => new { clsmaster.ClassName, clssubj.ClassSubjectId, clssubj.SubjectId })
                   .Join(_context.MasterItems, subj => subj.SubjectId, master => master.MasterDataId, (subj, master) => new { ClassName = subj.ClassName, ClassSubjectId = subj.ClassSubjectId, Subject = master.MasterDataName, ApplicationId = master.ApplicationId })
                   .Where(x => x.ClassSubjectId == classSubject.ClassSubjectId)
                   .Select(s => new { s.ClassName, s.Subject, s.ApplicationId })
                   .ToListAsync();

                    commonfunctions common = new commonfunctions(null, null, _context);
                    var customfeature = new CustomFeature();
                    customfeature.CustomFeatureName = classsubject[0].ClassName + "-" + classsubject[0].Subject;
                    customfeature.OrgId = classSubject.OrgId;
                    customfeature.SubOrgId = classSubject.SubOrgId;
                    customfeature.Active = classSubject.Active == 1 ? true : false;
                    customfeature.ApplicationId = (int)classsubject[0].ApplicationId;
                    customfeature.TableName = "classsubject";
                    customfeature.TableRowId = classSubject.ClassSubjectId;
                    customfeature.TableNameId = _classSubjectId;

                    common.AddToCustomFeature(customfeature, (bool)classSubject.Confidential, "classsubject");
                }
                return Ok(classSubject);
            }
            catch (DbUpdateException ex)
            {
                throw;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE: api/ClassSubjects/5
        //[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int key)
        {
            var classSubject = await _context.ClassSubjects.FindAsync(key);
            if (classSubject == null)
            {
                return NotFound();
            }

            _context.ClassSubjects.Remove(classSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassSubjectExists(int key)
        {
            return _context.ClassSubjects.Any(e => e.ClassSubjectId == key);
        }
    }
}
