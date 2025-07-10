namespace Ecomm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPrice = c.Decimal(precision: 18, scale: 2),
                        SKU = c.String(maxLength: 50),
                        StockQuantity = c.Int(nullable: false),
                        ImageURL = c.String(),
                        CategoryID = c.Int(nullable: false),
                        BrandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.BrandID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderItemID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        OrderTime = c.DateTime(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(maxLength: 50),
                        ShippingAddress = c.String(nullable: false),
                        CustomerName = c.String(maxLength: 100),
                        CustomerPhone = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.String(nullable: false),
                        Role = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.PcBuilds",
                c => new
                    {
                        BuildID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        BuildName = c.String(nullable: false, maxLength: 150),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BuildID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.PcBuildComponents",
                c => new
                    {
                        BuildComponentID = c.Int(nullable: false, identity: true),
                        BuildID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BuildComponentID)
                .ForeignKey("dbo.PcBuilds", t => t.BuildID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.BuildID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductSpecifications",
                c => new
                    {
                        SpecificationID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        SpecKey = c.String(nullable: false, maxLength: 100),
                        SpecValue = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.SpecificationID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSpecifications", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.PcBuilds", "UserID", "dbo.Users");
            DropForeignKey("dbo.PcBuildComponents", "ProductID", "dbo.Products");
            DropForeignKey("dbo.PcBuildComponents", "BuildID", "dbo.PcBuilds");
            DropForeignKey("dbo.Orders", "UserID", "dbo.Users");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropIndex("dbo.ProductSpecifications", new[] { "ProductID" });
            DropIndex("dbo.PcBuildComponents", new[] { "ProductID" });
            DropIndex("dbo.PcBuildComponents", new[] { "BuildID" });
            DropIndex("dbo.PcBuilds", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.OrderItems", new[] { "ProductID" });
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.ProductSpecifications");
            DropTable("dbo.PcBuildComponents");
            DropTable("dbo.PcBuilds");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
