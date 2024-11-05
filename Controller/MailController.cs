using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller
{
    public class MailController
    {
        MyDbContext db;
        SendMailView sendMailView;
        ShowMailView showMailView;
        DeleteMailView deleteMailView;

        public MailController(MyDbContext db)
        {
            this.db = db;
            sendMailView = new SendMailView(db);
        }
        public void ChooseOption()
        {

        }

        public void SendMail(string loginFrom)
        {
            Mail message;
            string[] labels = { "Do: ", "Temat: ", "Treść: " };
            string[] inputs = new string[3];
            int selectedIndex = 0;

            ConsoleKey key;
            do
            {
                sendMailView.StartCreateNewMessage(selectedIndex, labels, inputs);
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedIndex > 0) selectedIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedIndex < labels.Length - 1) selectedIndex++;
                        break;
                    case ConsoleKey.Enter:
                        sendMailView.PressEnter(selectedIndex, labels, inputs);
                        break;
                }
            } while (key != ConsoleKey.Spacebar);

            string recipientLogin = inputs[0];
            string subject = inputs[1];
            string content = inputs[2];

            message = new Mail(subject, content, loginFrom, recipientLogin);

            int userType = FindCorrectUser(loginFrom);
            switch (userType)
            {
                case 0:
                    throw new Exception("Error 404 - User not found");

                case 1:
                    var admin = db.Admins.FirstOrDefault(a => a.user.login == loginFrom);
                    if (admin != null)
                    {
                        db.Mails.Add(message);
                        message.read = true;
                        admin.user.messages.Add(message);
                    }
                    break;

                case 2:
                    var student = db.Students.FirstOrDefault(s => s.user.login == loginFrom);
                    if (student != null)
                    {
                        db.Mails.Add(message);
                        message.read = true;
                        student.user.messages.Add(message);
                    }
                    break;

                case 3:
                    var teacher = db.Teachers.FirstOrDefault(t => t.user.login == loginFrom);
                    if (teacher != null)
                    {
                        db.Mails.Add(message);
                        message.read = true;
                        teacher.user.messages.Add(message);
                    }
                    break;

                case 4:
                    var parent = db.Parents.FirstOrDefault(p => p.user.login == loginFrom);
                    if (parent != null)
                    {
                        db.Mails.Add(message);
                        message.read = true;
                        parent.user.messages.Add(message);
                    }
                    break;

                default:
                    throw new Exception("Invalid user type");
            }

            int recipientType = FindCorrectUser(recipientLogin);
            switch (recipientType)
            {
                case 0:
                    throw new Exception("Error 404 - Recipient not found");

                case 1:
                    var recipientAdmin = db.Admins.FirstOrDefault(a => a.user.login == recipientLogin);
                    if (recipientAdmin != null)
                    {
                        db.Mails.Add(message);
                        message.read = false;
                        recipientAdmin.user.messages.Add(message);
                    }
                    break;

                case 2:
                    var recipientStudent = db.Students.FirstOrDefault(s => s.user.login == recipientLogin);
                    if (recipientStudent != null)
                    {
                        db.Mails.Add(message);
                        message.read = false;
                        recipientStudent.user.messages.Add(message);
                    }
                    break;

                case 3:
                    var recipientTeacher = db.Teachers.FirstOrDefault(t => t.user.login == recipientLogin);
                    if (recipientTeacher != null)
                    {
                        db.Mails.Add(message);
                        message.read = false;
                        recipientTeacher.user.messages.Add(message);
                    }
                    break;

                case 4:
                    var recipientParent = db.Parents.FirstOrDefault(p => p.user.login == recipientLogin);
                    if (recipientParent != null)
                    {
                        db.Mails.Add(message);
                        message.read = false;
                        recipientParent.user.messages.Add(message);
                    }
                    break;

                default:
                    throw new Exception("Invalid recipient type");
            }
            db.SaveChanges();
        }
        public int FindCorrectUser(string login)
        {
            foreach (var u in db.Admins)
            {
                if (Equals(login, u.user.login)) { return 1; }
            }
            foreach (var u in db.Students)
            {
                if (Equals(login, u.user.login)) { return 2; }
            }
            foreach (var u in db.Teachers)
            {
                if (Equals(login, u.user.login)) { return 3; }
            }
            foreach (var u in db.Parents)
            {
                if (Equals(login, u.user.login)) { return 4; }
            }
            return 0;
        }
    }
}
