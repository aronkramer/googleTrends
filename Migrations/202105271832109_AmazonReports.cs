namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmazonReports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CampaignsReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Campaign = c.String(nullable: false, maxLength: 250),
                        Sku = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SearchTermReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Campaign = c.String(nullable: false, maxLength: 250),
                        SearchTerm = c.String(nullable: false, maxLength: 250),
                        Sales = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Units = c.Int(nullable: false),
                        Acos = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SearchTermReports");
            DropTable("dbo.CampaignsReports");
        }
    }
}
