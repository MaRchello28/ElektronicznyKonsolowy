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
                        user_name = c.String(),
                        user_surname = c.String(),
                        user_login = c.String(),
                        user_password = c.String(),
                    })
                .PrimaryKey(t => t.adminId);
            
            CreateTable(
                "dbo.ClassSchedules",
                c => new
                    {
                        classScheduleId = c.Int(nullable: false, identity: true),
                        studentClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.classScheduleId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        sessionId = c.Int(nullable: false, identity: true),
                        subjectId = c.Int(nullable: false),
                        teacherId = c.Int(nullable: false),
                        dayOfTheWeek = c.Int(nullable: false),
                        hourFrom = c.Time(nullable: false, precision: 7),
                        hourTo = c.Time(nullable: false, precision: 7),
                        ClassSchedule_classScheduleId = c.Int(),
                    })
                .PrimaryKey(t => t.sessionId)
                .ForeignKey("dbo.Teachers", t => t.teacherId, cascadeDelete: true)
                .ForeignKey("dbo.ClassSchedules", t => t.ClassSchedule_classScheduleId)
                .Index(t => t.teacherId)
                .Index(t => t.ClassSchedule_classScheduleId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        lessonId = c.Int(nullable: false, identity: true),
                        nuberOfLesson = c.Int(nullable: false),
                        name = c.String(),
                        description = c.String(),
                        Session_sessionId = c.Int(),
                    })
                .PrimaryKey(t => t.lessonId)
                .ForeignKey("dbo.Sessions", t => t.Session_sessionId)
                .Index(t => t.Session_sessionId);
            
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        attendanceId = c.Int(nullable: false, identity: true),
                        present = c.Boolean(nullable: false),
                        studentId = c.Int(nullable: false),
                        lessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.attendanceId)
                .ForeignKey("dbo.Lessons", t => t.lessonId, cascadeDelete: true)
                .Index(t => t.lessonId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        teacherId = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        user_name = c.String(),
                        user_surname = c.String(),
                        user_login = c.String(),
                        user_password = c.String(),
                    })
                .PrimaryKey(t => t.teacherId);
            
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
                "dbo.Mails",
                c => new
                    {
                        mailId = c.Int(nullable: false, identity: true),
                        subject = c.String(),
                        body = c.String(),
                        from = c.String(),
                        to = c.String(),
                        read = c.Boolean(nullable: false),
                        send = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.mailId);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        parentId = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        phoneNumber = c.String(),
                        user_name = c.String(),
                        user_surname = c.String(),
                        user_login = c.String(),
                        user_password = c.String(),
                    })
                .PrimaryKey(t => t.parentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        studentId = c.Int(nullable: false, identity: true),
                        user_name = c.String(),
                        user_surname = c.String(),
                        user_login = c.String(),
                        user_password = c.String(),
                        studentClassId = c.Int(),
                        parentId = c.Int(),
                    })
                .PrimaryKey(t => t.studentId)
                .ForeignKey("dbo.Parents", t => t.parentId)
                .ForeignKey("dbo.StudentClasses", t => t.studentClassId)
                .Index(t => t.studentClassId)
                .Index(t => t.parentId);
            
            CreateTable(
                "dbo.StudentClasses",
                c => new
                    {
                        studentClassId = c.Int(nullable: false, identity: true),
                        number = c.String(maxLength: 1),
                        letter = c.String(maxLength: 1),
                        teacherId = c.Int(nullable: false),
                        classScheduleId = c.Int(),
                    })
                .PrimaryKey(t => t.studentClassId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        subjectId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.subjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "studentClassId", "dbo.StudentClasses");
            DropForeignKey("dbo.Students", "parentId", "dbo.Parents");
            DropForeignKey("dbo.Grades", "studentId", "dbo.Students");
            DropForeignKey("dbo.Sessions", "ClassSchedule_classScheduleId", "dbo.ClassSchedules");
            DropForeignKey("dbo.Sessions", "teacherId", "dbo.Teachers");
            DropForeignKey("dbo.Lessons", "Session_sessionId", "dbo.Sessions");
            DropForeignKey("dbo.Attendances", "lessonId", "dbo.Lessons");
            DropIndex("dbo.Students", new[] { "parentId" });
            DropIndex("dbo.Students", new[] { "studentClassId" });
            DropIndex("dbo.Grades", new[] { "studentId" });
            DropIndex("dbo.Attendances", new[] { "lessonId" });
            DropIndex("dbo.Lessons", new[] { "Session_sessionId" });
            DropIndex("dbo.Sessions", new[] { "ClassSchedule_classScheduleId" });
            DropIndex("dbo.Sessions", new[] { "teacherId" });
            DropTable("dbo.Subjects");
            DropTable("dbo.StudentClasses");
            DropTable("dbo.Students");
            DropTable("dbo.Parents");
            DropTable("dbo.Mails");
            DropTable("dbo.Grades");
            DropTable("dbo.Teachers");
            DropTable("dbo.Attendances");
            DropTable("dbo.Lessons");
            DropTable("dbo.Sessions");
            DropTable("dbo.ClassSchedules");
            DropTable("dbo.Admins");
        }
    }
}
