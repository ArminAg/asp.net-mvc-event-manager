using asp.net_mvc_event_manager.Core.Models;
using System.Collections.Generic;

namespace asp.net_mvc_event_manager.Core.Repositories
{
    public interface IEventRepository
    {
        void Add(Event newEvent);
        Event GetEvent(int eventId);
        Event GetEventByIdAndArtist(int eventId, string artistId);
        IEnumerable<Event> GetEventsUserAttending(string userId);
        Event GetEventWithAttendees(int eventId);
        IEnumerable<Event> GetUpcomingEventsByArtist(string userId);
    }
}