namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        PasswordHash = c.String(maxLength: 4000),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ModelUsers", new[] { "Email" });
            DropIndex("dbo.ModelUsers", new[] { "UserName" });
            DropTable("dbo.ModelUsers");
        }
    }
}
