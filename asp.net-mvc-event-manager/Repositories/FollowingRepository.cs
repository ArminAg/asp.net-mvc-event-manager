using asp.net_mvc_event_manager.Models;
using System.Linq;

namespace asp.net_mvc_event_manager.Repositories
{
    public class FollowingRepository
    {
        private ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string followeeId, string followerId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);
        }
    }
}