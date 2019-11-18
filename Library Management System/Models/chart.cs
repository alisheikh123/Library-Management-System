using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class chart
    {

        public chart(double y, string label)
        {
            this.Y = y;
            this.label = label;
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string label { get; set; }

      
        public double Y { get; private set; }
    }
}