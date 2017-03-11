using asp.net_mvc_event_manager.Models;
using asp.net_mvc_event_manager.Repositories;

namespace asp.net_mvc_event_manager.Persistence
{
    public class UnitOfWork
    {
        private ApplicationDbContext _context;

        public EventRepository Events { get; private set; }
        public AttendanceRepository Attendances { get; private set; }
        public FollowingRepository Followings { get; private set; }
        public GenreRepository Genres { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Events = new EventRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}