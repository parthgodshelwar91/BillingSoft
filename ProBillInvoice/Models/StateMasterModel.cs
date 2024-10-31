using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class StateMasterModel
    {
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "State")]
        public int  state_id { get; set; }
        public string  state_code  { get; set; }
        public string state_type { get; set; }
        public string short_name { get; set; }
        public string state_name { get; set; }
        public string state_desc { get; set; }
        public int country_id { get; set; }
        public string country_name { get; set; }
        public bool defunct { get; set; }
    }
}