namespace ShoppingListApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HasNotes1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "HasNotes", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Note", "HasNotes");
        }
    }
}
