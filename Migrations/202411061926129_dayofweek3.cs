namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dayofweek3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "dayOfTheWeek", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "dayOfTheWeek");
        }
    }
}
