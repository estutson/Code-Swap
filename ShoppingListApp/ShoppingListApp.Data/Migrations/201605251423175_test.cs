namespace ShoppingListApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingListItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShoppingListId = c.Int(nullable: false),
                        Contents = c.String(),
                        IsChecked = c.Boolean(nullable: false),
                        CreatedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingList", t => t.ShoppingListId, cascadeDelete: true)
                .Index(t => t.ShoppingListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingListItem", "ShoppingListId", "dbo.ShoppingList");
            DropIndex("dbo.ShoppingListItem", new[] { "ShoppingListId" });
            DropTable("dbo.ShoppingListItem");
        }
    }
}
