using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TestRwaContext _context;
        private readonly IMapper _mapper;
        public PerformerController(IConfiguration configuration, TestRwaContext context, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<PerformerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformerDto>>> GetPerformers()
        {
            if (_context.Performers == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<PerformerDto>>(await _context.Performers.ToListAsync());
        }

        // GET api/<PerformerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformerDto>> GetPerformer(int id)
        {
            if (_context.Performers == null)
            {
                return NotFound();
            }
            var performer = await _context.Performers.FindAsync(id);

            if (performer == null)
            {
                return NotFound();
            }

            return _mapper.Map<PerformerDto>(performer);
        }

        // POST api/<PerformerController>
        [HttpPost]
        public async Task<ActionResult<PerformerDto>> PostPerformer(PerformerDto performerDto)
        {
            var performer = _mapper.Map<Performer>(performerDto);

            if (_context.Performers == null)
            {
                return Problem("Entity set 'TestRwaContext.Performers'  is null.");
            }
            _context.Performers.Add(performer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformer", new { id = performer.Id }, performer);
        }

        // PUT api/<PerformerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformer(int id, PerformerDto performerDto)
        {
            var performer = _mapper.Map<Performer>(performerDto);

            if (id != performer.Id)
            {
                return BadRequest();
            }

            _context.Entry(performer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformerExists(id))
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

        // DELETE api/<PerformerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformer(int id)
        {
            if (_context.Performers == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers.FindAsync(id);
            if (performer == null)
            {
                return NotFound();
            }

            // Check if there are dependent records in the Song table
            var hasDependentSongs = await _context.Songs.AnyAsync(s => s.PerformerId == id);
            if (hasDependentSongs)
            {
                return BadRequest("Cannot delete this performer because it is referenced by songs.");
            }

            _context.Performers.Remove(performer);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool PerformerExists(int id)
        {
            return (_context.Performers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
