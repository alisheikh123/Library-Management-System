using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_ReservationLogs
    {
        public int id { get; set; }
        public int art_id { get; set; }
        public int? student { get; set; }
        public string isbn { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string activity { get; set; }
        public DateTime? date { get; set; }

    }
}