namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class votes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salon", "Rating", c => c.Single(nullable: false));
            AddColumn("dbo.Salon", "NumReviews", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Salon", "NumReviews");
            DropColumn("dbo.Salon", "Rating");
        }
    }
}
