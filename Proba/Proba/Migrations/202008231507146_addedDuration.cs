namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDuration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Service", "Duration", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Service", "Duration");
        }
    }
}
