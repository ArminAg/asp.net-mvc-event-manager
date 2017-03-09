using System;
using System.ComponentModel.DataAnnotations;

namespace asp.net_mvc_event_manager.Models
{
    public class Event
    {
        public int Id { get; set; }
        
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
    }
}