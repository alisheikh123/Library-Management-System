using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Library_Management_System.Models
{
    public class LR_AlliedMaterial
    {
        public int id { get; set; }
        public int artCat_id { get; set; }
        [ForeignKey("artCat_id")]
        public virtual LR_ArticleCategory artCat { get; set; }
        [Required(ErrorMessage = "Please enter Title*")]
        public string title { get; set; }
        [Required(ErrorMessage = "Please enter Volume No*")]
        public int Volume_No { get; set; }
        [Required(ErrorMessage = "Please enter issue No*")]
        public int Issue_No { get; set; }
        [Required(ErrorMessage = "Please enter Publisher Name*")]
        [StringLength(35)]
        public string publisher_name { get; set; }
        public string thesis_category { get; set; }
        public string description { get; set; }
        public DateTime? purchase_date { get; set; }
        [Required(ErrorMessage = "Please enter the Cost*")]
        [Range(1, 9999, ErrorMessage = "Cost should be greater than 0.")]
        [RegularExpression(@"^(((\d{1})*))$", ErrorMessage = "Cost should be greater than 0.")]
        public int cost { get; set; }
        [Required(ErrorMessage = "Please enter Author Name*")]
        [StringLength(35)]
        public string author_name { get; set; }
        [Range(1, 9999, ErrorMessage = "Quantity should be greater than 0.")]
        [RegularExpression(@"^(((\d{1})*))$", ErrorMessage = "Quantity should be greater than 0.")]
        [Required(ErrorMessage = "Please enter Quantity*")]
        public int Quantity { get; set; }
        public DateTime? publication_date { get; set; }
        public string Pamphlet_Category { get; set; }
        public string Novel_category { get; set; }
        public string CD_category { get; set; }
        public string Reports_category { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string activity { get; set; }
        public DateTime? date { get; set; }
        public int Remaining_Quanity { get; set; }
    }
}