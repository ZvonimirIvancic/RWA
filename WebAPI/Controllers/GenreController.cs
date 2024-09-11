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
    public class GenreController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TestRwaContext _context;
        private readonly IMapper _mapper;

        public GenreController(IConfiguration configuration, TestRwaContext context, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<GenreDto>>(await _context.Genres.ToListAsync());
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenre(int id)
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return _mapper.Map<GenreDto>(genre);
        }

        // POST api/<GenreController>
        [HttpPost]
        public async Task<ActionResult<GenreDto>> PostGenre(GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);

            if (_context.Genres == null)
            {
                return Problem("Entity set 'TestRwaContext.Genres'  is null.");
            }
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenre", new { id = genre.Id }, genre);
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);

            if (id != genre.Id)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        /*{
            if (_context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            // Check if there are dependent records in the Song table
            var hasDependentSongs = await _context.SongGenres.AnyAsync(v => v.GenreId == id);
            if (hasDependentSongs)
            {
                return BadRequest("Cannot delete this genre because it is referenced by songs.");
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }
            var tag = await _context.Genres.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenreExists(int id)
        {
            return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
