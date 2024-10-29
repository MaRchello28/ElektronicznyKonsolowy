using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext():base("Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=D:\\Programy\\Studia\\Semestr 5\\ElektronicznyKonsolowy\\ElektronicznyKonsolowy\\AppData\\Database1.mdf;" +
            "Integrated Security=True;Connect Timeout=30")
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
