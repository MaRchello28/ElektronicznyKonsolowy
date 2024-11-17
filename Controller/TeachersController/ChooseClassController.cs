using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.TeacherViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Spectre.Console;
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
        ManageLessonsView manageLessonsView;
        ShowAttendanceWholeClassView showAttendanceWholeClassView;
        public ChooseClassController(MyDbContext db)
        {
            this.db = db; chooseClassView = new ChooseClassView(db);
            chooseCorrectSubjectView = new ChooseCorrectSubjectView(db);
            manageLessonsView = new ManageLessonsView(db);
            showAttendanceWholeClassView = new ShowAttendanceWholeClassView(db);
        }
        public void Run(int userId)
        {
            string name;
            int selectedClass = chooseClassView.selectClass();//zwraca idKlasy
            List<int> teacherSessions = FindSubjectsForThisTeacher(selectedClass, userId);
            List<string> subjectNames = new List<string>();
            for(int i=0; i< teacherSessions.Count; i++)
            {
                name = FindSubjectName(teacherSessions[i]);
                subjectNames.Add(name);
            }
            int selectedSubject = chooseCorrectSubjectView.Run(subjectNames);
            int selectedSession = teacherSessions[selectedSubject];
            int choose = chooseCorrectSubjectView.SelectOption();
            int idSelectedSubject = -1;
            foreach (var session in db.Sessions)
            {
                if(session.sessionId == selectedSession)
                {
                    idSelectedSubject = session.subjectId;
                }
            }
            switch (choose)
            {
                case 0://Zarzadzaj ocenami
                    {
                        var descriptionDates = new Dictionary<string, DateTime>();
                        var studentClass = db.StudentClasses
                        .Include(sc => sc.students)
                        .FirstOrDefault(sc => sc.studentClassId == selectedClass);
                        descriptionDates = GetDescriptionDates(studentClass);
                        choose = chooseCorrectSubjectView.ManageGrades(selectedClass, userId, selectedSession);
                        if (studentClass != null)
                        {
                            studentClass.students = studentClass.students.OrderBy(s => s.surname).ToList();
                        }
                        if (studentClass == null || studentClass.students == null || !studentClass.students.Any())
                        {
                            Console.WriteLine("Brak danych dla wybranej klasy.");
                            return;
                        }
                        switch (choose)
                        {
                            case 0: //Wyswietl oceny
                                {
                                    foreach (var student in studentClass.students)
                                    {
                                        db.Entry(student)
                                            .Collection(s => s.grades)
                                            .Load();
                                    }
                                    descriptionDates = GetDescriptionDates(studentClass);
                                    var sortedDescriptions = descriptionDates
                                        .OrderBy(d => d.Value)
                                        .Select(d => d.Key)
                                        .ToList();
                                    var table = new Table();
                                    table.AddColumn("IdUcznia");
                                    table.AddColumn("Nazwisko i Imię");

                                    int number = 1;
                                    foreach (var description in sortedDescriptions)
                                    {
                                        table.AddColumn(number++.ToString());
                                    }

                                    foreach (var student in studentClass.students)
                                    {
                                        var fullName = $"{student.surname} {student.name}";
                                        var row = new List<string> { student.studentId.ToString(), fullName };

                                        foreach (var description in sortedDescriptions)
                                        {
                                            // Filtrujemy oceny dla danej sesji
                                            var grade = student.grades
                                                .FirstOrDefault(g => g.description == description && g.sessionId == selectedSession);

                                            row.Add(grade != null ? grade.value.ToString() : "");
                                        }

                                        table.AddRow(row.ToArray());
                                    }

                                    chooseCorrectSubjectView.ShowGrades(table);
                                    break;
                                }
                            case 1://Wstaw nową
                                {
                                    studentClass = db.StudentClasses.FirstOrDefault(clas => clas.studentClassId == selectedClass);
                                    if (studentClass == null)
                                    {
                                        SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono klasy o podanym ID.");
                                        return;
                                    }
                                    List<string> studentsForNewGrades = chooseCorrectSubjectView.ChooseStudentsForNewGrades(studentClass);
                                    chooseCorrectSubjectView.AddNewGrade(studentsForNewGrades, userId, selectedSession, descriptionDates, idSelectedSubject);
                                    break;
                                }
                            case 2://Edytuj dostępne
                                {
                                    List<int> subjectsId = db.Sessions.Select(s => s.subjectId).ToList();
                                    bool was = false;
                                    var tableSelectedDescription = new Table();
                                    tableSelectedDescription.AddColumn("Nazwisko i imię");
                                    tableSelectedDescription.AddColumn("");
                                    choose = chooseCorrectSubjectView.EditGrade(descriptionDates);
                                    string description = descriptionDates.ElementAt(choose).Key.ToString();
                                    //Wyświetl uczniów w tabelce i ich ocenę za ten test
                                    List<Student> students = db.Students.Where(s => s.studentClassId == selectedClass)
                                        .OrderBy(s => s.user.surname).ToList();
                                    foreach(var s in students)
                                    {
                                        for(int i=0; i<subjectsId.Count; i++)
                                        {
                                            if (s.grades.FirstOrDefault(g => g.sessionId == selectedSession && g.subjectId == subjectsId[i] && g.description == description) 
                                                != null)
                                            {
                                                var grade = s.grades.FirstOrDefault(g => g.sessionId == selectedSession && g.subjectId == subjectsId[i] && g.description == description);
                                                tableSelectedDescription = chooseCorrectSubjectView.AddRowWithGrade(tableSelectedDescription, s, grade);
                                                was = true;
                                                break;
                                            }
                                        }
                                        if(was == false)
                                        {
                                            tableSelectedDescription = chooseCorrectSubjectView.AddRowWithoutGrade(tableSelectedDescription, s);
                                        }
                                        was = false;
                                    }
                                    AnsiConsole.Render(tableSelectedDescription);
                                    List<string> studentsForNewGrades = chooseCorrectSubjectView.SelectStudentsToEditGrade(students, description, selectedSession, subjectsId);
                                    chooseCorrectSubjectView.EditGradesSelectedStudents(studentsForNewGrades, selectedSession, description, subjectsId, userId);

                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                
                case 1://Zarzadzaj obecnoscia
                    {
                        choose = chooseCorrectSubjectView.ManageLessons();
                        switch (choose)
                        {
                            case 0://tworzy lekcje
                                {
                                    name = manageLessonsView.GetLessonName();
                                    string desc = manageLessonsView.AddLessonDescription();
                                    int number = manageLessonsView.GenerateLessonNumber(selectedSession);
                                    Lesson lesson = new Lesson(name, desc, number, selectedSession);
                                    db.Lessons.Add(lesson);
                                    db.SaveChanges();
                                    var getLesson = db.Lessons.FirstOrDefault(l => l.nuberOfLesson == lesson.nuberOfLesson && l.sessionId == lesson.sessionId);
                                    if(manageLessonsView.CheckAttendenceNow())
                                    {
                                        manageLessonsView.CheckAttendence(selectedClass, getLesson);
                                    }
                                    break;
                                }
                            case 1://wybiera istniejaca lekcje
                                {
                                    int lessonNumber = manageLessonsView.ShowExistingLessons(selectedSession);
                                    manageLessonsView.ShowAttendanceOfThisLesson(lessonNumber, selectedSession);
                                    manageLessonsView.EditAttendance(lessonNumber, selectedSession);
                                    break;
                                }
                            case 2://wyświetl wszystkie obecności z lekcji
                                {
                                    showAttendanceWholeClassView.Run(selectedSession);
                                    break;
                                }
                            case 3:
                                {
                                    break;
                                }
                        }
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
        public Dictionary<string, DateTime> GetDescriptionDates(StudentClass studentClass)
        {
            var descriptionDates = new Dictionary<string, DateTime>();

            foreach (var student in studentClass.students)
            {
                foreach (var grade in student.grades)
                {
                    if (grade != null)
                    {
                        if (!descriptionDates.ContainsKey(grade.description) || grade.time < descriptionDates[grade.description])
                        {
                            descriptionDates[grade.description] = grade.time;
                        }
                    }
                }
            }
            return descriptionDates;
        }
    }
}
