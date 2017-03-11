using asp.net_mvc_event_manager.Core;
using asp.net_mvc_event_manager.Core.Dtos;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace asp.net_mvc_event_manager.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notifications.GetNewNotificationsFor(userId);

            return Mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.UserNotifications.GetUserNotificationsFor(userId);

            notifications.ForEach(n => n.Read());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
