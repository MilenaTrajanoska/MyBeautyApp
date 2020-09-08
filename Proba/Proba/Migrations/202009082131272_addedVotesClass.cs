namespace Proba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedVotesClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vote",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SalonId = c.String(maxLength: 128),
                        ClientId = c.String(maxLength: 128),
                        vote = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Client", t => t.ClientId)
                .ForeignKey("dbo.Salon", t => t.SalonId)
                .Index(t => t.SalonId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vote", "SalonId", "dbo.Salon");
            DropForeignKey("dbo.Vote", "ClientId", "dbo.Client");
            DropIndex("dbo.Vote", new[] { "ClientId" });
            DropIndex("dbo.Vote", new[] { "SalonId" });
            DropTable("dbo.Vote");
        }
    }
}
