namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attendance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "Subject_subjectId", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSubjects", "Teacher_teacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherSubjects", "Subject_subjectId", "dbo.Subjects");
            DropIndex("dbo.Subjects", new[] { "Subject_subjectId" });
            DropIndex("dbo.TeacherSubjects", new[] { "Teacher_teacherId" });
            DropIndex("dbo.TeacherSubjects", new[] { "Subject_subjectId" });
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        attendanceId = c.Int(nullable: false, identity: true),
                        present = c.Boolean(nullable: false),
                        user_name = c.String(),
                        user_surname = c.String(),
                        user_login = c.String(),
                        user_password = c.String(),
                        lessonId = c.Int(nullable: false),
                        subject_subjectId = c.Int(),
                    })
                .PrimaryKey(t => t.attendanceId)
                .ForeignKey("dbo.Subjects", t => t.subject_subjectId)
                .ForeignKey("dbo.Lessons", t => t.lessonId, cascadeDelete: true)
                .Index(t => t.lessonId)
                .Index(t => t.subject_subjectId);
            
            AddColumn("dbo.Lessons", "nuberOfLesson", c => c.Int(nullable: false));
            AddColumn("dbo.Subjects", "teacher_teacherId", c => c.Int());
            CreateIndex("dbo.Lessons", "subjectId");
            CreateIndex("dbo.Subjects", "teacher_teacherId");
            AddForeignKey("dbo.Lessons", "subjectId", "dbo.Subjects", "subjectId", cascadeDelete: true);
            AddForeignKey("dbo.Subjects", "teacher_teacherId", "dbo.Teachers", "teacherId");
            DropColumn("dbo.Subjects", "Subject_subjectId");
            DropTable("dbo.TeacherSubjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        Teacher_teacherId = c.Int(nullable: false),
                        Subject_subjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_teacherId, t.Subject_subjectId });
            
            AddColumn("dbo.Subjects", "Subject_subjectId", c => c.Int());
            DropForeignKey("dbo.Attendances", "lessonId", "dbo.Lessons");
            DropForeignKey("dbo.Attendances", "subject_subjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "teacher_teacherId", "dbo.Teachers");
            DropForeignKey("dbo.Lessons", "subjectId", "dbo.Subjects");
            DropIndex("dbo.Subjects", new[] { "teacher_teacherId" });
            DropIndex("dbo.Attendances", new[] { "subject_subjectId" });
            DropIndex("dbo.Attendances", new[] { "lessonId" });
            DropIndex("dbo.Lessons", new[] { "subjectId" });
            DropColumn("dbo.Subjects", "teacher_teacherId");
            DropColumn("dbo.Lessons", "nuberOfLesson");
            DropTable("dbo.Attendances");
            CreateIndex("dbo.TeacherSubjects", "Subject_subjectId");
            CreateIndex("dbo.TeacherSubjects", "Teacher_teacherId");
            CreateIndex("dbo.Subjects", "Subject_subjectId");
            AddForeignKey("dbo.TeacherSubjects", "Subject_subjectId", "dbo.Subjects", "subjectId", cascadeDelete: true);
            AddForeignKey("dbo.TeacherSubjects", "Teacher_teacherId", "dbo.Teachers", "teacherId", cascadeDelete: true);
            AddForeignKey("dbo.Subjects", "Subject_subjectId", "dbo.Subjects", "subjectId");
        }
    }
}
