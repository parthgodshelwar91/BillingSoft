using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class VehicleMasterModel
    {
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Vehicle No")]
        [Required(ErrorMessage = "Vehicle No is required.")]
        [StringLength(10, ErrorMessage = "Payterm code must not be more than 10 char")]
        public string vehicle_number { get; set; }

        [Display(Name = "Vehicle Type")]
        [Required(ErrorMessage = "Vehicle Type is required.")]
        [StringLength(10, ErrorMessage = "Vehicle Type must not be more than 10 char")]
        public string vehicle_type { get; set; }

        [Display(Name = "Vehicle Name")]
        [Required(ErrorMessage = "Vehicle Name is required.")]
        public string vehicle_name { get; set; }

        [Display(Name = "Transporter")]
        public int transporter_id { get; set; }

        [Display(Name = "Average")]
        public decimal average { get; set; }

        [Display(Name = "Opn Read.(Kms)")]
        public decimal reading { get; set; }

        [Display(Name = "Tare Wt")]
        public int tare_weight { get; set; }

        [Display(Name = "Tare Date")]
        public DateTime tare_date_time { get; set; }
        public int godown_id { get; set; }
        public bool on_server { get; set; }
        public bool on_web { get; set; }
        public List<VehicleMasterModel> VehicleList { get; set; }
    }
}