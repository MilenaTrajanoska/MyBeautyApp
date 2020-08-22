namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientId = c.String(),
                        SalonId = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Notes = c.String(),
                        Service_Id = c.Int(),
                        Salon_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Service", t => t.Service_Id)
                .ForeignKey("dbo.Salon", t => t.Salon_UserId)
                .Index(t => t.Service_Id)
                .Index(t => t.Salon_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "Salon_UserId", "dbo.Salon");
            DropForeignKey("dbo.Reservation", "Service_Id", "dbo.Service");
            DropIndex("dbo.Reservation", new[] { "Salon_UserId" });
            DropIndex("dbo.Reservation", new[] { "Service_Id" });
            DropTable("dbo.Reservation");
        }
    }
}
