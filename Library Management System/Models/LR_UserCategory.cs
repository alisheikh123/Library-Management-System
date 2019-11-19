using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_UserCategory
    {
        public int id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(35)]
        public string name { get; set; }
        [Required]
        [StringLength(15)]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string shortname { get; set; }
        public int priority { get; set; }
         public DateTime? date { get; set; }
        [Required]
        public int BIssue_Limit { get; set; }
        [Required]
        public int FirstIDays { get; set; }
        [Required]
        public int ReIDays { get; set; }
        [Required]
        public int attempts { get; set; }



       

    }
}