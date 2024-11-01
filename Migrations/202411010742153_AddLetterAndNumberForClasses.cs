namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLetterAndNumberForClasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentClasses", "number", c => c.String(maxLength: 1));
            AddColumn("dbo.StudentClasses", "letter", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentClasses", "letter");
            DropColumn("dbo.StudentClasses", "number");
        }
    }
}
