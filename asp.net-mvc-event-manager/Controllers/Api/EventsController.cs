using asp.net_mvc_event_manager.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace asp.net_mvc_event_manager.Controllers.Api
{
    [Authorize]
    public class EventsController : ApiController
    {
        private ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var currentEvent = _context.Events.Single(e => e.Id == id && e.ArtistId == userId);

            if (currentEvent.IsCanceled)
                return NotFound();

            currentEvent.IsCanceled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
