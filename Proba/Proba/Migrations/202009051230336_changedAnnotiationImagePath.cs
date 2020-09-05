namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedAnnotiationImagePath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Client", "ImagePath", c => c.String(nullable: false));
        }
    }
}
