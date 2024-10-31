using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class RecipeHeaderModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }  

        public int recipe_id { get; set; }

        [Display(Name = "Recipe No")]
        public string recipe_no { get; set; }

        [Display(Name = "Recipe Name")]
        [Required(ErrorMessage = "Recipe name is required.")]
        public string recipe_name { get; set; }

        public int party_id { get; set; }

        public int site_id { get; set; }
       
        //--------------
        public string party_name { get; set; }
    }
}