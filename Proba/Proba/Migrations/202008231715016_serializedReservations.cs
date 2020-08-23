namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serializedReservations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salon", "ReservationsAsJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Salon", "ReservationsAsJson");
        }
    }
}
