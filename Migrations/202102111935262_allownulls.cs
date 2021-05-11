namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allownulls : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TopKeywords", "Asin", c => c.String(maxLength: 40, unicode: false));
            AlterColumn("dbo.TopKeywords", "Keyword", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TopKeywords", "Keyword", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("dbo.TopKeywords", "Asin", c => c.String(nullable: false, maxLength: 40, unicode: false));
        }
    }
}
