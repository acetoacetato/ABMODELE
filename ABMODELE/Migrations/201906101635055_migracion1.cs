namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredientes",
                c => new
                    {
                        IngredienteId = c.Int(nullable: false, identity: true),
                        Disponibilidad = c.Single(nullable: false),
                        Tipo = c.String(),
                        PrecioSingular = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredienteId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ingredientes");
        }
    }
}
