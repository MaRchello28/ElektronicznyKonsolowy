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
        public User user { get; set; }
        public Subject subject { get; set; }
        public int lessonId { get; set; }
    }
}
