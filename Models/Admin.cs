﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class Admin
    {
        public int adminId {  get; set; }
        public User user { get; set; }
        public Admin(User user)
        {
            this.user = user;
        }
    }
}
