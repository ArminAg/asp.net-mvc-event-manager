using asp.net_mvc_event_manager.Models;
using asp.net_mvc_event_manager.Repositories;
using asp.net_mvc_event_manager.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace asp.net_mvc_event_manager.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly EventRepository _eventRepository;
        private readonly FollowingRepository _followingRepository;
        private readonly GenreRepository _genrerepository;

        public EventsController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _eventRepository = new EventRepository(_context);
            _followingRepository = new FollowingRepository(_context);
            _genrerepository = new GenreRepository(_context);
        }

        [HttpPost]
        public ActionResult Search(EventsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            var currentEvent = _eventRepository.GetEvent(id);

            if (currentEvent == null)
                return HttpNotFound();

            var viewModel = new EventDetailsViewModel { Event = currentEvent };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending = _attendanceRepository.GetAttendance(currentEvent.Id , userId) != null;
                viewModel.IsFollowing = _followingRepository.GetFollowing(currentEvent.ArtistId, userId) != null;
            }

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var events = _eventRepository.GetUpcomingEventsByArtist(userId);

            return View(events);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = _eventRepository.GetEventsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Events I'm Attending",
                Attendances = _attendanceRepository.GetFutureAteendances(userId).ToLookup(a => a.EventId)
            };

            return View("Events", viewModel);
        }
        
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel()
            {
                Heading = "Add Event",
                Genres = _genrerepository.GetGenres()
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
            var dbEvent = _eventRepository.GetEvent(id);

            if (dbEvent == null)
                return HttpNotFound();

            if (dbEvent.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

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
            
            var existingEvent = _eventRepository.GetEventWithAttendees(viewModel.Id);

            if (existingEvent == null)
                return HttpNotFound();

            if (existingEvent.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            existingEvent.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.GenreId);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }
    }
}