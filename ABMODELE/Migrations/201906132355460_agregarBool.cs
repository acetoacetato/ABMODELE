namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SinIngredientes", "Sin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SinIngredientes", "Sin");
        }
    }
}
