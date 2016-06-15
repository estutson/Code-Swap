namespace ShoppingListApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUserIDToGuid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingList", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.ShoppingList", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingList", "OwnerId", c => c.Guid(nullable: false));
            DropColumn("dbo.ShoppingList", "UserId");
        }
    }
}
