namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productInTrends : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trends", "Product", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trends", "Product");
        }
    }
}
