using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class Session
    {
        public int sessionId { get; set; }
        public int subjectId { get; set; }
        public int teacherId { get; set; }
        public DayOfWeek dayOfTheWeek { get; set; }
        public TimeSpan hourFrom {  get; set; }
        public TimeSpan hourTo { get; set; }
        public ICollection<Lesson> lessons { get; set; }
        public int ClassScheduleId { get; set; }
        public Teacher? replacement { get; set; }
        public Session() { }
        public Session(int subjectId, int teacherId, DayOfWeek dayOfTheWeek, TimeSpan hourFrom, TimeSpan hourTo, Teacher? replacement) 
        { 
            this.subjectId = subjectId; this.teacherId = teacherId; this.dayOfTheWeek = dayOfTheWeek; 
            this.hourFrom = hourFrom; this.hourTo = hourTo; this.replacement = replacement; lessons = new List<Lesson>();
        }
        public Session(int subjectId, int teacherId, DayOfWeek dayOfTheWeek, TimeSpan hourFrom, TimeSpan hourTo)
        {
            this.subjectId = subjectId; this.teacherId = teacherId; this.dayOfTheWeek = dayOfTheWeek;
            this.hourFrom = hourFrom; this.hourTo = hourTo; lessons = new List<Lesson>();
        }
    }
}
