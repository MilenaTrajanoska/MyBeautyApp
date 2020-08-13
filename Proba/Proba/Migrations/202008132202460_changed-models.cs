namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedmodels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Client", "ImagePath", c => c.String());
        }
    }
}
