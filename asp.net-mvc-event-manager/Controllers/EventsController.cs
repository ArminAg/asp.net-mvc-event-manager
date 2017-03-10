using asp.net_mvc_event_manager.Models;
using asp.net_mvc_event_manager.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace asp.net_mvc_event_manager.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var events = _context.Events
                .Where(e => e.ArtistId == userId && e.DateTime > DateTime.Now)
                .Include(e => e.Genre)
                .ToList();

            return View(events);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var events = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Event)
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .ToList();

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = events,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Events I'm Attending"
            };
            return View("Events", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel()
            {
                Heading = "Add Event",
                Genres = _context.Genres.ToList()
            };

            return View("EventForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Heading = "Add Event";
                viewModel.Genres = _context.Genres.ToList();
                return View("EventForm", viewModel);
            }

            var newEvent = new Event()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.GenreId,
                Venue = viewModel.Venue
            };

            _context.Events.Add(newEvent);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var dbEvent = _context.Events.Single(e => e.Id == id && e.ArtistId == userId);

            var viewModel = new EventFormViewModel()
            {
                Heading = "Edit Event",
                Genres = _context.Genres.ToList(),
                Id = dbEvent.Id,
                Date = dbEvent.DateTime.ToString("d MMM yyyy"),
                Time = dbEvent.DateTime.ToString("HH:mm"),
                GenreId = dbEvent.GenreId,
                Venue = dbEvent.Venue
            };

            return View("EventForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Heading = "Edit Event";
                viewModel.Genres = _context.Genres.ToList();
                return View("EventForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var existingEvent = _context.Events.Single(e => e.Id == viewModel.Id && e.ArtistId == userId);
            existingEvent.Venue = viewModel.Venue;
            existingEvent.DateTime = viewModel.GetDateTime();
            existingEvent.GenreId = viewModel.GenreId;
            
            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }
    }
}