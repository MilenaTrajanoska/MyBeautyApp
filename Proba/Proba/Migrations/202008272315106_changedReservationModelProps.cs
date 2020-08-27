namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedReservationModelProps : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Salon", "ReservationsAsJson");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Salon", "ReservationsAsJson", c => c.String());
        }
    }
}
