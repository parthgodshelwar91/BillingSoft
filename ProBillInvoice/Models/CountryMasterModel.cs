using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class CountryMasterModel
    {
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Country")]
        public int country_id { get; set; }
        public string short_name  { get; set; }
        public string country_name    { get; set; }
        public string country_pin  { get; set; }

    }
}