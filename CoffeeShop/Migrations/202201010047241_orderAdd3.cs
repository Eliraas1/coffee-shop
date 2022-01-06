namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderAdd3 : DbMigration
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
            CreateTable(
                "dbo.TableOrder",
                c => new
                    {
                        Date = c.String(nullable: false, maxLength: 128),
                        Tid = c.Int(nullable: false),
                        Uid = c.Int(nullable: false),
                        NumberOfSeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Date, t.Tid });
            
            DropTable("dbo.Tbls");
        }
    }
}
