using asp.net_mvc_event_manager.Core.Models;

namespace asp.net_mvc_event_manager.Core.ViewModels
{
    public class EventDetailsViewModel
    {
        public Event Event { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}