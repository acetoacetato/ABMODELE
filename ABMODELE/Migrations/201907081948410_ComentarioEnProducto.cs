namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComentarioEnProducto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductoPersonalizadoes", "Comentario", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductoPersonalizadoes", "Comentario");
        }
    }
}
