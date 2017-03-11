using asp.net_mvc_event_manager.Models;
using asp.net_mvc_event_manager.Persistence;
using asp.net_mvc_event_manager.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace asp.net_mvc_event_manager.Controllers
{
    public class EventsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult Search(EventsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            var currentEvent = _unitOfWork.Events.GetEvent(id);

            if (currentEvent == null)
                return HttpNotFound();

            var viewModel = new EventDetailsViewModel { Event = currentEvent };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending = _unitOfWork.Attendances.GetAttendance(currentEvent.Id , userId) != null;
                viewModel.IsFollowing = _unitOfWork.Followings.GetFollowing(currentEvent.ArtistId, userId) != null;
            }

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var events = _unitOfWork.Events.GetUpcomingEventsByArtist(userId);

            return View(events);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = _unitOfWork.Events.GetEventsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Events I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAteendances(userId).ToLookup(a => a.EventId)
            };

            return View("Events", viewModel);
        }
        
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel()
            {
                Heading = "Add Event",
                Genres = _unitOfWork.Genres.GetGenres()
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
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("EventForm", viewModel);
            }

            var newEvent = new Event()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.GenreId,
                Venue = viewModel.Venue
            };

            _unitOfWork.Events.Add(newEvent);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Events");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var dbEvent = _unitOfWork.Events.GetEvent(id);

            if (dbEvent == null)
                return HttpNotFound();

            if (dbEvent.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new EventFormViewModel()
            {
                Heading = "Edit Event",
                Genres = _unitOfWork.Genres.GetGenres(),
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
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("EventForm", viewModel);
            }
            
            var existingEvent = _unitOfWork.Events.GetEventWithAttendees(viewModel.Id);

            if (existingEvent == null)
                return HttpNotFound();

            if (existingEvent.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            existingEvent.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.GenreId);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Events");
        }
    }
}