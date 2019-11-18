using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models
{
    public class AddBook
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bookid { get; set; }

        [Display(Name ="ISBN")]
        [Required(ErrorMessage = "Required")]
        public int isbn { get; set; }

        [Required(ErrorMessage ="Required")]
        [Display(Name ="Book Name")]
        public string bookname { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Author Name")]
        public string authorname { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Edition")]
        public string edition { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Copies Name")]
        public int copies { get; set; }
        
        public DateTime date { get; set; }
    }
}