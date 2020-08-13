namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedsalonmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Salon", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Salon", "ImagePath");
        }
    }
}
