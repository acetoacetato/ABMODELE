namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConSinIngredientes",
                c => new
                    {
                        IdProductoPersonalizado = c.Int(nullable: false),
                        IdIngredienteModel = c.Int(nullable: false),
                        IngredienteModel_IngredienteId = c.Int(),
                        ProductoPersonalizado_idOrden = c.Int(),
                        ProductoPersonalizado_idProducto = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdProductoPersonalizado, t.IdIngredienteModel })
                .ForeignKey("dbo.IngredienteModels", t => t.IngredienteModel_IngredienteId)
                .ForeignKey("dbo.ProductoPersonalizadoes", t => new { t.ProductoPersonalizado_idOrden, t.ProductoPersonalizado_idProducto })
                .Index(t => t.IngredienteModel_IngredienteId)
                .Index(t => new { t.ProductoPersonalizado_idOrden, t.ProductoPersonalizado_idProducto });
            
            CreateTable(
                "dbo.IngredienteModels",
                c => new
                    {
                        IngredienteId = c.Int(nullable: false, identity: true),
                        Disponibilidad = c.Single(nullable: false),
                        Tipo = c.String(),
                        PrecioSingular = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredienteId);
            
            CreateTable(
                "dbo.ProductoToIngredientes",
                c => new
                    {
                        ProductoId = c.Int(nullable: false),
                        IngredienteId = c.Int(nullable: false),
                        CantidadProducto = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductoId, t.IngredienteId })
                .ForeignKey("dbo.IngredienteModels", t => t.IngredienteId, cascadeDelete: true)
                .ForeignKey("dbo.ProductoModels", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.ProductoId)
                .Index(t => t.IngredienteId);
            
            CreateTable(
                "dbo.ProductoModels",
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
                        idOrden = c.Int(nullable: false),
                        idProducto = c.Int(nullable: false),
                        OrdenModel_NumOrden = c.Int(),
                        ProductoModel_ProductoId = c.Int(),
                    })
                .PrimaryKey(t => new { t.idOrden, t.idProducto })
                .ForeignKey("dbo.OrdenModels", t => t.OrdenModel_NumOrden)
                .ForeignKey("dbo.ProductoModels", t => t.ProductoModel_ProductoId)
                .Index(t => t.OrdenModel_NumOrden)
                .Index(t => t.ProductoModel_ProductoId);
            
            CreateTable(
                "dbo.OrdenModels",
                c => new
                    {
                        NumOrden = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(nullable: false),
                        FechaOrden = c.DateTime(nullable: false),
                        FechaEntrega = c.DateTime(nullable: false),
                        Monto = c.Int(nullable: false),
                        Pagado = c.Boolean(nullable: false),
                        MetodoPago = c.Int(nullable: false),
                        ListoEntrega = c.Boolean(nullable: false),
                        Entregado = c.Boolean(nullable: false),
                        UsuarioModel_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.NumOrden)
                .ForeignKey("dbo.UsuarioModels", t => t.UsuarioModel_UsuarioId)
                .Index(t => t.UsuarioModel_UsuarioId);
            
            CreateTable(
                "dbo.UsuarioModels",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        NombreUsuario = c.String(),
                        Password = c.String(),
                        Rol = c.Int(nullable: false),
                        Saldo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdenModels", "UsuarioModel_UsuarioId", "dbo.UsuarioModels");
            DropForeignKey("dbo.ProductoToIngredientes", "ProductoId", "dbo.ProductoModels");
            DropForeignKey("dbo.ProductoPersonalizadoes", "ProductoModel_ProductoId", "dbo.ProductoModels");
            DropForeignKey("dbo.ProductoPersonalizadoes", "OrdenModel_NumOrden", "dbo.OrdenModels");
            DropForeignKey("dbo.ConSinIngredientes", new[] { "ProductoPersonalizado_idOrden", "ProductoPersonalizado_idProducto" }, "dbo.ProductoPersonalizadoes");
            DropForeignKey("dbo.ProductoToIngredientes", "IngredienteId", "dbo.IngredienteModels");
            DropForeignKey("dbo.ConSinIngredientes", "IngredienteModel_IngredienteId", "dbo.IngredienteModels");
            DropIndex("dbo.OrdenModels", new[] { "UsuarioModel_UsuarioId" });
            DropIndex("dbo.ProductoPersonalizadoes", new[] { "ProductoModel_ProductoId" });
            DropIndex("dbo.ProductoPersonalizadoes", new[] { "OrdenModel_NumOrden" });
            DropIndex("dbo.ProductoToIngredientes", new[] { "IngredienteId" });
            DropIndex("dbo.ProductoToIngredientes", new[] { "ProductoId" });
            DropIndex("dbo.ConSinIngredientes", new[] { "ProductoPersonalizado_idOrden", "ProductoPersonalizado_idProducto" });
            DropIndex("dbo.ConSinIngredientes", new[] { "IngredienteModel_IngredienteId" });
            DropTable("dbo.UsuarioModels");
            DropTable("dbo.OrdenModels");
            DropTable("dbo.ProductoPersonalizadoes");
            DropTable("dbo.ProductoModels");
            DropTable("dbo.ProductoToIngredientes");
            DropTable("dbo.IngredienteModels");
            DropTable("dbo.ConSinIngredientes");
        }
    }
}
