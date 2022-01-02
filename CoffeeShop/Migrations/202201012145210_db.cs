namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {

            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tbls",
                c => new
                    {
                        tid = c.Int(nullable: false, identity: true),
                        amount = c.Int(nullable: false),
                        isIn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.tid);
            
            DropTable("dbo.TableOrder");
        }
    }
}
