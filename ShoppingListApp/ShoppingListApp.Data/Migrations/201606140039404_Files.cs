namespace ShoppingListApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        ListItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.ShoppingListItem", t => t.ListItem_Id)
                .Index(t => t.ListItem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "ListItem_Id", "dbo.ShoppingListItem");
            DropIndex("dbo.File", new[] { "ListItem_Id" });
            DropTable("dbo.File");
        }
    }
}
