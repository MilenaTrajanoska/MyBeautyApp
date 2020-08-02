namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedService : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Service", name: "Salon_UserId", newName: "SalonId");
            RenameIndex(table: "dbo.Service", name: "IX_Salon_UserId", newName: "IX_SalonId");
            AddColumn("dbo.Service", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Service", "Price");
            RenameIndex(table: "dbo.Service", name: "IX_SalonId", newName: "IX_Salon_UserId");
            RenameColumn(table: "dbo.Service", name: "SalonId", newName: "Salon_UserId");
        }
    }
}
