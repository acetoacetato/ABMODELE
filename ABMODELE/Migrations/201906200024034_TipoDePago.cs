namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoDePago : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MetodoDePagoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Ordens", "MetodoPago");
            AddForeignKey("dbo.Ordens", "MetodoPago", "dbo.MetodoDePagoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ordens", "MetodoPago", "dbo.MetodoDePagoes");
            DropIndex("dbo.Ordens", new[] { "MetodoPago" });
            DropTable("dbo.MetodoDePagoes");
        }
    }
}
