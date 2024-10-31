using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProBillInvoice.Models
{
    public class Temp_TicketsDetailModel
    {
        [Display(Name = "Party Name")]
        public int? PartyId { get; set; }

        [Display(Name = "Inv No")]
        public int? sale_invoice_id { get; set; }

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

        [Display(Name = "Ticket No")]      
        public int ticket_number { get; set; }

        [Display(Name = "Slip No")]
        public string slip_no { get; set; }

        [Display(Name = "Slip Type")]
        public string slip_type { get; set; }

        [Display(Name = "Trans Type")]
        public string trans_type { get; set; }

        [Display(Name = "Acct Type")]
        public string acct_type { get; set; }

        [Display(Name = "Vehicle No")]
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
        public int gross_weight { get; set; }

        [Display(Name = "Tare DateTime")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public Nullable<DateTime> tare_date_time { get; set; }

        [Display(Name = "Tare Wt")]
        public int tare_weight { get; set; }

        [Display(Name = "Net Wt")]
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
        
        [Display(Name = "Material")]
        public int? material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "concrete type")]
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

        [Display(Name = "Site Name")]
        public string location_name { get; set; }

        [Display(Name = "dist in km")]
        public decimal dist_in_km { get; set; }

        [Display(Name = "Measurements")]
        public string measurements { get; set; }

        [Display(Name = "Qty In CFT")]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal qty_in_cft { get; set; }

        [Display(Name = "Brass Qty")]       
        [Range(0, Int32.MaxValue)]
        public decimal brass_qty { get; set; }

        [Display(Name = "Unit")]
        public string qty_unit { get; set; }

        [Display(Name = "Driver Name")]
        public string driver_name { get; set; }

        [Display(Name = "Site Incharge")]
        public string site_incharge { get; set; }

        [Display(Name = "Contact Name")]
        public string contact_name { get; set; }

        [Display(Name = "Contact No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Contact number")]
        public string contact_no { get; set; }

        [Display(Name = "Batch No")]
        public string batch_no { get; set; }

        [Display(Name = "slump at plant")]
        public string slump_at_plant { get; set; }

        [Display(Name = "Recepe id")]
        public int recepe_id { get; set; }

        [Display(Name = "Recipe No")]
        public string recipe_name { get; set; }
                
        [Display(Name = "Material Source")]
        public string material_source { get; set; }

        [Display(Name = "Supplier wt")]
        public decimal supplier_wt { get; set; }

        [Display(Name = "mouisture content")]
        public decimal mouisture_content { get; set; }

        [Display(Name = "Quality check")]
        public string quality_check { get; set; }

        [Display(Name = "Is Valid")]
        public bool is_valid { get; set; }

        [Display(Name = "In P Use")]
        public bool in_p_use { get; set; }

        [Display(Name = "In T Use")]
        public bool in_t_use { get; set; }

        [Display(Name = "Material Rate")]
        public decimal material_rate { get; set; }

        [Display(Name = "Sub Total")]
        public decimal sub_total { get; set; }

        [Display(Name = "CGST")]
        public decimal CGST { get; set; }

        [Display(Name = "SGST")]
        public decimal SGST { get; set; }

        [Display(Name = "IGST")]
        public decimal IGST { get; set; }

        [Display(Name = "Misc Amount")]
        public decimal misc_amount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal total_amount { get; set; }

        [Display(Name = "invoice no")]
        public int invoice_no { get; set; }

        [Display(Name = "Financial Year")]
        public string financial_year { get; set; }

        public int godown_id { get; set; }
        public bool is_modify { get; set; }

        //------------------------------------
        [Display(Name = "20MM")]
        public decimal M_20MM { get; set; }

        [Display(Name = "RSAND")]
        public decimal RSAND { get; set; }

        [Display(Name = "10MM")]
        public decimal M_10MM { get; set; }

        [Display(Name = "CSAND")]
        public decimal CSAND { get; set; }

        [Display(Name = "CEMENT")]
        public decimal CEMENT { get; set; }

        [Display(Name = "FLYASH")]
        public decimal FLYASH { get; set; }

        [Display(Name = "WATER")]
        public decimal WATER { get; set; }

        [Display(Name = "ADMIX300")]
        public decimal ADMIX300 { get; set; }

        [Display(Name = "ADMIX350")]
        public decimal ADMIX350 { get; set; }

        [Display(Name = "ADMIX2202")]
        public decimal ADMIX2202 { get; set; }

        [Display(Name = "ADMIX400")]
        public decimal ADMIX400 { get; set; }

        [Display(Name = "MAPEIFLUID_R106")]
        public decimal MAPEIFLUID_R106 { get; set; }

        [Display(Name = "PC350")]
        public decimal PC350 { get; set; }
                
        public List<Temp_TicketsDetailModel> Temp_TicketsDetailList { get; set; }      
    }
}


//        SELECT TOP(200) ticket_number, slip_no, slip_type, trans_type, acct_type, vehicle_number, ticket_date_time, gross_date_time, gross_weight, tare_date_time, tare_weight, net_weight, pending, closed, shift, status, book_no, 
//                         royalty_no, dm_no, lr_no, order_id, material_id, concrete_type, mine_id, party_id, p_acct_id, loader_id, loader_name, loading_rate, transporter_id, t_acct_id, transporting_rate, transporting_rate_one, location_id, dist_in_km, 
//                         measurements, qty_in_cft, qty_unit, brass_qty, driver_name, site_incharge, contact_name, contact_no, batch_no, slump_at_plant, recepe_id, material_source, supplier_wt, mouisture_content, quality_check, is_valid, in_p_use, 
//                         in_t_use, material_rate, sub_total, CGST, SGST, IGST, misc_amount, total_amount, invoice_no, financial_year, godown_id, is_modify, [20MM], RSAND, [10MM], CSAND, CEMENT, FLYASH, WATER, ADMIX300, ADMIX350, 
//                         ADMIX2202, ADMIX400, MAPEIFLUID_R106, PC350
//FROM            temp_tickets_detail