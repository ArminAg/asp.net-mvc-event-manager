using asp.net_mvc_event_manager.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace asp.net_mvc_event_manager.Controllers.Api
{
    [Authorize]
    public class EventsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var currentEvent = _unitOfWork.Events.GetEventWithAttendees(id);

            if (currentEvent == null || currentEvent.IsCanceled)
                return NotFound();

            if (currentEvent.ArtistId != userId)
                return Unauthorized();

            currentEvent.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
