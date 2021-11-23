using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_UI.Models
{
 
     public class SellerRegister
    {
        //[Required]
       // [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        public string BusinessURL { get; set; }

        public string Industry { get; set; }



    }
}
