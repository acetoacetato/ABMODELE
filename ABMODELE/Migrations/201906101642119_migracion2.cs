namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredientes", "Nombre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredientes", "Nombre");
        }
    }
}
