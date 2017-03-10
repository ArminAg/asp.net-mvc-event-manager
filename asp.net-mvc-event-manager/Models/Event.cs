using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace asp.net_mvc_event_manager.Models
{
    public class Event
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }
        
        public Genre Genre { get; set; }


        [Required]
        public string ArtistId { get; set; }
        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Event()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = new Notification(NotificationType.EventCanceled, this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }
    }
}