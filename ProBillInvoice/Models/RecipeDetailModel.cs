using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class RecipeDetailModel
    {
        public int Mode { get; set; }
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        public int godown_id { get; set; }
        public bool on_server { get; set; }
        public bool on_web { get; set; }
        
        public int recipe_detail_id { get; set; }

        public int recipe_id { get; set; }

        [Display(Name = "Material")]
        public int material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "Recipe Name")]
        public string material_recipe_name { get; set; }

        public int material_recipe_id { get; set; }

        public decimal quantity { get; set; }

        public decimal recipe_qty { get; set; }

        public decimal rate { get; set; }

        public int site_id { get; set; }

        //------OFF Weighment---
        public decimal mcub { get; set; }

        public decimal qty_in_cft { get; set; }

        //public decimal recipe_qty { get; set; }
    }
}