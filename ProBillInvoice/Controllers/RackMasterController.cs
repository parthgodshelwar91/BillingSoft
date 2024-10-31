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
    public class RackMasterController : Controller
    {
        // GET: RackMaster
        public ActionResult Index()
        {
            ClsRackMaster lsRM = new ClsRackMaster();
            return View(lsRM.RackMaster());
        }

        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");

            RackMasterModel RM = new RackMasterModel();
            ClsRackMaster lsRM = new ClsRackMaster();
            RM.rack_id = lsRM.NextId();
            RM.RackList = lsRM.RackMaster();            
            return View(RM);
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Titlename = "Edit";
            ClsRackMaster lsRM = new ClsRackMaster();
            RackMasterModel RM = lsRM.RackMaster(id);

            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name", RM.store_id);
            
            RM.RackList = lsRM.RackMaster();            
            return View("Create", RM);
        }

        public ActionResult Details(string id)
        {
            ClsRackMaster lsRM = new ClsRackMaster();
            RackMasterModel RM = lsRM.RackMaster(id);
            return View(RM);
        }

        [HttpPost]
        public ActionResult Create(RackMasterModel RM)
        {
            ViewBag.Titlename = "Create";
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");
            ClsRackMaster lsRM = new ClsRackMaster();
            if (RM.rack_id != 0)
            {
                RM.Mode = 1;
                RM.last_edited_by = "Rakesh";
                RM.last_edited_date = DateTime.Now;
                lsRM.InsertUpdate(RM);                
                ViewBag.Message = "Detail save successfully";
            }            
            RM.RackList = lsRM.RackMaster();
            return View(RM);
        }

        [HttpPost]
        public ActionResult Edit(RackMasterModel RM)
        {
            ViewBag.Titlename = "Edit";
            ClsStoreMaster clsSM = new ClsStoreMaster();
            ViewBag.StoreList = new SelectList(clsSM.StoreMaster(), "store_id", "store_name");

            ClsRackMaster lsRM = new ClsRackMaster();
            if (RM.rack_id != 0)
            {
                RM.Mode = 2;
                RM.last_edited_by = "Rakesh";
                RM.last_edited_date = DateTime.Now;
                lsRM.InsertUpdate(RM);

                ViewBag.Message = "Detail save successfully";
            }            
            RM.RackList = lsRM.RackMaster();
            return View("Create", RM);
        }
    }
}