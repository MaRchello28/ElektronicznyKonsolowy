using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.TeacherViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.TeachersController
{
    public class ChooseClassController
    {
        MyDbContext db;
        ChooseClassView chooseClassView;
        ChooseCorrectSubjectView chooseCorrectSubjectView;
        public ChooseClassController(MyDbContext db)
        {
            this.db = db; chooseClassView = new ChooseClassView(db);
            chooseCorrectSubjectView = new ChooseCorrectSubjectView(db);
        }
        public void Run(int userId)
        {
            string name;
            int selectedClass = chooseClassView.selectClass();
            List<int> teacherSessions = FindSubjectsForThisTeacher(selectedClass, userId);
            List<string> subjectNames = new List<string>();
            for(int i=0; i< teacherSessions.Count; i++)
            {
                name = FindSubjectName(teacherSessions[i]);
                subjectNames.Add(name);
            }
            int selectedSubject = chooseCorrectSubjectView.Run(subjectNames);
            //Tu będzie się wyświetlać opcja "przejdz do lekcji", wstaw ocenę z przedmiotu
            int choose = chooseCorrectSubjectView.SelectOption();
            switch (choose)
            {
                case 0:
                    {
                        chooseCorrectSubjectView.ManageGrades(selectedClass);
                        break;
                    }
                case 1:
                    {
                        chooseCorrectSubjectView.ManageLessons();
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }
        public List<int> FindSubjectsForThisTeacher(int selectedClass, int userId)
        {
            List<int> teacherSessions = new List<int>();
            foreach(var sesion in db.Sessions)
            {
                if((sesion.teacherId == userId) && (sesion.ClassScheduleId == selectedClass))
                {
                    teacherSessions.Add(sesion.sessionId);
                }
            }
            return teacherSessions;
        }
        public string FindSubjectName(int sessionId)
        {
            var session = db.Sessions.FirstOrDefault(s => s.sessionId == sessionId);
            var subject = db.Subjects.FirstOrDefault(sub => sub.subjectId == session.subjectId);
            return subject?.name;
        }
    }
}
