namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProdPerYOrden : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SinIngredientes", new[] { "ProductoPersonalizado_IdOrden", "ProductoPersonalizado_IdProducto" }, "dbo.ProductoPersonalizadoes");
            DropIndex("dbo.SinIngredientes", new[] { "ProductoPersonalizado_IdOrden", "ProductoPersonalizado_IdProducto" });
            RenameColumn(table: "dbo.SinIngredientes", name: "ProductoPersonalizado_IdOrden", newName: "ProductoPersonalizado_Id");
            DropPrimaryKey("dbo.ProductoPersonalizadoes");
            AddColumn("dbo.ProductoPersonalizadoes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ProductoPersonalizadoes", "Id");
            CreateIndex("dbo.SinIngredientes", "ProductoPersonalizado_Id");
            AddForeignKey("dbo.SinIngredientes", "ProductoPersonalizado_Id", "dbo.ProductoPersonalizadoes", "Id");
            DropColumn("dbo.SinIngredientes", "ProductoPersonalizado_IdProducto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SinIngredientes", "ProductoPersonalizado_IdProducto", c => c.Int());
            DropForeignKey("dbo.SinIngredientes", "ProductoPersonalizado_Id", "dbo.ProductoPersonalizadoes");
            DropIndex("dbo.SinIngredientes", new[] { "ProductoPersonalizado_Id" });
            DropPrimaryKey("dbo.ProductoPersonalizadoes");
            DropColumn("dbo.ProductoPersonalizadoes", "Id");
            AddPrimaryKey("dbo.ProductoPersonalizadoes", new[] { "IdOrden", "IdProducto" });
            RenameColumn(table: "dbo.SinIngredientes", name: "ProductoPersonalizado_Id", newName: "ProductoPersonalizado_IdOrden");
            CreateIndex("dbo.SinIngredientes", new[] { "ProductoPersonalizado_IdOrden", "ProductoPersonalizado_IdProducto" });
            AddForeignKey("dbo.SinIngredientes", new[] { "ProductoPersonalizado_IdOrden", "ProductoPersonalizado_IdProducto" }, "dbo.ProductoPersonalizadoes", new[] { "IdOrden", "IdProducto" });
        }
    }
}
