namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedReservations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservation", "Salon_UserId", "dbo.Salon");
            DropIndex("dbo.Reservation", new[] { "Salon_UserId" });
            DropColumn("dbo.Reservation", "Salon_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "Salon_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservation", "Salon_UserId");
            AddForeignKey("dbo.Reservation", "Salon_UserId", "dbo.Salon", "UserId");
        }
    }
}
