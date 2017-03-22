namespace asp.net_mvc_event_manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeForeignKeyEventIdNameOnNotifications : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Notifications", name: "Event_Id", newName: "EventId");
            RenameIndex(table: "dbo.Notifications", name: "IX_Event_Id", newName: "IX_EventId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Notifications", name: "IX_EventId", newName: "IX_Event_Id");
            RenameColumn(table: "dbo.Notifications", name: "EventId", newName: "Event_Id");
        }
    }
}
