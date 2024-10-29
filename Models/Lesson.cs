using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class Lesson
    {
        public int lessonId {  get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int subjectId { get; set; }
        public Lesson(string name, string description, int subjectId) 
        { 
            this.name = name; this.description = description; this.subjectId=subjectId;
        }
    }
}
