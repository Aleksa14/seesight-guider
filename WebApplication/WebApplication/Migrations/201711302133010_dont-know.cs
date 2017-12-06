namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dontknow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModelPhotoes", "Url", c => c.String(maxLength: 4000));
            DropColumn("dbo.ModelPhotoes", "FileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModelPhotoes", "FileName", c => c.String(maxLength: 4000));
            DropColumn("dbo.ModelPhotoes", "Url");
        }
    }
}
