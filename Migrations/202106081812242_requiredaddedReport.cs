namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredaddedReport : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TopReportKWs", "Sku", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.TopReportKWs", "SearchTerm", c => c.String(nullable: false, maxLength: 250, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TopReportKWs", "SearchTerm", c => c.String(maxLength: 250, unicode: false));
            AlterColumn("dbo.TopReportKWs", "Sku", c => c.String(maxLength: 250, unicode: false));
        }
    }
}
