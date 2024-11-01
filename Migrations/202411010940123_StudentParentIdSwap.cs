namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentParentIdSwap : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Parent_parentId", "dbo.Parents");
            DropIndex("dbo.Students", new[] { "Parent_parentId" });
            RenameColumn(table: "dbo.Students", name: "Parent_parentId", newName: "parentId");
            AlterColumn("dbo.Students", "parentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "parentId");
            AddForeignKey("dbo.Students", "parentId", "dbo.Parents", "parentId", cascadeDelete: true);
            DropColumn("dbo.Students", "studentParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "studentParentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Students", "parentId", "dbo.Parents");
            DropIndex("dbo.Students", new[] { "parentId" });
            AlterColumn("dbo.Students", "parentId", c => c.Int());
            RenameColumn(table: "dbo.Students", name: "parentId", newName: "Parent_parentId");
            CreateIndex("dbo.Students", "Parent_parentId");
            AddForeignKey("dbo.Students", "Parent_parentId", "dbo.Parents", "parentId");
        }
    }
}
