using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class BrandMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Brand Name")]               
        public int brand_id { get; set; }

        [Display(Name = "Brand Name")]
        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(50, ErrorMessage = "Brand name must not be more than 50 char")]
        public string brand_name { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }
        public string last_edited_by { get; set; }
        public DateTime last_edited_date { get; set; }

        public List<BrandMasterModel> BrandList { get; set; }

       
    }
}