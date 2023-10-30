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
    public class PhotoGalleriesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public PhotoGalleriesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/PhotoGalleries
        [HttpGet]
        public IQueryable<PhotoGallery> GetPhotoGalleries()
        {
            return _context.PhotoGalleries.AsQueryable().AsNoTracking();
        }

        // GET: api/PhotoGalleries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoGallery>> GetPhotoGallery(int id)
        {
            var photoGallery = await _context.PhotoGalleries.FindAsync(id);

            if (photoGallery == null)
            {
                return NotFound();
            }

            return photoGallery;
        }

        // PUT: api/PhotoGalleries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhotoGallery(int id, PhotoGallery photoGallery)
        {
            if (id != photoGallery.PhotoId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(photoGallery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoGalleryExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<PhotoGallery> photoGallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.PhotoGalleries.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            photoGallery.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoGalleryExists(key))
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
        // POST: api/PhotoGalleries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhotoGallery>> PostPhotoGallery([FromBody]PhotoGallery photoGallery)
        {
            _context.PhotoGalleries.Add(photoGallery);
            await _context.SaveChangesAsync();

            return Ok(photoGallery);
        }

        // DELETE: api/PhotoGalleries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhotoGallery(int id)
        {
            var photoGallery = await _context.PhotoGalleries.FindAsync(id);
            if (photoGallery == null)
            {
                return NotFound();
            }

            _context.PhotoGalleries.Remove(photoGallery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoGalleryExists(int id)
        {
            return _context.PhotoGalleries.Any(e => e.PhotoId == id);
        }
    }
}
