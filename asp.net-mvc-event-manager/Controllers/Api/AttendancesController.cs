using asp.net_mvc_event_manager.Core;
using asp.net_mvc_event_manager.Core.Dtos;
using asp.net_mvc_event_manager.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace asp.net_mvc_event_manager.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(attendanceDto.EventId, userId);
            if (attendance != null)
                return BadRequest("The attendance already exists.");

            attendance = new Attendance
            {
                EventId = attendanceDto.EventId,
                AttendeeId = userId
            };

            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(id, userId);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
