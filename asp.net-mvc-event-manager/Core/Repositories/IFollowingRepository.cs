using asp.net_mvc_event_manager.Core.Models;

namespace asp.net_mvc_event_manager.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followeeId, string followerId);
    }
}