namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TableOrder",
                c => new
                    {
                        Date = c.String(nullable: false, maxLength: 128),
                        Tid = c.Int(nullable: false),
                        Uid = c.String(),
                        NumberOfSeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Date, t.Tid });
            
            DropTable("dbo.Tbls");
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
