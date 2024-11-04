using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class Attendance
    {
        public int attendanceId {  get; set; }
        public bool present {  get; set; }
        public int studentId { get; set; }
        public int lessonId { get; set; }
        public Attendance(bool present, int studentId, int lessonId) { 
            this.present = present; this.studentId = studentId; this.lessonId = lessonId;
        }
    }
}
