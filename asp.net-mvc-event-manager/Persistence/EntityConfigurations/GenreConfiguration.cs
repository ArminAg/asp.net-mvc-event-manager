using asp.net_mvc_event_manager.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace asp.net_mvc_event_manager.Persistence.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}