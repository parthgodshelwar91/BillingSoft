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
    public class PurchaseOrderApprovalController : Controller
    {
        // GET: PurchaseApproval  
        public ActionResult Create()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            string lsFromDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", DateTime.Now.AddMonths(-1));
            string lsToDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", DateTime.Now);

            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");

            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            PurchaseHeaderModel Header = new PurchaseHeaderModel();
            Header.FromDate = Convert.ToDateTime(lsFromDate);
            Header.ToDate = Convert.ToDateTime(lsToDate);
            Header.SiteId = SiteId;
            mymodel.PurchaseHeaderSearch = Header;
            string lsFilter = string.Empty;
            if (SiteId != 0)
            {
                lsFilter = lsFilter + "purchase_header.site_id = " + SiteId + " and ";
            }
            lsFilter = lsFilter + "purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' and po_import = 'false' and purchase_header.po_close_flag = 'False' ";

            ClsPO_Approval lsPurchase = new ClsPO_Approval();           
            mymodel.PurchaseHeader = lsPurchase.Header_FillByPendingForApproval(lsFilter);

            return View(mymodel);
        }
      
        [HttpPost]
        public ActionResult Search(PurchaseOrderModel POM)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();                      
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");

            string lsFilter = string.Empty;
            if (POM.PurchaseHeaderSearch.SiteId != 0) //SiteId
            {
                 lsFilter = lsFilter + "purchase_header.site_id = " + POM.PurchaseHeaderSearch.SiteId + " and "; //SiteId
            }
            if (POM.PurchaseHeaderSearch.company_id != null)
            {
                lsFilter = lsFilter + "purchase_header.company_id = " + POM.PurchaseHeaderSearch.company_id + " and ";
            }
            if (POM.PurchaseHeaderSearch.PartyId != null)
            {
                lsFilter = lsFilter + "purchase_header.party_id = " + POM.PurchaseHeaderSearch.PartyId + " and ";
            }

            string lsFromDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", POM.PurchaseHeaderSearch.FromDate);
            string lsToDate = string.Format("{0:yyyy/MM/dd 00:00:00.000}", POM.PurchaseHeaderSearch.ToDate);

            lsFilter = lsFilter + "purchase_header.po_date between '" + lsFromDate + "' and '" + lsToDate + "' ";
                        
            ClsPO_Approval lsPurchase = new ClsPO_Approval();
            PurchaseOrderModel mymodel = new PurchaseOrderModel();
            mymodel.PurchaseHeader = lsPurchase.Header_FillByPendingForApproval(lsFilter);
            return View("Create", mymodel);
        }            
               
        [HttpPost]
        public ActionResult View(PurchaseOrderModel mymodel, string PurchaseHeaderId)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();                     
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");

            ClsPO_Approval lsPOApproval = new ClsPO_Approval();           
            mymodel.PurchaseHeader = lsPOApproval.Header_FillByPendingForApproval("purchase_header.po_id = " + PurchaseHeaderId + "");           
            mymodel.PurchaseDetail = lsPOApproval.Detail_FillByPendingForApproval(PurchaseHeaderId);
            return View("Create", mymodel);
        }

        [HttpPost]
        public ActionResult Approved(PurchaseOrderModel mymodel, string PurchaseDetailId)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsPO_Approval lsPOApproval = new ClsPO_Approval();           
            int liPoId = lsPOApproval.FindPoId(PurchaseDetailId);
            lsPOApproval.POItemApproval(liPoId.ToString(), PurchaseDetailId, "True");

            //-----------------------------------------------------------------------------
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();                    
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");

            mymodel.PurchaseHeader = lsPOApproval.Header_FillByPendingForApproval("purchase_header.po_id = " + liPoId + "");            
            mymodel.PurchaseDetail = lsPOApproval.Detail_FillByPendingForApproval(liPoId.ToString());
            ViewBag.Messag = "Item is Approved";
            return View("Create", mymodel);
        }

        [HttpPost]
        public ActionResult AllApproved(PurchaseOrderModel mymodel, string poId)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsPO_Approval lsPOApproval = new ClsPO_Approval();            
            lsPOApproval.AllPOItemApproval(poId.ToString(), "True");

            //-----------------------------------------------------------------------------
            ClsSiteMaster lsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(lsSM.SiteMaster(SiteId), "site_id", "site_name");

            ClsPartyMaster lsPM = new ClsPartyMaster();            
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster(), "party_id", "party_name");

            ClsCompanyMaster lsCompany = new ClsCompanyMaster();
            ViewBag.CompanyMst = new SelectList(lsCompany.CompanyMaster(), "company_id", "company_name");

            mymodel.PurchaseHeader = lsPOApproval.Header_FillByPendingForApproval("purchase_header.po_id = " + poId + "");           
            mymodel.PurchaseDetail = lsPOApproval.Detail_FillByPendingForApproval(poId.ToString());
            ViewBag.Message = "All Items are Approved";
            return View("Create", mymodel);
        }
    }
}