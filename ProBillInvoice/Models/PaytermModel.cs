using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class PaytermModel
    {
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Payterm Code")]
        [Required(ErrorMessage = "Payterm code is required.")]
       // [StringLength(5, ErrorMessage = "Payterm code must not be more than 5 char")]
        public int payterm_code { get; set; }

        [Display(Name = "Payterm Description")]
        [Required(ErrorMessage = "Payterm description is required.")]
        [StringLength(50, ErrorMessage = "Payterm description must not be more than 50 char")]
        public string payterm_desc { get; set; }

        [Display(Name = "Payterm Days")]
        [Required(ErrorMessage = "Payterm days is required.")]       
        public decimal payterm_days { get; set; }

        public bool defunct { get; set; }

        [Display(Name = "Last Edited By")]
        public string last_edited_by { get; set; }

        [DataType(DataType.Date)]        
        [Display(Name = "Last Edited Date:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public Nullable<DateTime> last_edited_date { get; set; }

        public List<PaytermModel> PaytermList { get; set; }
    }
}