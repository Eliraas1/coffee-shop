namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbls", "taken", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbls", "taken");
        }
    }
}
