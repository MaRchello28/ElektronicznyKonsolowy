namespace ElektronicznyKonsolowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nslpINstp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "user_name", c => c.String());
            AddColumn("dbo.Admins", "user_surname", c => c.String());
            AddColumn("dbo.Admins", "user_login", c => c.String());
            AddColumn("dbo.Admins", "user_password", c => c.String());
            AddColumn("dbo.Parents", "user_name", c => c.String());
            AddColumn("dbo.Parents", "user_surname", c => c.String());
            AddColumn("dbo.Parents", "user_login", c => c.String());
            AddColumn("dbo.Parents", "user_password", c => c.String());
            AddColumn("dbo.Students", "user_name", c => c.String());
            AddColumn("dbo.Students", "user_surname", c => c.String());
            AddColumn("dbo.Students", "user_login", c => c.String());
            AddColumn("dbo.Students", "user_password", c => c.String());
            AddColumn("dbo.Teachers", "user_name", c => c.String());
            AddColumn("dbo.Teachers", "user_surname", c => c.String());
            AddColumn("dbo.Teachers", "user_login", c => c.String());
            AddColumn("dbo.Teachers", "user_password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "user_password");
            DropColumn("dbo.Teachers", "user_login");
            DropColumn("dbo.Teachers", "user_surname");
            DropColumn("dbo.Teachers", "user_name");
            DropColumn("dbo.Students", "user_password");
            DropColumn("dbo.Students", "user_login");
            DropColumn("dbo.Students", "user_surname");
            DropColumn("dbo.Students", "user_name");
            DropColumn("dbo.Parents", "user_password");
            DropColumn("dbo.Parents", "user_login");
            DropColumn("dbo.Parents", "user_surname");
            DropColumn("dbo.Parents", "user_name");
            DropColumn("dbo.Admins", "user_password");
            DropColumn("dbo.Admins", "user_login");
            DropColumn("dbo.Admins", "user_surname");
            DropColumn("dbo.Admins", "user_name");
        }
    }
}
