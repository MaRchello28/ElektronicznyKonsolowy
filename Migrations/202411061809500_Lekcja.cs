namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lekcja : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions");
            DropIndex("dbo.Lessons", new[] { "sessionId" });
            RenameColumn(table: "dbo.Lessons", name: "sessionId", newName: "Session_sessionId");
            AddColumn("dbo.StudentClasses", "classScheduleId", c => c.Int());
            AlterColumn("dbo.Lessons", "Session_sessionId", c => c.Int());
            CreateIndex("dbo.Lessons", "Session_sessionId");
            AddForeignKey("dbo.Lessons", "Session_sessionId", "dbo.Sessions", "sessionId");
            DropColumn("dbo.StudentClasses", "calendarId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentClasses", "calendarId", c => c.Int());
            DropForeignKey("dbo.Lessons", "Session_sessionId", "dbo.Sessions");
            DropIndex("dbo.Lessons", new[] { "Session_sessionId" });
            AlterColumn("dbo.Lessons", "Session_sessionId", c => c.Int(nullable: false));
            DropColumn("dbo.StudentClasses", "classScheduleId");
            RenameColumn(table: "dbo.Lessons", name: "Session_sessionId", newName: "sessionId");
            CreateIndex("dbo.Lessons", "sessionId");
            AddForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions", "sessionId", cascadeDelete: true);
        }
    }
}
