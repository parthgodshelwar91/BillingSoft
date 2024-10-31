using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class Ad_UserLoginTrackingModel
    {
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ToDate { get; set; }

        //-----------------------------------------------------------------------------------
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "User Name")]
        public string user_email { get; set; }

        [Display(Name = "ip_address")]
        public string ip_address { get; set; }

        [Display(Name = "Log In Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> log_in_time { get; set; }

        [Display(Name = "Log Out Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> log_out_time { get; set; }

        [Display(Name = "session")]
        public string session_id { get; set; }

        public List<Ad_UserLoginTrackingModel> UserLoginTrackingList { get; set; }

        //public int id { get; set; }
        //public string user_email { get; set; }
        //public string ip_address { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //public Nullable<DateTime> log_in_time { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //public Nullable<DateTime> log_out_time { get; set; }
        //public int session_id { get; set; }


    }
}