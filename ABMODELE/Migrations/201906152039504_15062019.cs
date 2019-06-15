namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15062019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoProductoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Ingredientes", "EsAuxiliar", c => c.Boolean(nullable: false));
            AddColumn("dbo.Productoes", "Tipo", c => c.Int(nullable: false));
            CreateIndex("dbo.Productoes", "Tipo");
            AddForeignKey("dbo.Productoes", "Tipo", "dbo.TipoProductoes", "Id", cascadeDelete: true);
            DropColumn("dbo.Ingredientes", "Tipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredientes", "Tipo", c => c.String());
            DropForeignKey("dbo.Productoes", "Tipo", "dbo.TipoProductoes");
            DropIndex("dbo.Productoes", new[] { "Tipo" });
            DropColumn("dbo.Productoes", "Tipo");
            DropColumn("dbo.Ingredientes", "EsAuxiliar");
            DropTable("dbo.TipoProductoes");
        }
    }
}
