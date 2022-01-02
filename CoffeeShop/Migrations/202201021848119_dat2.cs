namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dat2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.users",
                c => new
                    {
                        uid = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        email = c.String(),
                        password = c.String(),
                        role = c.String(),
                        coffeBought = c.Int(nullable: false),
                        age = c.Int(nullable: false),
                        isVip = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.uid);
            
        }
        
        public override void Down()
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
            
            DropTable("dbo.users");
        }
    }
}
