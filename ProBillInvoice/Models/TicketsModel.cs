using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class TicketsModel
    {
        //search critera
        [Display(Name = "Party Name")]
        public int? PartyId { get; set; }
               
        [Display(Name = "Acct Type")]
        public string AcctType { get; set; }

        [Display(Name = "Slip No")]        
        public string SlipNo { get; set; }

        [Display(Name = "Material")]
        public int? MaterialId { get; set; }

        [Display(Name = "Vehicle No")]
        public string VehicleNumber { get; set; }

        [Display(Name = "Transporter")]
        public int? TransporterId { get; set; }                      
                
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ToDate { get; set; }

        public int TotalCountvehical { get; set; }
        public int SumQty_in_cft { get; set; }
        public int SumNet_weight { get; set; }
               

        //Table
        [Display(Name = "Ticket No")]
        [Required(ErrorMessage = "Ticket Number is required.")]
        public int ticket_number { get; set; }

        [Display(Name = "Slip No")]
        [Required(ErrorMessage = "Slip no is required.")]
        public string slip_no { get; set; }

        [Display(Name = "Slip Type")]
        [Required(ErrorMessage = "Slip type is required.")]
        public string slip_type { get; set; }

        [Display(Name = "Trans Type")]
        //[Required(ErrorMessage = "Trans type is required.")]
        public string trans_type { get; set; }

        [Display(Name = "Acct Type")]
        //[Required(ErrorMessage = "Acct type is required.")]
        public string acct_type { get; set; }

        [Display(Name = "Vehicle No")]
        [Required(ErrorMessage = "Vehicle number is required.")]
        [StringLength(10, ErrorMessage = "Vehicle number must not be more than 10 char")]
        public string vehicle_number { get; set; }

        [Display(Name = "Ticket Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ticket_date_time { get; set; }

        [Display(Name = "Gross DateTime")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public Nullable<DateTime> gross_date_time { get; set; }

        [Display(Name = "Gross Wt")]
        [Required(ErrorMessage = "Gross Weight Required .")]
        [Range(0.01, 999999999, ErrorMessage = "Please enter Gross Weight")]
        public int gross_weight { get; set; }

        [Display(Name = "Tare DateTime")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public Nullable<DateTime> tare_date_time { get; set; }

        [Display(Name = "Tare Wt")]
        [Required(ErrorMessage = "Tare Weight Required.")]
        [Range(0.01, 999999999, ErrorMessage = "Please enter Tare Weight")]
        public int tare_weight { get; set; }

        [Display(Name = "Net Wt")]
        //[DisplayFormat(DataFormatString = "{0:0.000}")]
        public int net_weight { get; set; }
               
        [Display(Name = "Pending")]
        public bool pending { get; set; }

        [Display(Name = "Closed")]
        public bool closed { get; set; }

        [Display(Name = "Shift")]
        public string shift { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }

        [Display(Name = "Book No")]
        public string book_no { get; set; }

        [Display(Name = "Royalty No")]
        public string royalty_no { get; set; }

        [Display(Name = "DM No")]
        public string dm_no { get; set; }

        [Display(Name = "LR No")]
        public string lr_no { get; set; }

        [Display(Name = "Order No")]
        public int order_id { get; set; }

        [Display(Name = "Order No")]
        public string order_no { get; set; }

        [Display(Name = "Buyer Order No")]
        public string buyer_order_no { get; set; }

        [Display(Name = "Order Qty")]        
        public decimal order_qty { get; set; }        

        [Display(Name = "Material")]
        [Required(ErrorMessage = "Material is required.")]
        public int? material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "Conc Type")]
        public string concrete_type { get; set; }
       
        [Display(Name = "Mine")]
        public int? mine_id { get; set; }

        [Display(Name = "Customer")]
        public int? party_id { get; set; }

        [Display(Name = "Party Name")]
        public string party_name { get; set; }

        [Display(Name = "P Acct Id")]
        public int p_acct_id { get; set; }

        [Display(Name = "Loader Id")]
        public int? loader_id { get; set; }

        [Display(Name = "Loader Name")]
        public string loader_name { get; set; }

        [Display(Name = "Loading Rate")]
        public decimal loading_rate { get; set; }

        [Display(Name = "Transporter")]
        [Required(ErrorMessage = "Transporter field Required.")]
        public int? transporter_id { get; set; }

        [Display(Name = "Transporter")]
        public string transporter_name { get; set; }

        [Display(Name = "T Acct Id")]
        public int t_acct_id { get; set; }

        [Display(Name = "Transporting Rate")]
        public decimal transporting_rate { get; set; }
        [Display(Name = "Transporting Rate One")]
        public decimal transporting_rate_one { get; set; }

        [Display(Name = "Destination")]
        public int location_id { get; set; }
        [Display(Name = "Dist in km")]
        public decimal dist_in_km { get; set; }

        [Display(Name = "Trips")]
        public int trips { get; set; }

        [Display(Name = "Measurements")]
        public string measurements { get; set; }

        [Display(Name = "Qty In")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]        
        [Range(0.01, 999999999, ErrorMessage = "MCUB is required.")]
        public decimal qty_in_cft { get; set; }

        [Display(Name = "Unit")]
        public string qty_unit { get; set; }

        [Display(Name = "Brass Qty")]
        [Required(ErrorMessage = "Please enter Brass qty.")]
        [Range(0, Int32.MaxValue)]
        public decimal brass_qty { get; set; }

        [Display(Name = "Driver Name")]
        [StringLength(50, ErrorMessage = "Driver Name must not be more than 50 char")]
        public string driver_name { get; set; }
               
        public string site_incharge { get; set; }

        [Display(Name = "Contact Name")]
        [StringLength(50, ErrorMessage = "Contact Name must not be more than 50 char")]
        public string contact_name { get; set; }

        [Display(Name = "Contact No")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, ErrorMessage = "Contact Number must not be more than 10 Digit")]
        public string contact_no { get; set; }

        [Display(Name = "Batch No")]
        [StringLength(5, ErrorMessage = "Batch No must not be more than 5 char")]
        public string batch_no { get; set; }

        [Display(Name = "Docket No")]
        [StringLength(5, ErrorMessage = "Docket No must not be more than 5 char")]
        public string docket_no { get; set; }

        [Display(Name = "Slump At Plant")]
        public string slump_at_plant { get; set; }

        [Display(Name = "Recepe id")]
        public int recepe_id { get; set; }

        [Display(Name = "Material Source")]
        public string material_source { get; set; }

        [Display(Name = "Supplier Wt")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal supplier_wt { get; set; }

        [Display(Name = "Mouisture Content")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal mouisture_content { get; set; }

        [Display(Name = "Quality Check")]
        public string quality_check { get; set; }

        //image_one, image_two, image_three, image_four

        [Display(Name = "Is Valid")]
        public bool is_valid { get; set; }

        [Display(Name = "In P Use")]
        public bool in_p_use { get; set; }

        [Display(Name = "In T Use")]
        public bool in_t_use { get; set; }

        [Display(Name = "Rate")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal material_rate { get; set; }

        [Display(Name = "Sub Total")]
        public decimal sub_total { get; set; }

        [Display(Name = "CGST")]
        public decimal CGST { get; set; }

        [Display(Name = "SGST")]
        public decimal SGST { get; set; }

        [Display(Name = "IGST")]
        public decimal IGST { get; set; }

        [Display(Name = "Misc Amt")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal misc_amount { get; set; }

        [Display(Name = "Total Amt")]
        public decimal total_amount { get; set; }

        [Display(Name = "invoice no")]
        public int invoice_no { get; set; }

        [Display(Name = "Financial Year")]
        public string financial_year { get; set; }

        [Display(Name = "godown_id")]
        public int godown_id { get; set; }

        [Display(Name = "company_id")]
        public int company_id { get; set; }
        
        [Display(Name = "plant_serial_no")]
        public string plant_serial_no { get; set; }

        [Display(Name = "mixer_capacity")]
        public string mixer_capacity { get; set; }

        [Display(Name = "batch_size")]
        public string batch_size { get; set; }     
        
        [Display(Name = "on_server")]
        public bool on_server { get; set; }

        public bool is_modify { get; set; }

        public bool is_deleted { get; set; }

        [Display(Name = "on_web")]
        public bool on_web { get; set; }

        public string city_name { get; set; }
        public string mobile_no { get; set; }

        [Display(Name = "Sale Person")]
        public int? SalePersonId { get; set; }

        public int sale_person_id { get; set; }    
        public string sales_person { get; set; }  
        public string recipe_name { get; set; }        
        public string short_desc { get; set; }
        [Display(Name = "Site Name")]
        public string site_name { get; set; }
        //[Display(Name = "Amount")]
        //public decimal Amount { get; set; }
        //public int invoice_id { get; set; }

        public int unit_code { get; set; }
        public int cust_site_location_id { get; set; }
        public string location_detail { get; set; }
        public List<TicketsModel> TicketsList { get; set; }

        [Display(Name = "Batch Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        public string batch_start_time { get; set; }

        [Display(Name = "Batch End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss tt}")]
        public string batch_end_time { get; set; }

    }
}

//[Display(Name = "Start Time")]
//public DateTime StartTime { get; set; }
//[Display(Name = "Batch Start Time")]
//public string batch_start_time => StartTime.ToString("hh:mm tt");
//public DateTime EndTime { get; set; }
//[Display(Name = "Batch End Time")]
//public string batch_end_time => EndTime.ToString("hh:mm tt");