namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedthereservations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservation", "Client_UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Reservation", "Salon_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservation", "Client_UserId");
            CreateIndex("dbo.Reservation", "Salon_UserId");
            AddForeignKey("dbo.Reservation", "Client_UserId", "dbo.Client", "UserId");
            AddForeignKey("dbo.Reservation", "Salon_UserId", "dbo.Salon", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "Salon_UserId", "dbo.Salon");
            DropForeignKey("dbo.Reservation", "Client_UserId", "dbo.Client");
            DropIndex("dbo.Reservation", new[] { "Salon_UserId" });
            DropIndex("dbo.Reservation", new[] { "Client_UserId" });
            DropColumn("dbo.Reservation", "Salon_UserId");
            DropColumn("dbo.Reservation", "Client_UserId");
        }
    }
}
