namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedworkinghours : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salon", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Salon", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Salon", "EndTime");
            DropColumn("dbo.Salon", "StartTime");
        }
    }
}
