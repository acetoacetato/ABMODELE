namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Destacados : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "Destacado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "Destacado");
        }
    }
}
