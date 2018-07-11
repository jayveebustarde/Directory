namespace Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        PercentDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Details = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductDiscounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        DiscountId = c.Guid(nullable: false),
                        VariationId = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discounts", t => t.DiscountId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Variations", t => t.VariationId)
                .Index(t => t.ProductId)
                .Index(t => t.DiscountId)
                .Index(t => t.VariationId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        RegularPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        ProductTypeId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId, cascadeDelete: true)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Variations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        ProductId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDiscounts", "VariationId", "dbo.Variations");
            DropForeignKey("dbo.Variations", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductDiscounts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductDiscounts", "DiscountId", "dbo.Discounts");
            DropIndex("dbo.Variations", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.ProductDiscounts", new[] { "VariationId" });
            DropIndex("dbo.ProductDiscounts", new[] { "DiscountId" });
            DropIndex("dbo.ProductDiscounts", new[] { "ProductId" });
            DropTable("dbo.Variations");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.ProductDiscounts");
            DropTable("dbo.Discounts");
        }
    }
}
