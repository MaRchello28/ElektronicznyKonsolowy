using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class Subject
    {
        public int subjectId {  get; set; }
        public string name { get; set; }
        public Teacher teacher { get; set; }
        public virtual ICollection<Lesson> lessons { get; set; }
        public Subject(string name, Teacher t) 
        { 
            this.name = name; this.teacher = t;
            this.lessons = new List<Lesson>();
        }
    }
}
