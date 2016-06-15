namespace ShoppingListApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedmorefields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingListItem", "CreatedUTC", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ShoppingListItem", "ModifiedUTC", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingListItem", "ModifiedUTC");
            DropColumn("dbo.ShoppingListItem", "CreatedUTC");
        }
    }
}
