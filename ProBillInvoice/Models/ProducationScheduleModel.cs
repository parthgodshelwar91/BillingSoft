using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class ProducationScheduleModel
    {
        [Display(Name = "Schedule Id")]
        public int schedule_id { get; set; }
        [Display(Name = "Schedule No")]
        public string schedule_no { get; set; }

        [Display(Name = "Schedule Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> schedule_datetime { get; set; }

        [Display(Name = "Party Name")]
        public int party_id { get; set; }
        [Display(Name = "Party Name")]
        public string party_name { get; set; }
        [Display(Name = "Sale Order")]
        public int order_id { get; set; }
        [Display(Name = "Sale Order")]
        public string order_no { get; set; }
        [Display(Name = "Material Name")]
        public int material_id { get; set; }
        [Display(Name = "Material Name")]
        public string material_name { get; set; }
        public int godown_id { get; set; }       
        public bool on_server { get; set; }
        public bool on_web { get; set; }
        public string financial_year { get; set; }
      
        public List<ProductionSiteModel> ProdSiteList { get; set; }
        public List<ProducationScheduleModel> ProdScheduleList { get; set; }
    }
}