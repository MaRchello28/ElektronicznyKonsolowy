namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanieIdRodzicaDoUcznia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "studentParentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "studentParentId");
        }
    }
}
