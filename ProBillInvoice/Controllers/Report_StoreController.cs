using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class Report_StoreController : Controller
    {
        // GET: Report_Store
        string currentMonth = DateTime.Now.Month.ToString();
        string currentYear = DateTime.Now.Year.ToString();
        public ActionResult BinCard()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            BinCardModel mymodel = new BinCardModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "BinCard Datewise";

            ClsMaterialMaster lsMM = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMM.MaterialMaster_Type("Purchase"), "material_id", "material_name");
                        
            ClsBinCard lsBinCard = new ClsBinCard();
            lsBinCard.spBinCard(0, Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.BinCardList = lsBinCard.BinCard();
            return View(mymodel);                       
        }

        [HttpPost]
        public ActionResult BinCard(BinCardModel BCM)
        {            
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", BCM.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", BCM.ToDate);
            ViewBag.Titlename = "BinCard Datewise";
            
            BinCardModel mymodel = new BinCardModel();
            ClsBinCard lsBinCard = new ClsBinCard();     
            if (BCM.MaterialId != null)
            {
                lsBinCard.spBinCard(Convert.ToInt32(BCM.MaterialId), Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            }

            ClsMaterialMaster lsMM = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(lsMM.MaterialMaster_Type("Purchase"), "material_id", "material_name", BCM.MaterialId);

            mymodel.BinCardList = lsBinCard.BinCard();
            return View(mymodel);
        }

        //***** StockDatewise Report *********************************************
        public ActionResult StockDatewiseReport()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            Temp_StStockDetailModel mymodel = new Temp_StStockDetailModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Stock Detail For Purchase Material Datewise";                       
            ClsTemp_StStockDetail lsTemp_StStockDetail = new ClsTemp_StStockDetail();
            lsTemp_StStockDetail.spTemp_StStockDetail(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_StStockDetailList = lsTemp_StStockDetail.Temp_StStockDetail();
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult StockDatewiseReport(Temp_StStockDetailModel Stock)
        {
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.ToDate);
            ViewBag.Titlename = "Stock Detail For Purchase Material Datewise";

            Temp_StStockDetailModel mymodel = new Temp_StStockDetailModel();
            ClsTemp_StStockDetail lsTemp_StStockDetail = new ClsTemp_StStockDetail();
            lsTemp_StStockDetail.spTemp_StStockDetail(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_StStockDetailList = lsTemp_StStockDetail.Temp_StStockDetail();
            return View(mymodel);
        }

               
        //***** SaleConsumption Datewise Report *********************************************
        public ActionResult SaleConsumption_DatewiseReport()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            Temp_StockModel mymodel = new Temp_StockModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Sale Consumption Summary";
            ClsTemp_Stock lsTemp_Stock = new ClsTemp_Stock();
            lsTemp_Stock.SaleConsumptionDatewise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_StockList = lsTemp_Stock.Temp_Stock();
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult SaleConsumption_DatewiseReport(Temp_StockModel Stock)
        {
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.ToDate);
            ViewBag.Titlename = "Sale Consumption Summary";

            Temp_StockModel mymodel = new Temp_StockModel();
            ClsTemp_Stock lsTemp_Stock = new ClsTemp_Stock();
            lsTemp_Stock.SaleConsumptionDatewise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_StockList = lsTemp_Stock.Temp_Stock();
            return View(mymodel);
        }


        //***** SaleConsumption Partywise Report *********************************************
        public ActionResult SaleConsumption_PartywiseReport()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            Temp_StockModel mymodel = new Temp_StockModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Sale Consumption Summary";

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");

            ClsTemp_Stock lsTemp_Stock = new ClsTemp_Stock();
            lsTemp_Stock.SaleConsumptionPartywise(8, Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_StockList = lsTemp_Stock.Temp_Stock();
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult SaleConsumption_PartywiseReport(Temp_StockModel Stock)
        {
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.ToDate);
            ViewBag.Titlename = "Sale Consumption Summary";

            Temp_StockModel mymodel = new Temp_StockModel();
            ClsTemp_Stock lsTemp_Stock = new ClsTemp_Stock();   
            if (Stock.PartyId != null)
            {               
                lsTemp_Stock.SaleConsumptionPartywise(Convert.ToInt32(Stock.PartyId), Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            }

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name", Stock.PartyId);                      

            mymodel.Temp_StockList = lsTemp_Stock.Temp_Stock();
            return View(mymodel);
        }


        //***** SaleSummary Datewise Report *********************************************
        public ActionResult SaleSummary_DatewiseReport()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            Temp_StockPivotModel mymodel = new Temp_StockPivotModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Sale Consumption Summary";

            ClsTemp_StockPivot lsTemp_StockPivot = new ClsTemp_StockPivot();
            lsTemp_StockPivot.SaleSummaryDatewise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_StockPivotList = lsTemp_StockPivot.Temp_StockPivot();
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult SaleSummary_DatewiseReport(Temp_StockModel Stock)
        {
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.ToDate);
            ViewBag.Titlename = "Sale Consumption Summary";

            Temp_StockPivotModel mymodel = new Temp_StockPivotModel();
            ClsTemp_StockPivot lsTemp_StockPivot = new ClsTemp_StockPivot();
            lsTemp_StockPivot.SaleSummaryDatewise(Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_StockPivotList = lsTemp_StockPivot.Temp_StockPivot();
            return View(mymodel);
        }

        //***** SaleSummary Partywise Report *********************************************
        public ActionResult SaleSummary_PartywiseReport()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            Temp_StockPivotModel mymodel = new Temp_StockPivotModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Sale Consumption Summary";
            
            ClsTemp_StockPivot lsTemp_StockPivot = new ClsTemp_StockPivot();
            lsTemp_StockPivot.SaleSummaryPartywise(8, Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_StockPivotList = lsTemp_StockPivot.Temp_StockPivot();
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult SaleSummary_PartywiseReport(Temp_StockModel Stock)
        {
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", Stock.ToDate);
            ViewBag.Titlename = "Sale Consumption Summary";

            Temp_StockPivotModel mymodel = new Temp_StockPivotModel();
            ClsTemp_StockPivot lsTemp_StockPivot = new ClsTemp_StockPivot();            
            lsTemp_StockPivot.SaleSummaryPartywise(Convert.ToInt32(Stock.PartyId), Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate));
            mymodel.Temp_StockPivotList = lsTemp_StockPivot.Temp_StockPivot();
            return View(mymodel);
        }

        //***** SaleDetail Partywise Report *********************************************
        public ActionResult SaleDetail_PartywiseReport()
        {
            string lsFromDate = string.Format("{0}-{1}-01 00:00:00.000", DateTime.Now.Year, DateTime.Now.Month);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", DateTime.Now);
            Temp_TicketsDetailModel mymodel = new Temp_TicketsDetailModel();
            mymodel.FromDate = Convert.ToDateTime(lsFromDate);
            mymodel.ToDate = Convert.ToDateTime(lsToDate);
            ViewBag.Titlename = "Sale Detail";

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name");

            ClsSaleInvoiceHeader lsSIH = new ClsSaleInvoiceHeader();
            ViewBag.InvoicePartyList = new SelectList(lsSIH.SaleInvoiceId_Partywise(8, Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate)), "sale_invoice_id", "invoice_no");                       

            ClsTemp_TicketsDetail lsTemp_TicketsDetail = new ClsTemp_TicketsDetail();
            lsTemp_TicketsDetail.SaleDetailPartywise(0, 0);
            mymodel.Temp_TicketsDetailList = lsTemp_TicketsDetail.Temp_TicketsDetail();
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult SaleDetail_PartywiseReport(Temp_TicketsDetailModel TicketsDetail)
        {
            string lsFromDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", TicketsDetail.FromDate);
            string lsToDate = string.Format("{0:yyyy-MM-dd 00:00:00.000}", TicketsDetail.ToDate);
            ViewBag.Titlename = "Sale Detail";

            Temp_TicketsDetailModel mymodel = new Temp_TicketsDetailModel();
            ClsTemp_TicketsDetail lsTemp_TicketsDetail = new ClsTemp_TicketsDetail();
            if (TicketsDetail.PartyId != null)
            {
                lsTemp_TicketsDetail.SaleDetailPartywise(Convert.ToInt32(TicketsDetail.PartyId), Convert.ToInt32(TicketsDetail.sale_invoice_id));
            }

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.FillByCategoryPartyMaster("C"), "party_id", "party_name", TicketsDetail.PartyId);

            ClsSaleInvoiceHeader lsSIH = new ClsSaleInvoiceHeader();
            ViewBag.InvoicePartyList = new SelectList(lsSIH.SaleInvoiceId_Partywise(Convert.ToInt32(TicketsDetail.PartyId), Convert.ToDateTime(lsFromDate), Convert.ToDateTime(lsToDate)), "sale_invoice_id", "invoice_no", TicketsDetail.sale_invoice_id);

            mymodel.Temp_TicketsDetailList = lsTemp_TicketsDetail.Temp_TicketsDetail();
            return View(mymodel);
        }
    }
}