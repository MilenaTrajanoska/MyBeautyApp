namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lalalala1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Reservation");
            AlterColumn("dbo.Reservation", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Reservation", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Reservation");
            AlterColumn("dbo.Reservation", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Reservation", "Id");
        }
    }
}
