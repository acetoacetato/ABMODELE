namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriasProductos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CategoriaToProductoes",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoriaId, t.ProductoId })
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.CategoriaId)
                .Index(t => t.ProductoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoriaToProductoes", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.CategoriaToProductoes", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.CategoriaToProductoes", new[] { "ProductoId" });
            DropIndex("dbo.CategoriaToProductoes", new[] { "CategoriaId" });
            DropTable("dbo.CategoriaToProductoes");
            DropTable("dbo.Categorias");
        }
    }
}
