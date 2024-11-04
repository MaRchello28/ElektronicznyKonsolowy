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
        public DateTime dayOfTheWeek { get; set; }
        public TimeSpan hourFrom {  get; set; }
        public TimeSpan hourTo { get; set; }
        public ICollection<Lesson> lessons { get; set; }
        public Teacher? replacement { get; set; }

        public Session(int subjectId, int teacherId, DateTime dayOfTheWeek, TimeSpan hourFrom, TimeSpan hourTo, Teacher? replacement) 
        { 
            this.subjectId = subjectId; this.teacherId = teacherId; this.dayOfTheWeek = dayOfTheWeek; 
            this.hourFrom = hourFrom; this.hourTo = hourTo; this.replacement = replacement; lessons = new List<Lesson>();
        }
    }
}
