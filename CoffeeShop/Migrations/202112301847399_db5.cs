namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.users", "tbl_tid", "dbo.Tbls");
            DropIndex("dbo.users", new[] { "tbl_tid" });
            DropPrimaryKey("dbo.Tbls");
            AddColumn("dbo.users", "tbl_Date", c => c.DateTime());
            AlterColumn("dbo.Tbls", "tid", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Tbls", new[] { "tid", "Date" });
            CreateIndex("dbo.users", new[] { "tbl_tid", "tbl_Date" });
            AddForeignKey("dbo.users", new[] { "tbl_tid", "tbl_Date" }, "dbo.Tbls", new[] { "tid", "Date" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.users", new[] { "tbl_tid", "tbl_Date" }, "dbo.Tbls");
            DropIndex("dbo.users", new[] { "tbl_tid", "tbl_Date" });
            DropPrimaryKey("dbo.Tbls");
            AlterColumn("dbo.Tbls", "tid", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.users", "tbl_Date");
            AddPrimaryKey("dbo.Tbls", "tid");
            CreateIndex("dbo.users", "tbl_tid");
            AddForeignKey("dbo.users", "tbl_tid", "dbo.Tbls", "tid");
        }
    }
}
