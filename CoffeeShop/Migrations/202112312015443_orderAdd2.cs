namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderAdd2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TableOrder",
                c => new
                {
                    Date = c.String(nullable: false,maxLength: 50),
                    Tid = c.Int(nullable: false),
                    Uid = c.Int(nullable: false),
                    NumberOfSeats = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Date);

        }

        public override void Down()
        {
            DropTable("dbo.TableOrder");
        }

    }
}
