﻿namespace asp.net_mvc_event_manager.Core.Models
{
    public class Attendance
    {
        public Event Event { get; set; }
        public ApplicationUser Attendee { get; set; }
        public int EventId { get; set; }
        public string AttendeeId { get; set; }
    }
}