using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class ProductionSiteModel
    {      
        public int detail_id { get; set; }
        [Display(Name = "Schedule Id")]
        public int schedule_id { get; set; }
        [Display(Name = "Site Name")]
        public int site_id { get; set; }
        [Display(Name = "Site Name")]
        public string site_name { get; set; }
        [Display(Name = "Producation Capacity")]
        public decimal production_capacity { get; set; }
        [Display(Name = "Schedule Quanity")]
        public decimal scheduled_quanity { get; set; }
       
      
    }
}