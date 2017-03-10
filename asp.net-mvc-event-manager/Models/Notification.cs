using System;
using System.ComponentModel.DataAnnotations;

namespace asp.net_mvc_event_manager.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        [Required]
        public Event Event { get; private set; }

        // Available for Entity Framework
        public Notification()
        {

        }

        public Notification(NotificationType type, Event currentEvent)
        {
            if (currentEvent == null)
                throw new ArgumentNullException("currentEvent");

            Type = type;
            Event = currentEvent;
            DateTime = DateTime.Now;
        }
    }
}