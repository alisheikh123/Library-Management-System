using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
 
    public class DataPointcs
    {
        public DataPointcs(string label, double y)
        {
            this.Label = label;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [Display(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [Display(Name = "y")]
        public Nullable<double> Y = null;
    }
}