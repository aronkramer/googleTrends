namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailtrendsbool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "GoogleTrends", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emails", "GoogleTrends");
        }
    }
}
