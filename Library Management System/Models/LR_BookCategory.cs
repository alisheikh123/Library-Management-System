using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_BookCategory
    {
        public int id { get; set; }
        public int book_id { get; set; }
        [ForeignKey("book_id")]
      public virtual LR_Books book { get; set; }
        public int category_id { get; set; }
        [ForeignKey("category_id ")]
       public virtual LR_Category category{ get; set; }
        public string Activity { get; set; }
        public DateTime? date { get; set; }

    }
}