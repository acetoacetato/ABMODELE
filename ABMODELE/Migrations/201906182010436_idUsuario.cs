namespace ABMODELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idUsuario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ordens", "IdUsuario", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ordens", "IdUsuario", c => c.Int(nullable: false));
        }
    }
}
