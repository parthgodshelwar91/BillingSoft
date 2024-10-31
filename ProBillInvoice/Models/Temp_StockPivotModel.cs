using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class Temp_StockPivotModel
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

        [Display(Name = "Trans Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> trans_date { get; set; }

        [Display(Name = "Party Name")]
        public int party_id { get; set; }

        [Display(Name = "Party Name")]
        public string party_name { get; set; }

        [Display(Name = "Trips")]
        public int trips { get; set; }

        [Display(Name = "Material-1")]
        public decimal material_id_1 { get; set; }

        [Display(Name = "Material-2")]
        public decimal material_id_2 { get; set; }
        
        [Display(Name = "Material-3")]
        public decimal material_id_3 { get; set; }

        [Display(Name = "Material-4")]
        public decimal material_id_4 { get; set; }

        [Display(Name = "Material-5")]
        public decimal material_id_5 { get; set; }

        [Display(Name = "Material-6")]
        public decimal material_id_6 { get; set; }

        [Display(Name = "Material-7")]
        public decimal material_id_7 { get; set; }

        [Display(Name = "Material-8")]
        public decimal material_id_8 { get; set; }

        [Display(Name = "Material-9")]
        public decimal material_id_9 { get; set; }

        [Display(Name = "Material-10")]
        public decimal material_id_10 { get; set; }

        public List<Temp_StockPivotModel> Temp_StockPivotList { get; set; }
    }
}