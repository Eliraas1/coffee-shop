namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adsa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.drinks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        img = c.String(),
                        price = c.String(),
                        isAlcohol = c.Boolean(nullable: false),
                        amount = c.Int(nullable: false),
                        popular = c.Int(nullable: false),
                        isBusiness = c.Boolean(nullable: false, defaultValue:false),
                    })
                .PrimaryKey(t => t.id);
            
            
        }
        
        public override void Down()
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
            
            DropTable("dbo.drinks");
        }
    }
}
