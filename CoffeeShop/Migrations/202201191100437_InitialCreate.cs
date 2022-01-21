namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false),
                        did = c.Int(nullable: false),
                        uid = c.String(),
                        amount = c.Int(nullable: false),
                        confirm = c.Boolean(nullable: false),
                        tid = c.Int(nullable: false),
                        tdate = c.String(),
                        date = c.String(),
                        take = c.Boolean(nullable: false),
                        
                })
                .PrimaryKey(t => new { t.id, t.did });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
