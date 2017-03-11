using System.Collections.Generic;
using asp.net_mvc_event_manager.Models;

namespace asp.net_mvc_event_manager.Repositories
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