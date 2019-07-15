namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComentariosOrden : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ordens", "Comentario", c => c.String());
            DropColumn("dbo.ProductoPersonalizadoes", "Comentario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductoPersonalizadoes", "Comentario", c => c.String());
            DropColumn("dbo.Ordens", "Comentario");
        }
    }
}
