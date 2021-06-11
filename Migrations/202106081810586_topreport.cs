namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topreport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopReportKWs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sku = c.String(maxLength: 250, unicode: false),
                        SearchTerm = c.String(maxLength: 250, unicode: false),
                        Sales = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TopReportKWs");
        }
    }
}
