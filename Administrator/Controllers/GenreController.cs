using Administrator.ViewModels;
using AutoMapper;
using DAL.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Administrator.Controllers
{
    public class GenreController : Controller
    {
        private readonly TestRwaContext _context;
        private readonly IMapper _mapper;

        public GenreController(TestRwaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: GenreController
        public ActionResult Index()
        {
            try
            {
                var genreVms = _context.Genres.Select(x => new VMGenre
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    DebutYear = x.DebutYear,
                }).ToList();

                return View(genreVms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: GenreController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var genre = _context.Genres.FirstOrDefault(x => x.Id == id);
                var genreVM = new VMGenre
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Description = genre.Description,
                    DebutYear= genre.DebutYear
                };

                return View(genreVM);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMGenre genre)
        {
            try
            {
                var newGenre = new Genre
                {
                    Name = genre.Name,
                    Description = genre.Description,
                    DebutYear = genre.DebutYear
                };

                _context.Genres.Add(newGenre);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenreController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var genre = _context.Genres.FirstOrDefault(x => x.Id == id);
                var genreVM = new VMGenre
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Description = genre.Description,
                    DebutYear = genre.DebutYear
                };

                return View(genreVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMGenre genre)
        {
            try
            {
                var dbGenre = _context.Genres.FirstOrDefault(x => x.Id == id);
                dbGenre.Name = genre.Name;
                dbGenre.Description = genre.Description;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: GenreController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var genre = _context.Genres.FirstOrDefault(x => x.Id == id);
                var genreVM = new VMGenre
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Description = genre.Description,
                    DebutYear = genre.DebutYear
                };

                return View(genreVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VMGenre genre)
        {
            try
            {
                var dbGenre = _context.Genres.FirstOrDefault(x => x.Id == id);

                _context.Genres.Remove(dbGenre);

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
