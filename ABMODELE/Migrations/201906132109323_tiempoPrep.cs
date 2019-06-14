namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tiempoPrep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "tiempoPreparacion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "tiempoPreparacion");
        }
    }
}
