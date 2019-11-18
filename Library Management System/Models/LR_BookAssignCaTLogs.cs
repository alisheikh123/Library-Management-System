using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_BookAssignCaTLogs
    {
        public int id { get; set; }
        public int book_id { get; set; }
        public int book { get; set; }
        public int category { get; set; }
        public string Activity { get; set; }
        public DateTime? date { get; set; }
    }
}