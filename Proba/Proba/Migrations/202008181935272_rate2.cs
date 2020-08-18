namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salon", "RatePoints", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Salon", "RatePoints");
        }
    }
}
