namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedforeignkey : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reservation", new[] { "Client_UserId" });
            DropIndex("dbo.Reservation", new[] { "Salon_UserId" });
            DropColumn("dbo.Reservation", "ClientId");
            DropColumn("dbo.Reservation", "SalonId");
            RenameColumn(table: "dbo.Reservation", name: "Client_UserId", newName: "ClientId");
            RenameColumn(table: "dbo.Reservation", name: "Salon_UserId", newName: "SalonId");
            AlterColumn("dbo.Reservation", "ClientId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Reservation", "SalonId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservation", "ClientId");
            CreateIndex("dbo.Reservation", "SalonId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reservation", new[] { "SalonId" });
            DropIndex("dbo.Reservation", new[] { "ClientId" });
            AlterColumn("dbo.Reservation", "SalonId", c => c.String());
            AlterColumn("dbo.Reservation", "ClientId", c => c.String());
            RenameColumn(table: "dbo.Reservation", name: "SalonId", newName: "Salon_UserId");
            RenameColumn(table: "dbo.Reservation", name: "ClientId", newName: "Client_UserId");
            AddColumn("dbo.Reservation", "SalonId", c => c.String());
            AddColumn("dbo.Reservation", "ClientId", c => c.String());
            CreateIndex("dbo.Reservation", "Salon_UserId");
            CreateIndex("dbo.Reservation", "Client_UserId");
        }
    }
}
