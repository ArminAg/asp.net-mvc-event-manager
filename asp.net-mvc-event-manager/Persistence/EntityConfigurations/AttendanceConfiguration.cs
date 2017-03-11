using asp.net_mvc_event_manager.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace asp.net_mvc_event_manager.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => new { a.EventId, a.AttendeeId });
        }
    }
}