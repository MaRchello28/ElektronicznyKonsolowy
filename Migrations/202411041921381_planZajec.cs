namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class planZajec : DbMigration
    {
        public override void Up()
        {
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
                        dayOfTheWeek = c.DateTime(nullable: false),
                        hourFrom = c.Time(nullable: false, precision: 7),
                        hourTo = c.Time(nullable: false, precision: 7),
                        ClassSchedule_classScheduleId = c.Int(),
                    })
                .PrimaryKey(t => t.sessionId)
                .ForeignKey("dbo.Teachers", t => t.teacherId, cascadeDelete: true)
                .ForeignKey("dbo.ClassSchedules", t => t.ClassSchedule_classScheduleId)
                .Index(t => t.teacherId)
                .Index(t => t.ClassSchedule_classScheduleId);
            
            CreateIndex("dbo.Lessons", "sessionId");
            AddForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions", "sessionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "ClassSchedule_classScheduleId", "dbo.ClassSchedules");
            DropForeignKey("dbo.Sessions", "teacherId", "dbo.Teachers");
            DropForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions");
            DropIndex("dbo.Lessons", new[] { "sessionId" });
            DropIndex("dbo.Sessions", new[] { "ClassSchedule_classScheduleId" });
            DropIndex("dbo.Sessions", new[] { "teacherId" });
            DropTable("dbo.Sessions");
            DropTable("dbo.ClassSchedules");
        }
    }
}
