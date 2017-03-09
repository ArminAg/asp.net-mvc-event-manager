using asp.net_mvc_event_manager.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace asp.net_mvc_event_manager.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcomingEvents = _context.Events
                .Include(e => e.Artist)
                .Where(e => e.DateTime > DateTime.Now);

            return View(upcomingEvents);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}