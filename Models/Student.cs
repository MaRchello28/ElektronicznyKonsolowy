using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class Student
    {
        public int studentId { get; set; }
        public User user { get; set; }
        public virtual ICollection<Grade> grades { get; set; }
        public int studentClassId { get; set; }
        public Student(User user) 
        { 
            this.user = user; grades = new List<Grade>();
        }
    }
}
