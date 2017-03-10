﻿using asp.net_mvc_event_manager.Models;
using System;

namespace asp.net_mvc_event_manager.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public EventDto Event { get; set; }
    }
}