using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class StudentClass
    {
        public int studentClassId {  get; set; }
        public char number {  get; set; }
        public char letter { get; set; }
        public ICollection <Student> students { get; set; }
        public int teacherId { get; set; }
        public StudentClass(char number, char letter, int teacherId) 
        { 
            students = new List<Student>(); this.number = number; this.letter = letter; this.teacherId = teacherId;
        }
    }
}
