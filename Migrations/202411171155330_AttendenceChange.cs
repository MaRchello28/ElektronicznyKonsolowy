namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendenceChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "isPresent", c => c.String());
            DropColumn("dbo.Attendances", "present");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendances", "present", c => c.Boolean(nullable: false));
            DropColumn("dbo.Attendances", "isPresent");
        }
    }
}
