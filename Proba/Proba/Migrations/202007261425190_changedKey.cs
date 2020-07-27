namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Clients");
            AlterColumn("dbo.Clients", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Clients");
            AlterColumn("dbo.Clients", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Clients", "Id");
        }
    }
}
