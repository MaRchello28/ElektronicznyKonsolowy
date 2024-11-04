using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class Calendar
    {
        public int calendarId {  get; set; }
        public ICollection<Session> sessions { get; set; }
        public int studentClassId { get; set; }
    }
}
