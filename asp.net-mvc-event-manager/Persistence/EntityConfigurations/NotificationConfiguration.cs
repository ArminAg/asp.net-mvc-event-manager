using asp.net_mvc_event_manager.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace asp.net_mvc_event_manager.Persistence.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            HasRequired(n => n.Event);
        }
    }
}