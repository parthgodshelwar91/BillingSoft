using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class FinancialYearModel
    {       
        [Display(Name = "Fin Year")]
        public string FinancialYear { get; set; }

        [Display(Name = "Active")]
        public bool is_active { get; set; }
    }
}