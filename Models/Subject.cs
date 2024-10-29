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
        public virtual ICollection<Teacher> teachers { get; set; }
        public virtual ICollection<Subject> subjects { get; set; }
        public Subject(string name) 
        { 
            this.name = name; teachers = new List<Teacher>();
        }
    }
}
