using asp.net_mvc_event_manager.Dtos;
using asp.net_mvc_event_manager.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace asp.net_mvc_event_manager.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.EventId == attendanceDto.EventId))
                return BadRequest("The attendance already exists.");

            var attendace = new Attendance
            {
                EventId = attendanceDto.EventId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendace);
            _context.SaveChanges();

            return Ok();
        }
    }
}
