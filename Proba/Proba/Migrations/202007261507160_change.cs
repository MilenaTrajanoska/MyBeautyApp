namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers");
            DropPrimaryKey("dbo.Clients");
            AddPrimaryKey("dbo.Clients", "UserId");
            AddForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers");
            DropPrimaryKey("dbo.Clients");
            AddPrimaryKey("dbo.Clients", "Id");
            AddForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
