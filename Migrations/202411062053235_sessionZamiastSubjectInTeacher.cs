namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sessionZamiastSubjectInTeacher : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "Teacher_teacherId", "dbo.Teachers");
            DropIndex("dbo.Subjects", new[] { "Teacher_teacherId" });
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
                    })
                .PrimaryKey(t => t.sessionId)
                .ForeignKey("dbo.Teachers", t => t.teacherId, cascadeDelete: true)
                .Index(t => t.teacherId);
            
            CreateIndex("dbo.Lessons", "sessionId");
            AddForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions", "sessionId", cascadeDelete: true);
            DropColumn("dbo.Subjects", "Teacher_teacherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "Teacher_teacherId", c => c.Int());
            DropForeignKey("dbo.Sessions", "teacherId", "dbo.Teachers");
            DropForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions");
            DropIndex("dbo.Sessions", new[] { "teacherId" });
            DropIndex("dbo.Lessons", new[] { "sessionId" });
            DropTable("dbo.Sessions");
            CreateIndex("dbo.Subjects", "Teacher_teacherId");
            AddForeignKey("dbo.Subjects", "Teacher_teacherId", "dbo.Teachers", "teacherId");
        }
    }
}
