using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class Tickets_OfflineModel   
    {
        public RecipeHeaderModel RecipeHeader { get; set; }
        public RecipeDetailModel RD { get; set; }
        public List<RecipeDetailModel> RecipeDetail { get; set; }
        public List<RecipeHeaderModel> RHM { get; set; }
        public TicketsModel Tickets { get; set; }
        public CityMasterModel CityMaster { get; set; }
        public List<TicketsModel> OfflineTicketList { get; set; }
        public Ad_CompanyMasterModel Company { get; set; }                      
    }
}