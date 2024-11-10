namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subjectOnSession : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "sessionId", c => c.Int(nullable: false));
            DropColumn("dbo.Grades", "subjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Grades", "subjectId", c => c.Int(nullable: false));
            DropColumn("dbo.Grades", "sessionId");
        }
    }
}
