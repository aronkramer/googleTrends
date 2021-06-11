namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullsales : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TopReportKWs", "Sales", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TopReportKWs", "Sales", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
