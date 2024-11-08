using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class ClassSchedule
    {
        public int classScheduleId {  get; set; }
        public ICollection<Session> sessions { get; set; }
        public int studentClassId { get; set; }
        public ClassSchedule() { }
        public ClassSchedule(int classId)
        {
            this.studentClassId = classId;
            this.sessions = new List<Session>();
        }
        public void AddSession(Session session)
        {
            sessions.Add(session);
        }
        public void RemoveSession(Session session)
        {
            sessions.Remove(session);
        }
    }
}
