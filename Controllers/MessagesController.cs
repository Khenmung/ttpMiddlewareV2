using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller")]
    [EnableQuery]
    public class MessagesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public MessagesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        [EnableQuery]
        public IQueryable<Message> Get()
        {
            return _context.Messages.AsNoTracking().AsQueryable();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(short id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(short id, Message message)
        {
            if (id != message.MessageId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<Message> message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.Messages.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            message.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(key))
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
        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage([FromBody]Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(short id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(short id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
