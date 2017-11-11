namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ModelUsers", new[] { "Mail" });
            AddColumn("dbo.ModelUsers", "Email", c => c.String(maxLength: 4000));
            CreateIndex("dbo.ModelUsers", "Email", unique: true);
            DropColumn("dbo.ModelUsers", "Mail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModelUsers", "Mail", c => c.String(maxLength: 4000));
            DropIndex("dbo.ModelUsers", new[] { "Email" });
            DropColumn("dbo.ModelUsers", "Email");
            CreateIndex("dbo.ModelUsers", "Mail", unique: true);
        }
    }
}
