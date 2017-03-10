using System;
using System.ComponentModel.DataAnnotations;

namespace asp.net_mvc_event_manager.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        [Required]
        public Event Event { get; set; }
    }
}