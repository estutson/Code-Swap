namespace ShoppingListApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HasNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingListItem", "HasNotes", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingListItem", "HasNotes");
        }
    }
}
