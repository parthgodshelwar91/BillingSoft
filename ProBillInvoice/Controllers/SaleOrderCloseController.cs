using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class SaleOrderCloseController : Controller
    {
        // GET: SaleOrderClose
        public ActionResult Create()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string lsFromDate = string.Format("{0: yyyy/MM/dd 00:00:00.000}", DateTime.Now.AddMonths(-1));
            string lsToDate = string.Format("{0: yyyy/MM/dd 00:00:00.000}", DateTime.Now);

            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            string lsFilter = string.Empty;
            if (SiteId != 0)
            {
                lsFilter = lsFilter + "sale_order_header.site_id = " + SiteId + " and ";
            }
            lsFilter = lsFilter + "sale_order_header.order_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            SaleOrderModel mymodel = new SaleOrderModel();
            SaleOrderHeaderModel Header = new SaleOrderHeaderModel();
            ClsSaleorderClose lsSaleClose = new ClsSaleorderClose();
            Header.FromDate = Convert.ToDateTime(lsFromDate);
            Header.ToDate = Convert.ToDateTime(lsToDate);
            Header.SiteId = SiteId;
            mymodel.SaleOrderHeaderSearch = Header;
            mymodel.SaleOrderHeader = lsSaleClose.PendingSO(lsFilter);
            return View("Create", mymodel);
        }

        [HttpPost]
        public ActionResult Search(SaleOrderModel SOM)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(SiteId), "site_id", "site_name");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            string lsFilter = string.Empty;
            if (SOM.SaleOrderHeaderSearch.SiteId != null)
            {
                lsFilter = lsFilter + "sale_order_header.site_id = " + SOM.SaleOrderHeaderSearch.SiteId + " and ";
            }
            if (SOM.SaleOrderHeaderSearch.PartyId != null)
            {
                lsFilter = lsFilter + "sale_order_header.party_id = " + SOM.SaleOrderHeaderSearch.PartyId + " and ";
            }

            string lsFromDate = string.Format("{0: yyyy/MM/dd 00:00:00.000}", SOM.SaleOrderHeaderSearch.FromDate);
            string lsToDate = string.Format("{0: yyyy/MM/dd 00:00:00.000}", SOM.SaleOrderHeaderSearch.ToDate);
            lsFilter = lsFilter + "sale_order_header.order_date between '" + lsFromDate + "' and '" + lsToDate + "' ";

            ClsSaleorderClose lsSaleClose = new ClsSaleorderClose();
            SaleOrderModel mymodel = new SaleOrderModel();
            mymodel.SaleOrderHeader = lsSaleClose.PendingSO(lsFilter);
            return View("Create", mymodel);
        }

        [HttpPost]
        public ActionResult View(SaleOrderModel mymodel, string OrderID)
        {
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster(Convert.ToInt32(Session["LoginSiteId"])), "site_id", "site_name");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            ClsSaleorderClose lsSaleClose = new ClsSaleorderClose();
            mymodel.SaleOrderHeader = lsSaleClose.PendingSO("sale_order_header.order_id = " + OrderID + "");
            mymodel.SaleOrderDetail = lsSaleClose.PendingSO_Detail(OrderID);
            return View("Create", mymodel);
        }

        [HttpPost]
        public ActionResult Close(SaleOrderModel mymodel, string SaleOrderDetailID)
        {
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster((int)Session["LoginSiteId"]), "site_id", "site_name");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            ClsSaleorderClose lsSaleClose = new ClsSaleorderClose();
            int liOrderID = lsSaleClose.FindOrderId(SaleOrderDetailID);
            lsSaleClose.ItemClose(liOrderID.ToString(), SaleOrderDetailID, "True");

            mymodel.SaleOrderHeader = lsSaleClose.PendingSO("sale_order_header.order_id = " + liOrderID + "");
            mymodel.SaleOrderDetail = lsSaleClose.PendingSO_Detail(liOrderID.ToString());
            ViewBag.Messg = "Pending item are Closed";
            return View("Create", mymodel);
        }

        [HttpPost]
        public ActionResult AllClose(SaleOrderModel mymodel, string SaleOrderID)
        {
            ClsSiteMaster lsSite = new ClsSiteMaster();
            ViewBag.SiteList = new SelectList(lsSite.SiteMaster((int)Session["LoginSiteId"]), "site_id", "site_name");
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            ClsSaleorderClose lsSaleClose = new ClsSaleorderClose();
            lsSaleClose.AllItemClose(SaleOrderID.ToString(), "True");

            mymodel.SaleOrderHeader = lsSaleClose.PendingSO("sale_order_header.order_id = " + SaleOrderID + "");
            mymodel.SaleOrderDetail = lsSaleClose.PendingSO_Detail(SaleOrderID.ToString());
            ViewBag.Message = "All Pending Items are Closed";
            return View("Create", mymodel);
        }        
    }
}