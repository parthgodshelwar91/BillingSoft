using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class SiteMasterModel
    {

        [Display(Name = "Site Id")]
        public int  site_id { get; set; }

        [Display(Name = "Site Code")]
        [Required(ErrorMessage = "Site Code is required.")]
        [StringLength(2, ErrorMessage = "Site Code must not be more than 2 char")]
        public string site_code { get; set; }

        [Display(Name = "Site Name")]
        [Required(ErrorMessage = "Site Name is required.")]
        [StringLength(50, ErrorMessage = "Site name must not be more than 50 char")]
        public string site_name { get; set; }

        [Display(Name = "Site Address")]
        [Required(ErrorMessage = "Site Address is required.")]
        [StringLength(100, ErrorMessage = "Site Address must not be more than 100 char")]
        public string site_address { get; set; }

        [Display(Name = "Active")]
        public bool defunct { get; set; }
        [Display(Name = "Company")]
        public int company_id { get; set; }
        [Display(Name = "Plant Serial No")]
        public string plant_serial_no { get; set; }
        [Display(Name = "Mixer Capacity")]
        public string mixer_capacity { get; set; }
        [Display(Name = "Batch Size")]
        public string batch_size { get; set; }

        [Display(Name = "Last Edited By")]
        public string last_edited_by { get; set; }

        [Display(Name = "Last Edited Date")]
        public DateTime last_edited_date  { get; set; }

        public List<SiteMasterModel> SiteList { get; set; }
    }
}