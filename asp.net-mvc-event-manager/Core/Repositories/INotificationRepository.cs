using asp.net_mvc_event_manager.Core.Models;
using System.Collections.Generic;

namespace asp.net_mvc_event_manager.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotificationsFor(string userId);
    }
}