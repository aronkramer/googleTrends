namespace BillTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Keyword = c.String(maxLength: 150, unicode: false),
                        Date = c.String(maxLength: 150, unicode: false),
                        Ranking = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        NoData = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trends");
        }
    }
}
