namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poprawasesji : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sessions", "ClassSchedule_classScheduleId", "dbo.ClassSchedules");
            DropIndex("dbo.Sessions", new[] { "ClassSchedule_classScheduleId" });
            RenameColumn(table: "dbo.Sessions", name: "ClassSchedule_classScheduleId", newName: "ClassScheduleId");
            AlterColumn("dbo.Sessions", "ClassScheduleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sessions", "ClassScheduleId");
            AddForeignKey("dbo.Sessions", "ClassScheduleId", "dbo.ClassSchedules", "classScheduleId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "ClassScheduleId", "dbo.ClassSchedules");
            DropIndex("dbo.Sessions", new[] { "ClassScheduleId" });
            AlterColumn("dbo.Sessions", "ClassScheduleId", c => c.Int());
            RenameColumn(table: "dbo.Sessions", name: "ClassScheduleId", newName: "ClassSchedule_classScheduleId");
            CreateIndex("dbo.Sessions", "ClassSchedule_classScheduleId");
            AddForeignKey("dbo.Sessions", "ClassSchedule_classScheduleId", "dbo.ClassSchedules", "classScheduleId");
        }
    }
}
