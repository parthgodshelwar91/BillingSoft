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
    public class PurchaseOrderCloseController : Controller
    {
        // GET: ClosePendingPo
        public ActionResult Create()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string lsFromDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", DateTime.Now.AddMonths(-1));
            string lsToDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", DateTime.Now);

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            Header.FromDate = Convert.ToDateTime(lsFromDate);
            Header.ToDate = Convert.ToDateTime(lsToDate);            
            mymodel.PurchaseHeaderSearch = Header;

            string lsFilter = string.Empty;
            if (SiteId != 0)
            {
                lsFilter = lsFilter + "purchase_header.site_id=" + SiteId + " and ";
            }
            lsFilter = lsFilter + "purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' and po_import = 'false' ";

            ClsPO_Close lsPoClose = new ClsPO_Close();
            mymodel.PurchaseHeader = lsPoClose.PurchaseHeader_PendingList(lsFilter);
            return View("Create" , mymodel);
        }

        [HttpPost]
        public ActionResult Search(PurchaseOrderModel Purchase)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            string lsFilter = string.Empty;
            if(Purchase.PurchaseHeaderSearch.SiteId != null)
            {
                lsFilter = lsFilter + "purchase_header.site_id = " + Purchase.PurchaseHeaderSearch.SiteId +  " And ";
            }
            string lsFromDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", Purchase.PurchaseHeaderSearch.FromDate);
            string lsToDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", Purchase.PurchaseHeaderSearch.ToDate);
            lsFilter = lsFilter + "purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            PurchaseOrderModel mymodel = new PurchaseOrderModel();           
            ClsPO_Close lsPoClose = new ClsPO_Close();
            mymodel.PurchaseHeader = lsPoClose.PurchaseHeader_PendingList(lsFilter);

            return View("Create" , mymodel);
        }

        [HttpPost]
        public ActionResult View(PurchaseOrderModel Purchase, string PurchaseHeaderId)
         {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            ClsPO_Close lsPoClose = new ClsPO_Close();
            mymodel.PurchaseHeader = lsPoClose.PurchaseHeader_PendingList("purchase_header.po_id  =  " + PurchaseHeaderId + " ");
            mymodel.PurchaseDetail = lsPoClose.PurchaseDetail_PendingList(PurchaseHeaderId);
            return View("Create", mymodel);
        }

        [HttpPost]
        public ActionResult Close(PurchaseOrderModel Purchase, string PurchaseDetailId)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPO_Close lsPoClose = new ClsPO_Close();
            int liPurchaseId = lsPoClose.FindPOId(PurchaseDetailId);
            lsPoClose.PendingItemClose(liPurchaseId.ToString(), PurchaseDetailId, "True");

            PurchaseOrderModel mymodel = new PurchaseOrderModel();           
            mymodel.PurchaseHeader = lsPoClose.PurchaseHeader_PendingList("purchase_header.po_id  =  " + liPurchaseId + " ");
            mymodel.PurchaseDetail = lsPoClose.PurchaseDetail_PendingList(liPurchaseId.ToString());
            ViewBag.Messag = " Pending Items are closed";
            return View("Create" , mymodel);
        }

        [HttpPost]
        public ActionResult CloseAll(PurchaseOrderModel Purchase, string PoNo)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPO_Close lsPoClose = new ClsPO_Close();
            lsPoClose.AllItemClose(PoNo.ToString(), "True");

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            mymodel.PurchaseHeader = lsPoClose.PurchaseHeader_PendingList("purchase_header.po_id  =  " + PoNo + " ");
            mymodel.PurchaseDetail = lsPoClose.PurchaseDetail_PendingList(PoNo);
            ViewBag.Message = "All Pending Items are closed";

            return View("Create", mymodel);           
        }

    }
}