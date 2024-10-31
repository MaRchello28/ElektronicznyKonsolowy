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
        public int nuberOfLesson { get; set; } = 1;
        public string name { get; set; }
        public string description { get; set; }
        public int subjectId { get; set; }
        public ICollection<Attendance> attendances { get; set; }
        public Lesson(string name, string description, int subjectId) 
        { 
            this.name = name; this.description = description; this.subjectId=subjectId;
            attendances = new List<Attendance>(); this.nuberOfLesson++;
        }
    }
}
