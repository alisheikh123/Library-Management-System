using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Models
{
    public class LR_Books
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        //public int artCat_id { get; set; }
        //[ForeignKey("artCat_id")]
        //public virtual LR_ArticleCategory artCat { get; set; }

      [Required]
        // [RegularExpression(@"^ (?: ISBN(?:-1[03]) ?:?●)?(?=[0-9X]{10}$|(?=(?:[0-9]+[-●]){3})↵[-●0-9X]{13}$|97[89] [0-9]{10}$|(?=(?:[0-9]+[-●]){4})[-●0-9]{17}$)↵(?:97[89] [-●]?)?[0 - 9]{1,5}[-●]?[0 - 9]+[-●]?[0 - 9]+[-●]?[0 - 9X]$", ErrorMessage = "Enter Valid ISBN No of 10 or 13 digits")]
        //[RegularExpression(@"ISBN(?:-13) ?:?\x20 * (?=.{17}$)97(?:8|9)([-])\d{1,5}\1\d{1,7}\1\d{1,6}\1\d$", ErrorMessage = "Enter Valid ISBN No of 13 digits")]
        //[RegularExpression("^(?:ISBN(?:-13)?:? )?(?=[0-9]{13}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)97[89][- ]?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9]$",ErrorMessage ="Enter valid isbn no of 13 digits")]
        [RegularExpression(@"^(?:ISBN(?:-13)?:? )?(?=[0-9]{13}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)97[89][- ]?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9]$", ErrorMessage = "Enter valid 13 digits isbn number only")]
        [MaxLength(17, ErrorMessage = "ISBN number too long")]
        [Remote("Uniqu", "Home", AdditionalFields = "Id , ISBN", HttpMethod = "POST", ErrorMessage = "ISBN already exists!")]
        public string ISBN { get; set; }
        
        public string Language { get; set; }
        public string Company { get; set; }
        [Required]
        [StringLength(35)]
        [RegularExpression(@"^[a-zA-Z .-]+$", ErrorMessage = "Use letters only please")]
        public string name { get; set; }
        [StringLength(100)]
        public string description { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        public string author_name { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]
        public string publisher_name { get; set; }
        [Required]
        [Range(1, 9999, ErrorMessage = "Cost should be greater than 0.")]
        [RegularExpression(@"^(((\d{1})*))$", ErrorMessage = "Cost should be greater than 0.")]
        public float cost { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? purchase_date { get; set; }
        public DateTime? date { get; set; }
        public string activity { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime date_year_ofpublication { get; set; }
        [Range(1, 9999, ErrorMessage = "Edition should be greater than 0.")]
        [RegularExpression(@"^(((\d{1})*))$", ErrorMessage = "Edition should be greater than 0.")]
        public float edition { get; set; }
        public Decimal Impact_Factor { get; set; }
        public Decimal Eigen_Factor { get; set; }
        public int Bill_num { get; set; }
        public string status { get; set; }
        public string article_category { get; set; }
        [Range(1, 9999, ErrorMessage = "Quantity should be greater than 0.")]
        [RegularExpression(@"^(((\d{1})*))$", ErrorMessage = "Quantity should be greater than 0.")]
        [Required]
        public int Quantity { get; set; }
        public int Remaining_Quanity { get; set; }
        public string type_category { get; set; }

    }
}