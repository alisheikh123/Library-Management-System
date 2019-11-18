using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_ReturnBook
    {
        public int id { get; set; }
        public int artCat_id { get; set; }
        [ForeignKey("artCat_id")]
        public virtual LR_ArticleCategory artCat { get; set; }
        [Required]
        public int? student_id { get; set; }
        public string Department { get; set; }
        public string book_title { get; set; }
       public string author_name { get; set; }
        public string ISBN { get; set; }
        public DateTime Issue_Date { get; set; }
        public DateTime DueDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime returnedDate { get; set; }
        public string Status { get; set; }
        public decimal? fine { get; set; }
        public string activity { get; set; }


    }
}