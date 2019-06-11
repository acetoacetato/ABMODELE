namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuitaFechaEnProducto : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Productoes", "FechaPreparacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Productoes", "FechaPreparacion", c => c.DateTime(nullable: false));
        }
    }
}
