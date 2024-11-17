namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSessionIdForLessons : DbMigration
    {
        public override void Up()
        {
            // Usunięcie tabeli Lessons
            DropTable("dbo.Lessons");

            // Utworzenie nowej tabeli Lessons
            CreateTable(
                "dbo.Lessons",
                c => new
                {
                    lessonId = c.Int(nullable: false, identity: true),
                    nuberOfLesson = c.Int(nullable: false),
                    name = c.String(),
                    description = c.String(),
                    date = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    sessionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.lessonId)
                .ForeignKey("dbo.Sessions", t => t.sessionId, cascadeDelete: true)
                .Index(t => t.sessionId);
        }
        public override void Down()
        {
            // Usunięcie nowej tabeli Lessons
            DropForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions");
            DropIndex("dbo.Lessons", new[] { "sessionId" });
            DropTable("dbo.Lessons");

            // Opcjonalnie: Przywrócenie starej wersji tabeli (jeśli potrzebujesz)
        }
    }
}
