using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class StStockHeaderModel
    {
        [Display(Name = "Site Name")]
        public int? SiteId { get; set; }

        [Display(Name = "Material")]
        public int? MaterialId { get; set; }

        [Display(Name = "Store Name")]
        public int? StoreId { get; set; }
               
        [Display(Name = "Rack Name")]
        public int? RackId { get; set; }

        [Display(Name = "Storewise Level")]
        public bool Storewise_flag { get; set; }

        [Display(Name = "BrandWise Level")]
        public bool BrandWise_flag { get; set; }        

        //------------------------------------------------------------------------------
        [Display(Name = "Mode")]
        public int Mode { get; set; }

        [Display(Name = "ST Stock Header ID")]
        [Required(ErrorMessage = "ST Stock Header ID is required.")]
        public int st_stock_header_id { get; set; }

        [Display(Name = "Material")]
        public int material_id { get; set; }

        [Display(Name = "Material")]
        public string material_name { get; set; }

        [Display(Name = "Brand")]
        public int? brand_id { get; set; }

        [Display(Name = "Brand")]
        public string brand_name { get; set; }

        [Display(Name = "Store Name")] 
        public int? store_id { get; set; }

        [Display(Name = "Store Name")]
        public string store_name { get; set; }

        [Display(Name = "Unit")]
        public int unit_code { get; set; }

        [Display(Name = "Unit")]
        public string short_desc { get; set; }

        [Display(Name = "Site Name")]
        public int? site_id { get; set; }

        [Display(Name = "Site Name")]
        public string site_name { get; set; }

        [Display(Name = "Rack Name")]
        public int? rack_id { get; set; }

        [Display(Name = "Rack Name")]
        public string rack_name { get; set; }

        [Display(Name = "Opening Qty")]
        public decimal opening_qty { get; set; }

        [Display(Name = "Total Rec Qty")]
        public decimal total_rec_qty { get; set; }

        [Display(Name = "Total Iss Qty")]
        public decimal total_iss_qty { get; set; }

        [Display(Name = "Qty")]
        public decimal total_balance { get; set; }

        [Display(Name = "Amount")]
        public decimal total_amount { get; set; }        

        [Display(Name = "Re Order")]
        public decimal re_order { get; set; }

        [Display(Name = "Minm Level")]
        public decimal min_level { get; set; }

        [Display(Name = "Maxm Level")]
        public decimal max_level { get; set; }

        [Display(Name = "Rate")]
        public decimal item_avg_rate { get; set; }

        public string created_by { get; set; }
        public Nullable<DateTime> created_date { get; set; }
        public string last_edited_by { get; set; }
        public Nullable<DateTime> last_edited_date { get; set; }
    }
}