using asp.net_mvc_event_manager.Core.Repositories;

namespace asp.net_mvc_event_manager.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IEventRepository Events { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IApplicationUserRepository Users { get; }
        INotificationRepository Notifications { get; }
        IUserNotificationRepository UserNotifications { get; }

        void Complete();
    }
}