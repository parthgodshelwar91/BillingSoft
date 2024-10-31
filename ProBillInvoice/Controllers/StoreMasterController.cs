using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Controllers
{
    public class StoreMasterController : Controller
    {
        // GET: StoreMaster
        public ActionResult Index()
        {
            ClsStoreMaster lsSM = new ClsStoreMaster();
            return View(lsSM.StoreMaster());
            
        }

        public ActionResult Create()
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ViewBag.Titlename = "Create";
            ClsSiteMaster clsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(clsSM.SiteMaster(SiteId), "site_id", "site_name");

            StoreMasterModel SM = new StoreMasterModel();
            ClsStoreMaster lsSM = new ClsStoreMaster();
            SM.store_id = lsSM.NextId();
            
            SM.StoreList = lsSM.StoreMaster();
            return View(SM);
        }
        [HttpPost]
        public ActionResult Create(StoreMasterModel SM)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ViewBag.Titlename = "Create";
            ClsSiteMaster clsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(clsSM.SiteMaster(SiteId), "site_id", "site_name");

            RackMasterModel RM = new RackMasterModel();
            ClsStoreMaster lsSM = new ClsStoreMaster();
            ClsRackMaster lsRM = new ClsRackMaster();
            if (SM.store_id != 0)
            {
                SM.Mode = 1;
                SM.last_edited_by = User.Identity.Name;
                SM.last_edited_date = DateTime.Now;
                lsSM.InsertUpdate(SM);                                
                ViewBag.Message = "Detail save successfully";
            }
            SM.StoreList = lsSM.StoreMaster();
            return View(SM);
        }

        public ActionResult Edit(string id)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ViewBag.Titlename = "Edit";
            
            ClsStoreMaster lsSM = new ClsStoreMaster();
            StoreMasterModel SM = lsSM.StoreMaster(id);

            ClsSiteMaster clsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(clsSM.SiteMaster(SiteId), "site_id", "site_name", SM.site_id);
            SM.StoreList = lsSM.StoreMaster();
            return View("Create", SM);
        }
        [HttpPost]
        public ActionResult Edit(StoreMasterModel SM)
        {
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ViewBag.Titlename = "Edit";
            ClsSiteMaster clsSM = new ClsSiteMaster();
            ViewBag.StoreList = new SelectList(clsSM.SiteMaster(SiteId), "site_id", "site_name");
            ClsStoreMaster lsSM = new ClsStoreMaster();
            if (SM.store_id != 0)
            {
                SM.Mode = 2;
                SM.last_edited_by = User.Identity.Name;
                SM.last_edited_date = DateTime.Now;
                lsSM.InsertUpdate(SM);

                ViewBag.Message = "Detail save successfully";
            }
            SM.StoreList = lsSM.StoreMaster();
            return View("Create", SM);
        }

        public ActionResult Details(string id)
        {
            ClsStoreMaster lsSM = new ClsStoreMaster();
            StoreMasterModel SM = lsSM.StoreMaster(id);
            return View(SM);
        }
    }
}