using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models
{
    public class LR_Issue
    {
       

        public int id { get; set; }
        public int artCat_id { get; set; }
        [ForeignKey("artCat_id")]
        public virtual LR_ArticleCategory artCat { get; set; }
        [Required]
      
        public int? student_id { get; set; }
        public string Department { get; set; }

        public string book_title { get; set; }
        //[Required]
        public string ISBN { get; set; }
        //[Required]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        //[DataType(DataType.Date)]
        public DateTime Issue_Date { get; set; }
        //[Required]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        //[DataType(DataType.Date)]
        public string title { get; set; }
        public string author_name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string activity { get; set; }
        public string Status { get; set; }
        public int Remaining_Quanity { get; set; }
        public string Label { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
    }
}