namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        adminId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.adminId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        gradeId = c.Int(nullable: false, identity: true),
                        value = c.Double(nullable: false),
                        wage = c.Int(nullable: false),
                        description = c.String(),
                        studentId = c.Int(nullable: false),
                        teacherId = c.Int(nullable: false),
                        time = c.DateTime(nullable: false),
                        subjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.gradeId)
                .ForeignKey("dbo.Students", t => t.studentId, cascadeDelete: true)
                .Index(t => t.studentId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        lessonId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        subjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.lessonId);
            
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        mailId = c.Int(nullable: false, identity: true),
                        subject = c.String(),
                        body = c.String(),
                        from = c.String(),
                        to = c.String(),
                    })
                .PrimaryKey(t => t.mailId);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        parentId = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        phoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.parentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        studentId = c.Int(nullable: false, identity: true),
                        studentClassId = c.Int(nullable: false),
                        Parent_parentId = c.Int(),
                    })
                .PrimaryKey(t => t.studentId)
                .ForeignKey("dbo.Parents", t => t.Parent_parentId)
                .ForeignKey("dbo.StudentClasses", t => t.studentClassId, cascadeDelete: true)
                .Index(t => t.studentClassId)
                .Index(t => t.Parent_parentId);
            
            CreateTable(
                "dbo.StudentClasses",
                c => new
                    {
                        studentClassId = c.Int(nullable: false, identity: true),
                        teacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.studentClassId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        subjectId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        Subject_subjectId = c.Int(),
                    })
                .PrimaryKey(t => t.subjectId)
                .ForeignKey("dbo.Subjects", t => t.Subject_subjectId)
                .Index(t => t.Subject_subjectId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        teacherId = c.Int(nullable: false, identity: true),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.teacherId);
            
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        Teacher_teacherId = c.Int(nullable: false),
                        Subject_subjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_teacherId, t.Subject_subjectId })
                .ForeignKey("dbo.Teachers", t => t.Teacher_teacherId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_subjectId, cascadeDelete: true)
                .Index(t => t.Teacher_teacherId)
                .Index(t => t.Subject_subjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherSubjects", "Subject_subjectId", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSubjects", "Teacher_teacherId", "dbo.Teachers");
            DropForeignKey("dbo.Subjects", "Subject_subjectId", "dbo.Subjects");
            DropForeignKey("dbo.Students", "studentClassId", "dbo.StudentClasses");
            DropForeignKey("dbo.Students", "Parent_parentId", "dbo.Parents");
            DropForeignKey("dbo.Grades", "studentId", "dbo.Students");
            DropIndex("dbo.TeacherSubjects", new[] { "Subject_subjectId" });
            DropIndex("dbo.TeacherSubjects", new[] { "Teacher_teacherId" });
            DropIndex("dbo.Subjects", new[] { "Subject_subjectId" });
            DropIndex("dbo.Students", new[] { "Parent_parentId" });
            DropIndex("dbo.Students", new[] { "studentClassId" });
            DropIndex("dbo.Grades", new[] { "studentId" });
            DropTable("dbo.TeacherSubjects");
            DropTable("dbo.Teachers");
            DropTable("dbo.Subjects");
            DropTable("dbo.StudentClasses");
            DropTable("dbo.Students");
            DropTable("dbo.Parents");
            DropTable("dbo.Mails");
            DropTable("dbo.Lessons");
            DropTable("dbo.Grades");
            DropTable("dbo.Admins");
        }
    }
}
