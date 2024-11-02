namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowNullInStudent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "parentId", "dbo.Parents");
            DropForeignKey("dbo.Students", "studentClassId", "dbo.StudentClasses");
            DropIndex("dbo.Students", new[] { "studentClassId" });
            DropIndex("dbo.Students", new[] { "parentId" });
            AlterColumn("dbo.Students", "studentClassId", c => c.Int());
            AlterColumn("dbo.Students", "parentId", c => c.Int());
            CreateIndex("dbo.Students", "studentClassId");
            CreateIndex("dbo.Students", "parentId");
            AddForeignKey("dbo.Students", "parentId", "dbo.Parents", "parentId");
            AddForeignKey("dbo.Students", "studentClassId", "dbo.StudentClasses", "studentClassId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "studentClassId", "dbo.StudentClasses");
            DropForeignKey("dbo.Students", "parentId", "dbo.Parents");
            DropIndex("dbo.Students", new[] { "parentId" });
            DropIndex("dbo.Students", new[] { "studentClassId" });
            AlterColumn("dbo.Students", "parentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "studentClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "parentId");
            CreateIndex("dbo.Students", "studentClassId");
            AddForeignKey("dbo.Students", "studentClassId", "dbo.StudentClasses", "studentClassId", cascadeDelete: true);
            AddForeignKey("dbo.Students", "parentId", "dbo.Parents", "parentId", cascadeDelete: true);
        }
    }
}
