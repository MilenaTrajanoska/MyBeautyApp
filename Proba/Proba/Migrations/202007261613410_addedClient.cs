namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedClient : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Client", new[] { "UserId" });
            DropPrimaryKey("dbo.Client");
            AlterColumn("dbo.Client", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Client", "UserId");
            CreateIndex("dbo.Client", "UserId");
            DropColumn("dbo.Client", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Client", "Id", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.Client", new[] { "UserId" });
            DropPrimaryKey("dbo.Client");
            AlterColumn("dbo.Client", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Client", "Id");
            CreateIndex("dbo.Client", "UserId");
        }
    }
}
