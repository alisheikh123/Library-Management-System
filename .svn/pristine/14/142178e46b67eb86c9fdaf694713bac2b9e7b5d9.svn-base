using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models
{
    public class LR_Fine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fineId { get; set; }
        [Display(Name ="Fine")]
        [Required(ErrorMessage = "Required")]
        public decimal fine { get; set; }

        [Display(Name = "Day Limit")]
        [Required(ErrorMessage = "Required")]
        public int dayLimit { get; set; }
    }
}