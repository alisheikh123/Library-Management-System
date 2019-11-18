using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_Reservations
    {
        public int id { get; set; }

        public int artCat_id { get; set; }
        [ForeignKey("artCat_id")]
        public virtual LR_ArticleCategory artCat { get; set; }
        public int? student_id { get; set; }
        public string ISBN { get; set; }
        public string title { get; set; }
        public string author_name { get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }
    }
}