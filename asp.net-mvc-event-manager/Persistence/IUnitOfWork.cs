using asp.net_mvc_event_manager.Repositories;

namespace asp.net_mvc_event_manager.Persistence
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IEventRepository Events { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }

        void Complete();
    }
}