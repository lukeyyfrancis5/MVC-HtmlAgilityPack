namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        LedgerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ledgers", t => t.LedgerID, cascadeDelete: true)
                .Index(t => t.LedgerID);
            
            CreateTable(
                "dbo.Ledgers",
                c => new
                    {
                        LedgerId = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LedgerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coins", "LedgerID", "dbo.Ledgers");
            DropIndex("dbo.Coins", new[] { "LedgerID" });
            DropTable("dbo.Ledgers");
            DropTable("dbo.Coins");
        }
    }
}
