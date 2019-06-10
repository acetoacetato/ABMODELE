namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbConLogin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductoToIngredientes",
                c => new
                    {
                        ProductoId = c.Int(nullable: false),
                        IngredienteId = c.Int(nullable: false),
                        CantidadProducto = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductoId, t.IngredienteId })
                .ForeignKey("dbo.Ingredientes", t => t.IngredienteId, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.ProductoId)
                .Index(t => t.IngredienteId);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Precio = c.Int(nullable: false),
                        ConJuna = c.Boolean(nullable: false),
                        FechaPreparacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductoId);
            
            CreateTable(
                "dbo.ProductoPersonalizadoes",
                c => new
                    {
                        IdOrden = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Orden_NumOrden = c.Int(),
                        Producto_ProductoId = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdOrden, t.IdProducto })
                .ForeignKey("dbo.Ordens", t => t.Orden_NumOrden)
                .ForeignKey("dbo.Productoes", t => t.Producto_ProductoId)
                .Index(t => t.Orden_NumOrden)
                .Index(t => t.Producto_ProductoId);
            
            CreateTable(
                "dbo.Ordens",
                c => new
                    {
                        NumOrden = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(nullable: false),
                        FechaOrden = c.DateTime(nullable: false),
                        FechaEntrega = c.DateTime(nullable: false),
                        Monto = c.Int(nullable: false),
                        Pagado = c.Boolean(nullable: false),
                        MetodoPago = c.Int(nullable: false),
                        Preparado = c.Boolean(nullable: false),
                        Entregado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NumOrden);
            
            CreateTable(
                "dbo.SinIngredientes",
                c => new
                    {
                        IdProductoPersonalizado = c.Int(nullable: false),
                        IdIngrediente = c.Int(nullable: false),
                        Ingrediente_IngredienteId = c.Int(),
                        ProductoPersonalizado_IdOrden = c.Int(),
                        ProductoPersonalizado_IdProducto = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdProductoPersonalizado, t.IdIngrediente })
                .ForeignKey("dbo.Ingredientes", t => t.Ingrediente_IngredienteId)
                .ForeignKey("dbo.ProductoPersonalizadoes", t => new { t.ProductoPersonalizado_IdOrden, t.ProductoPersonalizado_IdProducto })
                .Index(t => t.Ingrediente_IngredienteId)
                .Index(t => new { t.ProductoPersonalizado_IdOrden, t.ProductoPersonalizado_IdProducto });
            
            AddColumn("dbo.AspNetUsers", "Saldo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductoToIngredientes", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.SinIngredientes", new[] { "ProductoPersonalizado_IdOrden", "ProductoPersonalizado_IdProducto" }, "dbo.ProductoPersonalizadoes");
            DropForeignKey("dbo.SinIngredientes", "Ingrediente_IngredienteId", "dbo.Ingredientes");
            DropForeignKey("dbo.ProductoPersonalizadoes", "Producto_ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.ProductoPersonalizadoes", "Orden_NumOrden", "dbo.Ordens");
            DropForeignKey("dbo.ProductoToIngredientes", "IngredienteId", "dbo.Ingredientes");
            DropIndex("dbo.SinIngredientes", new[] { "ProductoPersonalizado_IdOrden", "ProductoPersonalizado_IdProducto" });
            DropIndex("dbo.SinIngredientes", new[] { "Ingrediente_IngredienteId" });
            DropIndex("dbo.ProductoPersonalizadoes", new[] { "Producto_ProductoId" });
            DropIndex("dbo.ProductoPersonalizadoes", new[] { "Orden_NumOrden" });
            DropIndex("dbo.ProductoToIngredientes", new[] { "IngredienteId" });
            DropIndex("dbo.ProductoToIngredientes", new[] { "ProductoId" });
            DropColumn("dbo.AspNetUsers", "Saldo");
            DropTable("dbo.SinIngredientes");
            DropTable("dbo.Ordens");
            DropTable("dbo.ProductoPersonalizadoes");
            DropTable("dbo.Productoes");
            DropTable("dbo.ProductoToIngredientes");
        }
    }
}
