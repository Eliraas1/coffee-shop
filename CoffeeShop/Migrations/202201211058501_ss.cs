namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "price", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "price");
        }
    }
}
