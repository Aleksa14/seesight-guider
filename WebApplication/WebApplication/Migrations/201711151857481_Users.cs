namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelPlaces",
                c => new
                    {
                        PlaceId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Rate = c.Double(nullable: false),
                        Address = c.String(maxLength: 4000),
                        Author_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PlaceId)
                .ForeignKey("dbo.ModelUsers", t => t.Author_UserId)
                .Index(t => t.Author_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelPlaces", "Author_UserId", "dbo.ModelUsers");
            DropIndex("dbo.ModelPlaces", new[] { "Author_UserId" });
            DropTable("dbo.ModelPlaces");
        }
    }
}
