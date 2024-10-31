using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class Temp_StockModel
    {
        [Display(Name = "Party Name")]
        public int? PartyId { get; set; }

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

        [Display(Name = "Party Name")]
        public int party_id { get; set; }

        [Display(Name = "Party Name")]
        public string party_name { get; set; }

        [Display(Name = "Material")]
        public int material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "Unit")]
        public int unit_code { get; set; }

        [Display(Name = "Unit")]
        public string qty_unit { get; set; }

        [Display(Name = "Trips")]
        public int trips { get; set; }

        [Display(Name = "Quantity")]
        public decimal quantity { get; set; }
              
        public List<Temp_StockModel> Temp_StockList { get; set; }
    }
}