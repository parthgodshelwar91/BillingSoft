using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class BinCardModel
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

        [Display(Name = "Trans Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime trans_date { get; set; }

        [Display(Name = "Rec Type")]
        public string rec_type { get; set; }

        [Display(Name = "Rec Type")]
        public decimal issue_qty { get; set; }

        [Display(Name = "Opening Qty")]
        public decimal opening_qty { get; set; }

        [Display(Name = "Rec Qty")]
        public decimal received_qty { get; set; }

        [Display(Name = "Issued Qty")]
        public decimal issued_qty { get; set; }

        [Display(Name = "Machine")]
        public int machine_id { get; set; }
                
        public List<BinCardModel> BinCardList { get; set; }      
    }
}