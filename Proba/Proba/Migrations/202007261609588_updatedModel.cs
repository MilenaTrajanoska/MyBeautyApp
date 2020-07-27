namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clients", newName: "Client");
            DropIndex("dbo.Client", new[] { "UserId" });
            DropPrimaryKey("dbo.Client");
            AddColumn("dbo.Client", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Client", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Client", "Id");
            CreateIndex("dbo.Client", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Client", new[] { "UserId" });
            DropPrimaryKey("dbo.Client");
            AlterColumn("dbo.Client", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Client", "Id");
            AddPrimaryKey("dbo.Client", "UserId");
            CreateIndex("dbo.Client", "UserId");
            RenameTable(name: "dbo.Client", newName: "Clients");
        }
    }
}
