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
        public int nuberOfLesson { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<Attendance> attendances { get; set; }
        public Lesson(string name, string description, int numberOfLesson) 
        { 
            this.name = name; this.description = description;
            attendances = new List<Attendance>(); this.nuberOfLesson=numberOfLesson;
        }
    }
}
