using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class CityMasterModel
    {
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "City")]
        public int city_id { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City Name is required.")]
        public string city_name { get; set; }
        public int tahasil_id { get; set; }
        public string tahasil_name { get; set; }
        public int district_id { get; set; }
        public string district_name{ get; set; }
        public int State_Id { get; set; }
        public string state_name{ get; set; }
        public int country_id { get; set; }
        public string country_name{ get; set; }
        public bool Defunct { get; set; }

        public List<CityMasterModel> CityList { get; set; }
    }
}