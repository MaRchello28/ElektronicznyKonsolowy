using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Models
{
    public class Mail
    {
        public int mailId {  get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public bool read = false;
        public Mail() { }
        public Mail(string subject, string body, string from, string to) 
        { 
            this.subject = subject; this.from = from; this.to = to; this.body = body;
        }
    }
}
