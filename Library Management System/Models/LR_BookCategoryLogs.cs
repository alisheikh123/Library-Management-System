using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_BookCategoryLogs
    {
        public int id { get; set; }
        public int cat_id { get; set; }
        public string name { get; set; }
        public int parent { get; set; }
        public DateTime? date { get; set; }
        public string Activity { get; set; }
    }
}