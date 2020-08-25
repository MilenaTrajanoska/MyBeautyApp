namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedReservationclass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservation", "Service_Id", "dbo.Service");
            DropIndex("dbo.Reservation", new[] { "Service_Id" });
            RenameColumn(table: "dbo.Reservation", name: "Service_Id", newName: "ServiceId");
            AlterColumn("dbo.Reservation", "ServiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservation", "ServiceId");
            AddForeignKey("dbo.Reservation", "ServiceId", "dbo.Service", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "ServiceId", "dbo.Service");
            DropIndex("dbo.Reservation", new[] { "ServiceId" });
            AlterColumn("dbo.Reservation", "ServiceId", c => c.Int());
            RenameColumn(table: "dbo.Reservation", name: "ServiceId", newName: "Service_Id");
            CreateIndex("dbo.Reservation", "Service_Id");
            AddForeignKey("dbo.Reservation", "Service_Id", "dbo.Service", "Id");
        }
    }
}
