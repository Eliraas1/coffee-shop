namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl1 : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbls");
        }
    }
}
