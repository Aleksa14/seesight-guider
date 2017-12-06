namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelRates",
                c => new
                    {
                        RateId = c.Int(nullable: false, identity: true),
                        Rate = c.Int(nullable: false),
                        RatedPlace_PlaceId = c.Int(),
                        ModelUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.RateId)
                .ForeignKey("dbo.ModelPlaces", t => t.RatedPlace_PlaceId)
                .ForeignKey("dbo.ModelUsers", t => t.ModelUser_UserId)
                .Index(t => t.RatedPlace_PlaceId)
                .Index(t => t.ModelUser_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelRates", "ModelUser_UserId", "dbo.ModelUsers");
            DropForeignKey("dbo.ModelRates", "RatedPlace_PlaceId", "dbo.ModelPlaces");
            DropIndex("dbo.ModelRates", new[] { "ModelUser_UserId" });
            DropIndex("dbo.ModelRates", new[] { "RatedPlace_PlaceId" });
            DropTable("dbo.ModelRates");
        }
    }
}
