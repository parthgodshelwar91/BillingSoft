using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProBillInvoice.Models;

namespace ProBillInvoice
{
    public class StStockHeader_StockPostingModel
    {
        public StStockHeaderModel StStockHeaderSearch { get; set; }
        public List<StStockHeaderModel> StStockHeader { get; set; }
        public List<StStockHeaderModel> StStockHeaderList { get; set; }
    }
}