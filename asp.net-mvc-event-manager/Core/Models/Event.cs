using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace asp.net_mvc_event_manager.Core.Models
{
    public class Event
    {
        public int Id { get; set; }
        public bool IsCanceled { get; private set; }
        public ApplicationUser Artist { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public Genre Genre { get; set; }
        public string ArtistId { get; set; }
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Event()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.EventCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

        public void Modify(DateTime dateTime, string venue, byte genreId)
        {
            var notification = Notification.EventUpdated(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genreId;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }
    }
}