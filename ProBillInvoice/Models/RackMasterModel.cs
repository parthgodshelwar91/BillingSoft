using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class RackMasterModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }
                
        [Display(Name = "Rack Id")]
        [Required(ErrorMessage = "Rack ID is required.")]
        public int rack_id { get; set; }
        public int store_id { get; set; }

        [Display(Name = "Site Name")]
        [Required(ErrorMessage = "Site Id is required.")]
        public int site_id { get; set; }

        [Display(Name = "Site Name")]
        [Required(ErrorMessage = "Site Name is required.")]
        public string site_name { get; set; }

        [Display(Name = "Rack Code")]        
        [MaxLength(3, ErrorMessage = "Rack code must be at most 3 digits.")]

        public string rack_code { get; set; }

        [Display(Name = "Rack Name")]
        [Required(ErrorMessage = "Rack Name is required.")]
        [StringLength(50, ErrorMessage = "Rack Name must not be more than 50 Digit")]
        public string rack_name { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }
                
        public string last_edited_by { get; set; }
        public DateTime last_edited_date { get; set; }
    
        public List<RackMasterModel> RackList { get; set; }        
    }
}