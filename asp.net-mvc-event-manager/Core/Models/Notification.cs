using System;

namespace asp.net_mvc_event_manager.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        public Event Event { get; private set; }

        // Available for Entity Framework
        public Notification()
        {

        }

        private Notification(NotificationType type, Event currentEvent)
        {
            if (currentEvent == null)
                throw new ArgumentNullException("currentEvent");

            Type = type;
            Event = currentEvent;
            DateTime = DateTime.Now;
        }

        public static Notification EventCreated(Event createdEvent)
        {
            return new Notification(NotificationType.EventCreated, createdEvent);
        }

        public static Notification EventUpdated(Event newEvent, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.EventUpdated, newEvent);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification EventCanceled(Event canceledEvent)
        {
            return new Notification(NotificationType.EventCanceled, canceledEvent);
        }
    }
}