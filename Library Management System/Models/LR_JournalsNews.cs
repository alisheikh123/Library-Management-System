using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_JournalsNews
    {
        public int id { get; set; }
        public int artCat_id { get; set; }
        [ForeignKey("artCat_id")]
        public virtual LR_ArticleCategory artCat { get; set; }
       
        public string name { get; set; }
        [StringLength(100)]
        public string description { get; set; }
        [Required(ErrorMessage = "Please enter Author Name*")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        public string author_name { get; set; }
        [Required(ErrorMessage = "Please enter Publisher Name*")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        public string publisher_name { get; set; }
        [Required(ErrorMessage = "Please enter Cost*")]
        [Range(1, 9999, ErrorMessage = "Cost should be greater than 0.")]
        [RegularExpression(@"^(((\d{1})*))$", ErrorMessage = "Cost should be greater than 0.")]
        public float cost { get; set; }
        [Required(ErrorMessage = "Please enter Purchasing Date*")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? purchase_date { get; set; }
        public DateTime? date { get; set; }
        public string activity { get; set; }
        [Required(ErrorMessage = "Please enter Publication Date*")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime date_year_ofpublication { get; set; }

        [Required(ErrorMessage = "Please enter Edition*")]
        [Range(1, 9999, ErrorMessage = "Edition should be greater than 0.")]
        [RegularExpression(@"^(((\d{1})*))$", ErrorMessage = "Edition should be greater than 0.")]
        public float edition { get; set; }

        [Required(ErrorMessage = "Please enter Impact factor*")]
        public Decimal Impact_Factor { get; set; }
        [Required(ErrorMessage = "Please enter Eigen factor*")]
        public Decimal Eigen_Factor { get; set; }

        public int Bill_num { get; set; }
        public string status { get; set; }
        public string article_category { get; set; }
        [Range(1, 9999, ErrorMessage = "Quantity should be greater than 0.")]
        [RegularExpression(@"^(((\d{1})*))$", ErrorMessage = "Quantity should be greater than 0.")]
        [Required(ErrorMessage = "Please enter the quantity* ")]
        public int Quantity { get; set; }
        public string Company { get; set; }
        public string Language { get; set; }
        public int Remaining_Quanity { get; set; }
        public string type_category { get; set; }
    }
}