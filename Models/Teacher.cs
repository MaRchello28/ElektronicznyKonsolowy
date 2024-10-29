using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class Teacher
    {
        public int teacherId {  get; set; }
        public string email { get; set; }
        public User user { get; set; }
        public virtual ICollection<Subject> subjects { get; set; }
        public Teacher(string email, User user)
        {
            this.email = email; this.user = user;
        }
    }
}
