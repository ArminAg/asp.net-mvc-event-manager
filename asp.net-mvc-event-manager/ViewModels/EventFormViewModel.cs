using asp.net_mvc_event_manager.Models;
using System.Collections.Generic;

namespace asp.net_mvc_event_manager.ViewModels
{
    public class EventFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public byte GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}