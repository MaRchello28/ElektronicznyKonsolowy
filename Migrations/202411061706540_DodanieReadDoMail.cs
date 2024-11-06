namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanieReadDoMail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "read", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mails", "read");
        }
    }
}
