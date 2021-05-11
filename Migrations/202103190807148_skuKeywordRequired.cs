namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skuKeywordRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TopKeywords", "SKU", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("dbo.TopKeywords", "Keyword", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TopKeywords", "Keyword", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.TopKeywords", "SKU", c => c.String(maxLength: 150, unicode: false));
        }
    }
}
