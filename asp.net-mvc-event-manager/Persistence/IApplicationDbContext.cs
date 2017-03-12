using asp.net_mvc_event_manager.Core.Models;
using System.Data.Entity;

namespace asp.net_mvc_event_manager.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}