﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class StudentClass
    {
        public int studentClassId {  get; set; }
        [MaxLength(1)]
        public string number {  get; set; }
        [MaxLength(1)]
        public string letter { get; set; }
        public ICollection <Student> students { get; set; }
        public int teacherId { get; set; }
        public int? calendarId { get; set; }
        public StudentClass() { }
        public StudentClass(string number, string letter, int teacherId) 
        { 
            students = new List<Student>(); this.number = number; this.letter = letter; this.teacherId = teacherId;
        }
    }
}
