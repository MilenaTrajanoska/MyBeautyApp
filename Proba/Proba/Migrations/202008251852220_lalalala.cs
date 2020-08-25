namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lalalala : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Reservation");
            AlterColumn("dbo.Reservation", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Reservation", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Reservation");
            AlterColumn("dbo.Reservation", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Reservation", "Id");
        }
    }
}
