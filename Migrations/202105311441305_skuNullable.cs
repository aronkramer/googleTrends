namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skuNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CampaignsReports", "Sku", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CampaignsReports", "Sku", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
