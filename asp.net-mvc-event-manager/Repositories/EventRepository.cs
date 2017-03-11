using asp.net_mvc_event_manager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace asp.net_mvc_event_manager.Repositories
{
    public class EventRepository
    {
        private ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Event GetEventByIdAndArtist(int eventId, string artistId)
        {
            return _context.Events.Single(e => e.Id == eventId && e.ArtistId == artistId);
        }

        public IEnumerable<Event> GetUpcomingEventsByArtist(string userId)
        {
            return _context.Events
                    .Where(e => e.ArtistId == userId &&
                                e.DateTime > DateTime.Now &&
                                !e.IsCanceled)
                    .Include(e => e.Genre)
                    .ToList();
        }

        public Event GetEvent(int eventId)
        {
            return _context.Events
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .SingleOrDefault(e => e.Id == eventId);
        }

        public Event GetEventWithAttendees(int eventId)
        {
            return _context.Events
                .Include(e => e.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(e => e.Id == eventId);
        }

        public IEnumerable<Event> GetEventsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Event)
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .ToList();
        }
    }
}