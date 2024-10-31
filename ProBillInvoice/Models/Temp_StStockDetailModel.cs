using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class Temp_StStockDetailModel
    {
        [Display(Name = "Material")]
        public int? MaterialId { get; set; }

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ToDate { get; set; }

        //------------------------------------------------------------------------------        
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Material")]
        public int material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "Unit")]
        public int unit_code { get; set; }

        [Display(Name = "Unit")]
        public string short_desc { get; set; }
              
        [Display(Name = "Opening Qty")]
        public decimal opening_qty { get; set; }

        [Display(Name = "Rec Qty")]
        public decimal received_qty { get; set; }

        [Display(Name = "Issued Qty")]
        public decimal issued_qty { get; set; }

        [Display(Name = "Balance Qty")]
        public decimal total_balance { get; set; }
        
        public List<Temp_StStockDetailModel> Temp_StStockDetailList { get; set; }
    }
}