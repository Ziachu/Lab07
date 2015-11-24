namespace UamTTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetTransfer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PlannedDate = c.DateTime(),
                        TransferDate = c.DateTime(),
                        Reference = c.String(),
                        Budget_Id = c.Int(),
                        DestinationAccount_Id = c.Int(),
                        SourceAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Budgets", t => t.Budget_Id)
                .ForeignKey("dbo.Accounts", t => t.DestinationAccount_Id)
                .ForeignKey("dbo.Accounts", t => t.SourceAccount_Id)
                .Index(t => t.Budget_Id)
                .Index(t => t.DestinationAccount_Id)
                .Index(t => t.SourceAccount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transfers", "SourceAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transfers", "DestinationAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transfers", "Budget_Id", "dbo.Budgets");
            DropIndex("dbo.Transfers", new[] { "SourceAccount_Id" });
            DropIndex("dbo.Transfers", new[] { "DestinationAccount_Id" });
            DropIndex("dbo.Transfers", new[] { "Budget_Id" });
            DropTable("dbo.Transfers");
            DropTable("dbo.Budgets");
        }
    }
}
