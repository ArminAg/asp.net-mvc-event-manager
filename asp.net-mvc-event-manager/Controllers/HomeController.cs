using asp.net_mvc_event_manager.Models;
using asp.net_mvc_event_manager.Repositories;
using asp.net_mvc_event_manager.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace asp.net_mvc_event_manager.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var upcomingEvents = _context.Events
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .Where(e => e.DateTime > DateTime.Now && !e.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = upcomingEvents
                    .Where(e => 
                            e.Artist.Name.Contains(query) || 
                            e.Genre.Name.Contains(query) || 
                            e.Venue.Contains(query));
            }

            var userId = User.Identity.GetUserId();
            var attendances = _attendanceRepository.GetFutureAteendances(userId).ToLookup(a => a.EventId);

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Events",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Events", viewModel);
        }
    }
}