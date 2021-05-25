namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kwVersioncontrol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TopKeywords", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TopKeywords", "Version");
        }
    }
}
