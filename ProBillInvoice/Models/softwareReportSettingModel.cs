using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class softwareReportSettingModel
    {
        [Display(Name = "Mode")]
        public int Mode { get; set; }
        public int report_id { get; set; }        
        public string report_name { get; set; }
        
        public string d_report_title { get;set;}
        public string report_title { get; set; }

        public string d_logo { get; set; }
        public string logo { get; set; }

        public string d_authorized_signature { get; set; }
        public string authorized_signature { get; set; }

        public string d_stamp { get; set; }
        public string stamp { get; set; }
        public string report_title_forecolor { get; set; }
        public string d_report_title_forecolor { get; set; }
        public string d_report_title_fontSize { get; set; }
        public string report_title_fontSize { get; set; }
        public string company_name_forecolor { get; set; }
        public string d_company_name_forecolor { get; set; }
        public string d_stamp_name { get; set; }
        public string stamp_name { get; set; }

        public string d_reg_no { get; set; }
        public string reg_no { get; set; }

    }
}

//d_reg_no,reg_no