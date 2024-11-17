namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDuplicateSessionId : DbMigration
    {
        public override void Up()
        {
            // Usuń klucz obcy dla nadmiarowej kolumny Session_sessionId (jeśli istnieje)
            DropForeignKey("dbo.Lessons", "Session_sessionId", "dbo.Sessions");

            // Usuń indeks powiązany z Session_sessionId
            DropIndex("dbo.Lessons", new[] { "Session_sessionId" });

            // Usuń nadmiarową kolumnę Session_sessionId
            DropColumn("dbo.Lessons", "Session_sessionId");

            // Usuń istniejący indeks dla sessionId, jeśli istnieje
            DropIndex("dbo.Lessons", new[] { "sessionId" });

            // Opcjonalnie: Usuń istniejący klucz obcy dla sessionId, aby go odbudować
            DropForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions");

            // Dodaj poprawny klucz obcy dla sessionId
            AddForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions", "sessionId", cascadeDelete: true);

            // Dodaj indeks dla sessionId
            CreateIndex("dbo.Lessons", "sessionId");
        }


        public override void Down()
        {
            // Przywrócenie kolumny Session_sessionId
            AddColumn("dbo.Lessons", "Session_sessionId", c => c.Int());

            // Przywrócenie relacji dla Session_sessionId
            CreateIndex("dbo.Lessons", "Session_sessionId");
            AddForeignKey("dbo.Lessons", "Session_sessionId", "dbo.Sessions", "sessionId");

            // Przywrócenie relacji dla sessionId
            DropIndex("dbo.Lessons", new[] { "sessionId" });
            DropForeignKey("dbo.Lessons", "sessionId", "dbo.Sessions");
        }

    }
}
