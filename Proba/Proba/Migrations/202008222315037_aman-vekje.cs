namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amanvekje : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservation", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservation", "Date");
        }
    }
}
