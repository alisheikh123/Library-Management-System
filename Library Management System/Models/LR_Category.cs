using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_Category
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please enter Name*")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(35)]
        public string name { get; set; }
        [Required(ErrorMessage = "Please enter Short Name*")]
        [StringLength(15)]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string shortname { get; set; }
        public int parent { get; set; } 
        public string Activity { get; set; }
        public DateTime? date { get; set; }
    }
}