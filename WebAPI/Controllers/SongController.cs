using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Packaging.Signing;
using WebAPI.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TestRwaContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SongController> _logger;

        public SongController(IConfiguration configuration, TestRwaContext context, IMapper mapper, ILogger<SongController> logger)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        private async Task LogAsync(string level, string message)
        {
            var log = new Log
            {
                Timestamp = DateTime.UtcNow,
                Level = level,
                Message = message
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }

        // GET: api/<SongController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDto>>> GetSongs()
        {
            _logger.LogInformation("Fetching all songs.");
            await LogAsync("Information", "Fetching all songs.");
            if (_context.Songs == null)
            {
                return NotFound();
            }
            var songs = await _context.Songs
                        .Include(s => s.SongGenres)
                        .ThenInclude(sg => sg.Genre)
                        .ToListAsync();
            return _mapper.Map<List<SongDto>>(songs);
        }

        // GET api/<SongController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDto>> GetSong(int id)
        {
            _logger.LogInformation($"Fetching song with ID: {id}");
            await LogAsync("Information", $"Fetching song with ID: {id}");
            if (_context.Songs == null)
            {
                return NotFound();
            }
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                _logger.LogWarning($"Song with ID {id} not found.");
                await LogAsync("Warning", $"Song with ID {id} not found.");
                return NotFound();
            }

            return _mapper.Map<SongDto>(song);
        }

        // GET Search

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<SongDto>>> Search(int page, int size, string? orderBy, string? direction, string? filter)
        {
            var songs = await _context.Songs.ToListAsync();
            IEnumerable<Song> ordered;
            IEnumerable<Song> filteredSongs;

            if (filter != null)
            {
                filteredSongs = songs.Where(x =>
                    x.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                filteredSongs = songs;
            }

            // Ordering
            if (string.Compare(orderBy, "id", true) == 0)
            {
                ordered = filteredSongs.OrderBy(x => x.Id);
            }
            else if (string.Compare(orderBy, "name", true) == 0)
            {
                ordered = filteredSongs.OrderBy(x => x.Name);
            }
            else if (string.Compare(orderBy, "yearOfRelease", true) == 0)
            {
                ordered = filteredSongs.OrderBy(x => x.YearOfRelease);
            }
            else
            {
            // default: order by Id
                ordered = filteredSongs.OrderBy(x => x.Id);
            }

            // descending order
            if (string.Compare(direction, "desc", true) == 0)
            {
                ordered = ordered.Reverse();
            }


            // Now we can page the correctly ordered items
            var retVal = ordered.Skip((page - 1) * size).Take(size);


            return Ok(_mapper.Map<List<SongDto>>(retVal));
        }

        // POST api/<SongController>
        [HttpPost]
        public async Task<ActionResult<SongDto>> PostSong(SongDto song)
        {
            if (_context.Songs == null)
            {
                return Problem("Entity set 'TestRwaContext.Songs'  is null.");
            }

            var newSong = _mapper.Map<Song>(song);
            _context.Songs.Add(newSong);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Created a new song with ID: {newSong.Id}");
            await LogAsync("Information", $"Created a new song with ID: {newSong.Id}");

            return CreatedAtAction("GetSong", new { id = newSong.Id }, _mapper.Map<SongDto>(newSong));
        }

        // PUT api/<SongController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, SongDto songDto)
        {
            Song song = _mapper.Map<Song>(songDto);
            if (id != song.Id)
            {
                _logger.LogWarning($"Song ID mismatch. Request ID: {id}, Song ID: {song.Id}");
                await LogAsync("Warning", $"Song ID mismatch. Request ID: {id}, Song ID: {song.Id}");
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated song with ID: {song.Id}");
                await LogAsync("Information", $"Updated song with ID: {song.Id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    _logger.LogWarning($"Song with ID {id} not found during update.");
                    await LogAsync("Warning", $"Song with ID {id} not found during update.");
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Concurrency error while updating song with ID {id}");
                    await LogAsync("Error", $"Concurrency error while updating song with ID {id}");
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<SongController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                _logger.LogWarning($"Song with ID {id} not found during deletion.");
                await LogAsync("Warning", $"Song with ID {id} not found during deletion.");
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted song with ID: {id}");
            await LogAsync("Information", $"Deleted song with ID: {id}");

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: api/logs/get/N
        [HttpGet("logs/get/{n?}")]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogs(int n = 10)
        {
            var logs = await _context.Logs
                .OrderByDescending(l => l.Timestamp)
                .Take(n)
                .ToListAsync();

            return Ok(logs);
        }

        // GET: api/logs/count
        [HttpGet("logs/count")]
        public async Task<ActionResult<int>> GetLogCount()
        {
            var count = await _context.Logs.CountAsync();
            return Ok(count);
        }
    }
}

