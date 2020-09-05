namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedAttrRequiredSalon : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Salon", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Salon", "ImagePath", c => c.String(nullable: false));
        }
    }
}
