using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_UserCategoryLogs
    {
        public int id { get; set; }
        public int user { get; set; }
        public int category { get; set; }
        public int priority { get; set; }
        public string status { get; set; }
        public DateTime? date { get; set; }
    }
}