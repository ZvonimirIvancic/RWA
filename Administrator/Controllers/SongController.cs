using Administrator.ViewModels;
using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using X.PagedList.Extensions;

namespace Administrator.Controllers
{
    public class SongController : Controller
    {
        private readonly TestRwaContext _context;
        private readonly IMapper _mapper;

        public SongController(TestRwaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: SongController
        public async Task<IActionResult> Index(int? page, string? searchText)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;
            ViewData["pages"] = pageNumber;
            List<Song> testRwaContextPaged = null;
            if (searchText != null)
            {
                testRwaContextPaged =
                    await _context.Songs
                    .Include(s => s.Performer)
                    .Include(sg => sg.SongGenres)
                    .ThenInclude(s => s.Genre)
                    .Where(s => s.Name.Contains(searchText))
                    .OrderBy(s => s.Name)
                    .ToListAsync();



                ViewData["pages"] = testRwaContextPaged.Count() / pageSize;
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("SearchText", searchText, options);
                ViewData["page"] = page;

                return View(testRwaContextPaged.ToPagedList(pageNumber, pageSize));
            }
            testRwaContextPaged =
                await _context.Songs
                .Include(s => s.SongGenres)
                .ThenInclude(sg => sg.Genre)
                .Include(s => s.Performer)
                .OrderBy(s => s.Name)
                .ToListAsync();

            Response.Cookies.Delete("SearchText");
            ViewData["pages"] = testRwaContextPaged.Count() / pageSize;
            ViewData["page"] = page;
            return View(testRwaContextPaged.ToPagedList(pageNumber, pageSize));
            //try
            //{
            //    var songVms = _context.Songs
            //        .Include(x => x.Performer)
            //        .Include(x => x.SongGenres)
            //        .ThenInclude(x=>x.Genre)

            //        .Select(x => new VMSong
            //        {
            //            Id = x.Id,
            //            Name = x.Name,
            //            Tempo = x.Tempo,
            //            Melody = x.Melody,
            //            Language = x.Language,
            //            YearOfRelease = x.YearOfRelease,
            //            PerformerId = x.PerformerId,


            //        }).ToList();

            //    return View(songVms);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        // GET: SongController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var song = _context.Songs.FirstOrDefault(x => x.Id == id);
                var songVM = new VMSong
                {
                    Id = song.Id,
                    Name = song.Name,
                    Tempo = song.Tempo,
                    Melody = song.Melody,
                    Language = song.Language,
                    YearOfRelease = song.YearOfRelease,
                    PerformerId = song.PerformerId,
                };

                Performer performer = _context.Performers.FirstOrDefault(x => x.Id.Equals(songVM.PerformerId));

                string fullnamePerformer = performer.FirstName + " " + performer.LastName;

                ViewBag.PerformerName = fullnamePerformer;
   
                return View(songVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: SongController/Create
        public ActionResult Create()
        {

            try
            {
                ViewBag.GenreDdlItems = _context.Genres.Select(x =>
                 new SelectListItem
                 {
                    Text = x.Name,
                     Value = x.Id.ToString()
                  });
                ViewBag.PerformerDdlItems = _context.Performers.Select(x =>
                 new SelectListItem
             {
                     Text = $"{x.FirstName} {x.LastName}",
                             Value = x.Id.ToString()
                        });

                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: SongController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VMSong song)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.GenreDdlItems = _context.Genres.Select(x =>
                     new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.Id.ToString()
                     });
                     ViewBag.PerformerDdlItems = _context.Performers.Select(x =>
                     new SelectListItem
                    {
                    Text = $"{x.FirstName} {x.LastName}",
                         Value = x.Id.ToString()
                       });

                    ModelState.AddModelError("", "Failed to create song");

                    return View();
                }

                var existingSong = await _context.Songs.FirstOrDefaultAsync(s => s.Name == song.Name);

                if (existingSong != null)
                {
                    ModelState.AddModelError("Name", "A song with the same name already exists.");
                    return View(song);
                }

                // If no duplicate is found, map and add the song
                var newSong = _mapper.Map<Song>(song);
                _context.Songs.Add(newSong);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: SongController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.GenreDdlItems = _context.Genres.Select(x =>
             new SelectListItem
             {
                 Text = x.Name,
                 Value = x.Id.ToString()
             });
            ViewBag.PerformerDdlItems = _context.Performers.Select(x =>
 new SelectListItem
 {
     Text = $"{x.FirstName} {x.LastName}",
     Value = x.Id.ToString()
 });

            var song = _context.Songs.FirstOrDefault(x => x.Id == id);
            var songVM = new VMSong
            {
                Id = song.Id,
                Name = song.Name,
                Tempo = song.Tempo,
                Melody = song.Melody,
                Language = song.Language,
                YearOfRelease = song.YearOfRelease,
                PerformerId = song.PerformerId,
            };

            return View(songVM);
        }

        // POST: SongController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMSong song)
        {
            try
            {
                var dbSong = _context.Songs.FirstOrDefault(x => x.Id == id);
                dbSong.Id = song.Id;
                dbSong.Name = song.Name;
                dbSong.Tempo = song.Tempo;
                dbSong.Melody = song.Melody;
                dbSong.Language = song.Language;
                dbSong.YearOfRelease = song.YearOfRelease;
                dbSong.PerformerId = song.PerformerId;

                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SongController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var song = _context.Songs.FirstOrDefault(x => x.Id == id);
                var songVM = new VMSong
                {
                    Id = song.Id,
                    Name = song.Name,
                    Tempo = song.Tempo,
                    Melody = song.Melody,
                    Language = song.Language,
                    YearOfRelease = song.YearOfRelease,
                    PerformerId = song.PerformerId,
                };
                Performer performer = _context.Performers.FirstOrDefault(x => x.Id.Equals(songVM.PerformerId));

                string fullnamePerformer = performer.FirstName + " " + performer.LastName;

                ViewBag.PerformerName = fullnamePerformer;

                return View(songVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: SongController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Delete(int id, VMSong song)
        {
            try
            {
                var dbSong = _context.Songs.FirstOrDefault(x => x.Id == id);

                _context.Songs.Remove(dbSong);

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
