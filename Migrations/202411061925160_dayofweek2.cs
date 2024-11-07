namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dayofweek2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sessions", "dayOfTheWeek");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sessions", "dayOfTheWeek", c => c.DateTime(nullable: false));
        }
    }
}
