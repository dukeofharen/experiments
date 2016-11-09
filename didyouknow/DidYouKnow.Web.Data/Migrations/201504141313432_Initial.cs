namespace DidYouKnow.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        description = c.String(nullable: false, maxLength: 500, storeType: "nvarchar"),
                        image = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        created = c.DateTime(nullable: false, precision: 0),
                        last_modified = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.facts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        fact = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        source = c.String(maxLength: 500, storeType: "nvarchar"),
                        created = c.DateTime(nullable: false, precision: 0),
                        last_modified = c.DateTime(nullable: false, precision: 0),
                        category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.categories", t => t.category_id, cascadeDelete: true)
                .Index(t => t.category_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.facts", "category_id", "dbo.categories");
            DropIndex("dbo.facts", new[] { "category_id" });
            DropTable("dbo.facts");
            DropTable("dbo.categories");
        }
    }
}
