using asp.net_mvc_event_manager.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace asp.net_mvc_event_manager.Persistence.EntityConfigurations
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            Property(e => e.ArtistId)
                .IsRequired();

            Property(e => e.GenreId)
                .IsRequired();

            Property(e => e.Venue)
                .IsRequired()
                .HasMaxLength(255);
            
            HasMany(e => e.Attendances)
                .WithRequired(a => a.Event)
                .WillCascadeOnDelete(false);
        }
    }
}