using asp.net_mvc_event_manager.Core.Models;
using asp.net_mvc_event_manager.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace asp.net_mvc_event_manager.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Event.Artist)
                .ToList();
        }
    }
}