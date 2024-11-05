using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    [ComplexType]
    public class User
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public ICollection<Mail> messages { get; set; } = new List<Mail>();
        public User() { }
        public User(string name, string surname, string login, string password)
        {
            this.name = name; this.surname=surname; this.login = login; this.password = password;
        }
    }
}
