using asp.net_mvc_event_manager.Models;
using System.Collections.Generic;

namespace asp.net_mvc_event_manager.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Event> UpcomingEvents { get; set; }
        public bool ShowActions { get; set; }
    }
}