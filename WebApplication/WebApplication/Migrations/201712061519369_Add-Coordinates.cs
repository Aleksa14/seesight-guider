namespace WebApplication.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddCoordinates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModelPlaces", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.ModelPlaces", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModelPlaces", "Longitude");
            DropColumn("dbo.ModelPlaces", "Latitude");
        }
    }
}
