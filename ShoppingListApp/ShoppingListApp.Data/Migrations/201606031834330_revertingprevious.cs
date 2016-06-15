namespace ShoppingListApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revertingprevious : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Note", "HasNotes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Note", "HasNotes", c => c.Boolean(nullable: false));
        }
    }
}
