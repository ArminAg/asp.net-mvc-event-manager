namespace asp.net_mvc_event_manager.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (id, Name) VALUES (1, 'Jazz')");
            Sql("INSERT INTO Genres (id, Name) VALUES (2, 'Blues')");
            Sql("INSERT INTO Genres (id, Name) VALUES (3, 'Rock')");
            Sql("INSERT INTO Genres (id, Name) VALUES (4, 'Electronic')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
