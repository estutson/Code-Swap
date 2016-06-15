namespace ShoppingListApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingListItem", "ShoppingListId", "dbo.ShoppingList");
            DropIndex("dbo.ShoppingListItem", new[] { "ShoppingListId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ShoppingListItem", "ShoppingListId");
            AddForeignKey("dbo.ShoppingListItem", "ShoppingListId", "dbo.ShoppingList", "Id", cascadeDelete: true);
        }
    }
}
