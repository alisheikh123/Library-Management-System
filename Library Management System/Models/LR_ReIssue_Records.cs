using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_ReIssue_Records
    {
       public int id { get; set; }
        public int? student_id { get; set; }
        public string ISBN { get; set; }
        public string author_name { get; set; }
        public int artCat_id { get; set; }
        [ForeignKey("artCat_id")]
        public virtual LR_ArticleCategory artCat { get; set; }
        public int fine { get; set; }
        public DateTime? date { get; set; }
        public DateTime ReIssue_Date { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
        public string title { get; set; }
        public string activity { get; set; }
    }
}