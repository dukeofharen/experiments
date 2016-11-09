namespace ToolList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        created = c.DateTime(nullable: false, precision: 0),
                        updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tools",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        siteUrl = c.String(nullable: false, maxLength: 500, storeType: "nvarchar"),
                        downloadUrl = c.String(maxLength: 500, storeType: "nvarchar"),
                        imageUrl = c.String(maxLength: 500, storeType: "nvarchar"),
                        activated = c.Boolean(nullable: false),
                        activationCode = c.String(nullable: false, unicode: false),
                        description = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        version = c.String(maxLength: 20, storeType: "nvarchar"),
                        type = c.Int(nullable: false),
                        license = c.Int(nullable: false),
                        creator = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        creatorSite = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        created = c.DateTime(nullable: false, precision: 0),
                        updated = c.DateTime(nullable: false, precision: 0),
                        categoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.categories", t => t.categoryId, cascadeDelete: true)
                .Index(t => t.categoryId);
            
            CreateTable(
                "dbo.os",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        created = c.DateTime(nullable: false, precision: 0),
                        updated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tool_os",
                c => new
                    {
                        toolId = c.Int(nullable: false),
                        osId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.toolId, t.osId })
                .ForeignKey("dbo.tools", t => t.toolId, cascadeDelete: true)
                .ForeignKey("dbo.os", t => t.osId, cascadeDelete: true)
                .Index(t => t.toolId)
                .Index(t => t.osId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tool_os", "osId", "dbo.os");
            DropForeignKey("dbo.tool_os", "toolId", "dbo.tools");
            DropForeignKey("dbo.tools", "categoryId", "dbo.categories");
            DropIndex("dbo.tool_os", new[] { "osId" });
            DropIndex("dbo.tool_os", new[] { "toolId" });
            DropIndex("dbo.tools", new[] { "categoryId" });
            DropTable("dbo.tool_os");
            DropTable("dbo.os");
            DropTable("dbo.tools");
            DropTable("dbo.categories");
        }
    }
}
