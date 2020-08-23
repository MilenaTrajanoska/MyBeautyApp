namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedDurationType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Service", "Duration", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Service", "Duration", c => c.Time(nullable: false, precision: 7));
        }
    }
}
