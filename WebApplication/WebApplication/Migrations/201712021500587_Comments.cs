namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelComments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentMessage = c.String(maxLength: 4000),
                        Author_UserId = c.Int(),
                        Place_PlaceId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.ModelUsers", t => t.Author_UserId)
                .ForeignKey("dbo.ModelPlaces", t => t.Place_PlaceId)
                .Index(t => t.Author_UserId)
                .Index(t => t.Place_PlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelComments", "Place_PlaceId", "dbo.ModelPlaces");
            DropForeignKey("dbo.ModelComments", "Author_UserId", "dbo.ModelUsers");
            DropIndex("dbo.ModelComments", new[] { "Place_PlaceId" });
            DropIndex("dbo.ModelComments", new[] { "Author_UserId" });
            DropTable("dbo.ModelComments");
        }
    }
}
