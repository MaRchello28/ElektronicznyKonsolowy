namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubjectIdToGrades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "subjectId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grades", "subjectId");
        }
    }
}
