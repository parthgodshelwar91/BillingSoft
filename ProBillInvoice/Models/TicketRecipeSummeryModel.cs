using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class TicketRecipeSummeryModel
    {
        public int ticket_recipe_id { get; set; }
        public int ticket_number { get; set; }
        public int recipe_id { get; set; }
        public int SrNo { get; set; }
        public int material_id{get;set;}
        public decimal	recipe_qty { get; set; }
	    public decimal mcub { get; set; } 
	    public decimal quantity { get; set; } 
	    public decimal rate { get; set; }
        public decimal	total { get; set; }
        public int godown_id { get; set; } 
	    public bool on_server { get; set; } 
	    public bool on_web { get; set; } 
    }
}