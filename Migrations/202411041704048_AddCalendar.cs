namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCalendar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lessons", "subjectId", "dbo.Subjects");
            DropForeignKey("dbo.Attendances", "subject_subjectId", "dbo.Subjects");
            DropIndex("dbo.Lessons", new[] { "subjectId" });
            DropIndex("dbo.Attendances", new[] { "subject_subjectId" });
            DropIndex("dbo.Subjects", new[] { "teacher_teacherId" });
            AddColumn("dbo.Lessons", "sessionId", c => c.Int(nullable: false));
            AddColumn("dbo.Attendances", "studentId", c => c.Int(nullable: false));
            AddColumn("dbo.StudentClasses", "calendarId", c => c.Int());
            CreateIndex("dbo.Subjects", "Teacher_teacherId");
            DropColumn("dbo.Lessons", "subjectId");
            DropColumn("dbo.Attendances", "user_name");
            DropColumn("dbo.Attendances", "user_surname");
            DropColumn("dbo.Attendances", "user_login");
            DropColumn("dbo.Attendances", "user_password");
            DropColumn("dbo.Attendances", "subject_subjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendances", "subject_subjectId", c => c.Int());
            AddColumn("dbo.Attendances", "user_password", c => c.String());
            AddColumn("dbo.Attendances", "user_login", c => c.String());
            AddColumn("dbo.Attendances", "user_surname", c => c.String());
            AddColumn("dbo.Attendances", "user_name", c => c.String());
            AddColumn("dbo.Lessons", "subjectId", c => c.Int(nullable: false));
            DropIndex("dbo.Subjects", new[] { "Teacher_teacherId" });
            DropColumn("dbo.StudentClasses", "calendarId");
            DropColumn("dbo.Attendances", "studentId");
            DropColumn("dbo.Lessons", "sessionId");
            CreateIndex("dbo.Subjects", "teacher_teacherId");
            CreateIndex("dbo.Attendances", "subject_subjectId");
            CreateIndex("dbo.Lessons", "subjectId");
            AddForeignKey("dbo.Attendances", "subject_subjectId", "dbo.Subjects", "subjectId");
            AddForeignKey("dbo.Lessons", "subjectId", "dbo.Subjects", "subjectId", cascadeDelete: true);
        }
    }
}
