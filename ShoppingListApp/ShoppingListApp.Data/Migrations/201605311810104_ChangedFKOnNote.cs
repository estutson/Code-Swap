namespace ShoppingListApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedFKOnNote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShoppingListItemId = c.Int(nullable: false),
                        Body = c.String(),
                        CreatedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Note");
        }
    }
}
