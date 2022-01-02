namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.users",
                c => new
                    {
                        email = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        password = c.String(),
                        role = c.String(),
                        cb = c.Int(nullable: false),
                        tbl_tid = c.Int(),
                    })
                .PrimaryKey(t => t.email)
                .ForeignKey("dbo.Tbls", t => t.tbl_tid)
                .Index(t => t.tbl_tid);
            
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
            DropForeignKey("dbo.users", "tbl_tid", "dbo.Tbls");
            DropIndex("dbo.users", new[] { "tbl_tid" });
            DropTable("dbo.Tbls");
            DropTable("dbo.users");
        }
    }
}
