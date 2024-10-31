using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class Temp_TicketsModel
    {
        [Display(Name = "Party Name")]
        public int? PartyId { get; set; }

        [Display(Name = "Material")]
        public int? MaterialId { get; set; }

        [Display(Name = "Transporter")]
        public int? TransporterId { get; set; }               

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Date is Requried")]
        public Nullable<DateTime> ToDate { get; set; }

        //------------------------------------------------------------------------------        
        [Display(Name = "Sr No")]
        public int sr_no { get; set; }

        [Display(Name = "Party Name")]
        public int? party_id { get; set; }

        [Display(Name = "Party Name")]
        public string party_name { get; set; }

        [Display(Name = "Transporter")]
        public int? transporter_id { get; set; }

        [Display(Name = "Transporter")]
        public string transporter_name { get; set; }

        [Display(Name = "Material")]
        public int? material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "Trips")]
        public int trips { get; set; }

        [Display(Name = "Qty Unit")]
        public string qty_unit { get; set; }

        [Display(Name = "Total Qty")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal qty_in_cft { get; set; }

        [Display(Name = "Total Net Wt")]
        public int net_weight { get; set; }

        [Display(Name = "Supplier Wt")]
        public decimal supplier_wt { get; set; }

        public List<Temp_TicketsModel> Temp_TicketsList { get; set; }
    }
}