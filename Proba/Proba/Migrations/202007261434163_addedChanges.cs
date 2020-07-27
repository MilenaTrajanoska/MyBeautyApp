namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropPrimaryKey("dbo.Clients");
            AlterColumn("dbo.Clients", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Clients", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Clients", "Id");
            CreateIndex("dbo.Clients", "UserId");
            AddForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropPrimaryKey("dbo.Clients");
            AlterColumn("dbo.Clients", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Clients", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Clients", "Id");
            CreateIndex("dbo.Clients", "UserId");
            AddForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
