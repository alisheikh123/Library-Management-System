using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_ReturnLogs
    {
        public int id { get; set; }
        public int? student { get; set; }
        public int art_cat { get; set; }
        public string title { get; set; }
        public string isbn { get; set; }
        public string author { get; set; }
        public string Status { get; set; }
        public DateTime? returnedDate { get; set; }
    }
}