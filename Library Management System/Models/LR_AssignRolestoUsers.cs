using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_Management_System.Models
{
    public class LR_AssignRolestoUsers
    {
         public int id { get; set; }
         public int user { get; set; }
         public int usercategory_id { get; set; }
        [ForeignKey("usercategory_id ")]
        public virtual LR_UserCategory category { get; set; }
         public int priority { get; set; }
        public DateTime? date { get; set; }
    }
}