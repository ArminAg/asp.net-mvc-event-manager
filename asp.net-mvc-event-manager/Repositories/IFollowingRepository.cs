using asp.net_mvc_event_manager.Models;

namespace asp.net_mvc_event_manager.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followeeId, string followerId);
    }
}