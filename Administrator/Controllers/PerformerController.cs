using Administrator.ViewModels;
using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Administrator.Controllers
{
    public class PerformerController : Controller
    {
        private readonly TestRwaContext _context;
        private readonly IMapper _mapper;

        public PerformerController(TestRwaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: PerformerController
        public ActionResult Index()
        {
            try
            {
                var performerVms = _context.Performers.Select(x => new VMPerformer
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    YearOfBirth = x.YearOfBirth
                }).ToList();

                return View(performerVms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: PerformerController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
            var performer = _context.Performers.FirstOrDefault(x => x.Id == id);
            var performerVM = new VMPerformer
            {
                Id = performer.Id,
                FirstName = performer.FirstName,
                LastName = performer.LastName,
                YearOfBirth = performer.YearOfBirth
            };

            return View(performerVM);
                }
            catch (Exception ex)
            {

                throw ex;
            }
}

        // GET: PerformerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PerformerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMPerformer performer)
        {
            try
            {
                var newPerformer = new Performer
                {
                    FirstName = performer.FirstName,
                    LastName = performer.LastName,
                    YearOfBirth = performer.YearOfBirth
                };

                _context.Performers.Add(newPerformer);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PerformerController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var performer = _context.Performers.FirstOrDefault(x => x.Id == id);
                var performerVM = new VMPerformer
                {
                    Id = performer.Id,
                    FirstName = performer.FirstName,
                    LastName = performer.LastName,
                    YearOfBirth = performer.YearOfBirth
                };

                return View(performerVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: PerformerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMPerformer performer)
        {
            try
            {
                var dbPerformer = _context.Performers.FirstOrDefault(x => x.Id == id);
                dbPerformer.FirstName = performer.FirstName;
                dbPerformer.LastName = performer.LastName;
                dbPerformer.YearOfBirth = performer.YearOfBirth;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: PerformerController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var performer = _context.Performers.FirstOrDefault(x => x.Id == id);
                var performerVM = new VMPerformer
                {
                    Id = performer.Id,
                    FirstName = performer.FirstName,
                    LastName = performer.LastName,
                    YearOfBirth = performer.YearOfBirth
                };

                return View(performerVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: PerformerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VMPerformer performer)
        {
            try
            {
                var dbPerformer = _context.Performers.FirstOrDefault(x => x.Id == id);

                _context.Performers.Remove(dbPerformer);

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
