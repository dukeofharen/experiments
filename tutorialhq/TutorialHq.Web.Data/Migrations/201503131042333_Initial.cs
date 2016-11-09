namespace TutorialHq.Web.Data.Migrations
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
                        category_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        description = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        created = c.DateTime(nullable: false, precision: 0),
                        last_modified = c.DateTime(nullable: false, precision: 0),
                        ip = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        parent_id = c.Int(),
                    })
                .PrimaryKey(t => t.category_id)
                .ForeignKey("dbo.categories", t => t.parent_id)
                .Index(t => t.parent_id);
            
            CreateTable(
                "dbo.tutorials",
                c => new
                    {
                        tutorial_id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        description = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        url = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        num_votes = c.Int(nullable: false),
                        num_clicks = c.Int(nullable: false),
                        num_comments = c.Int(nullable: false),
                        avg_rating = c.Double(nullable: false),
                        status = c.Int(nullable: false),
                        created = c.DateTime(nullable: false, precision: 0),
                        last_modified = c.DateTime(nullable: false, precision: 0),
                        ip = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        user_id = c.Int(nullable: false),
                        category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.tutorial_id)
                .ForeignKey("dbo.users", t => t.user_id, cascadeDelete: true)
                .ForeignKey("dbo.categories", t => t.category_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.clicks",
                c => new
                    {
                        click_id = c.Int(nullable: false, identity: true),
                        created = c.DateTime(nullable: false, precision: 0),
                        last_modified = c.DateTime(nullable: false, precision: 0),
                        ip = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        user_id = c.Int(),
                        tutorial_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.click_id)
                .ForeignKey("dbo.users", t => t.user_id)
                .ForeignKey("dbo.tutorials", t => t.tutorial_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.tutorial_id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        user_id = c.Int(nullable: false, identity: true),
                        user_name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        password_hash = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        email = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        last_login = c.DateTime(nullable: false, precision: 0),
                        user_role = c.Int(nullable: false),
                        name = c.String(maxLength: 100, storeType: "nvarchar"),
                        location = c.String(maxLength: 100, storeType: "nvarchar"),
                        website = c.String(maxLength: 100, storeType: "nvarchar"),
                        activated = c.Boolean(nullable: false),
                        activation_code = c.String(nullable: false, unicode: false),
                        created = c.DateTime(nullable: false, precision: 0),
                        last_modified = c.DateTime(nullable: false, precision: 0),
                        ip = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.user_id)
                .Index(t => t.user_name, unique: true, name: "UserNameUnique")
                .Index(t => t.email, unique: true, name: "EmailUnique");
            
            CreateTable(
                "dbo.comments",
                c => new
                    {
                        comment_id = c.Int(nullable: false, identity: true),
                        content = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        created = c.DateTime(nullable: false, precision: 0),
                        last_modified = c.DateTime(nullable: false, precision: 0),
                        ip = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        user_id = c.Int(nullable: false),
                        tutorial_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.comment_id)
                .ForeignKey("dbo.users", t => t.user_id, cascadeDelete: true)
                .ForeignKey("dbo.tutorials", t => t.tutorial_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.tutorial_id);
            
            CreateTable(
                "dbo.votes",
                c => new
                    {
                        vote_id = c.Int(nullable: false, identity: true),
                        rating = c.Int(nullable: false),
                        created = c.DateTime(nullable: false, precision: 0),
                        last_modified = c.DateTime(nullable: false, precision: 0),
                        ip = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        user_id = c.Int(nullable: false),
                        tutorial_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.vote_id)
                .ForeignKey("dbo.users", t => t.user_id, cascadeDelete: true)
                .ForeignKey("dbo.tutorials", t => t.tutorial_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.tutorial_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tutorials", "category_id", "dbo.categories");
            DropForeignKey("dbo.votes", "tutorial_id", "dbo.tutorials");
            DropForeignKey("dbo.comments", "tutorial_id", "dbo.tutorials");
            DropForeignKey("dbo.clicks", "tutorial_id", "dbo.tutorials");
            DropForeignKey("dbo.votes", "user_id", "dbo.users");
            DropForeignKey("dbo.tutorials", "user_id", "dbo.users");
            DropForeignKey("dbo.comments", "user_id", "dbo.users");
            DropForeignKey("dbo.clicks", "user_id", "dbo.users");
            DropForeignKey("dbo.categories", "parent_id", "dbo.categories");
            DropIndex("dbo.votes", new[] { "tutorial_id" });
            DropIndex("dbo.votes", new[] { "user_id" });
            DropIndex("dbo.comments", new[] { "tutorial_id" });
            DropIndex("dbo.comments", new[] { "user_id" });
            DropIndex("dbo.users", "EmailUnique");
            DropIndex("dbo.users", "UserNameUnique");
            DropIndex("dbo.clicks", new[] { "tutorial_id" });
            DropIndex("dbo.clicks", new[] { "user_id" });
            DropIndex("dbo.tutorials", new[] { "category_id" });
            DropIndex("dbo.tutorials", new[] { "user_id" });
            DropIndex("dbo.categories", new[] { "parent_id" });
            DropTable("dbo.votes");
            DropTable("dbo.comments");
            DropTable("dbo.users");
            DropTable("dbo.clicks");
            DropTable("dbo.tutorials");
            DropTable("dbo.categories");
        }
    }
}
