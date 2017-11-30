namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addphoto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelPhotoes",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 4000),
                        Place_PlaceId = c.Int(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.ModelPlaces", t => t.Place_PlaceId)
                .Index(t => t.Place_PlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelPhotoes", "Place_PlaceId", "dbo.ModelPlaces");
            DropIndex("dbo.ModelPhotoes", new[] { "Place_PlaceId" });
            DropTable("dbo.ModelPhotoes");
        }
    }
}
