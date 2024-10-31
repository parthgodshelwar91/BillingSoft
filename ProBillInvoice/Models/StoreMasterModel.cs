using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class StoreMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Store Id")]
        [Required(ErrorMessage = "Rack ID is required.")]
        public int store_id { get; set; }

        [Display(Name = "Site Name")]
        [Required(ErrorMessage = "Site Id is required.")]
        public int site_id { get; set; }

        [Display(Name = "Site Name")]
        [Required(ErrorMessage = "Site Name is required.")]
        public string site_name { get; set; }

        [Display(Name = "Store Code")]
        [MaxLength(3, ErrorMessage = "Store code must be at most 3 digits.")]
        public string store_code { get; set; }

        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "Store name is required.")]
        [MaxLength(50, ErrorMessage = "Store name must be at most 50 digits.")]
        public string store_name { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }

        public string last_edited_by { get; set; }
        public DateTime last_edited_date { get; set; }

        public List<StoreMasterModel> StoreList { get; set; }
    }
}