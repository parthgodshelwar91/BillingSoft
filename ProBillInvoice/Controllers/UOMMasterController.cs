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
    public class UOMMasterController : Controller
    {
        // GET: UnitMaster
        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            UnitMasterModel UOM = new UnitMasterModel();
            ClsUnitMaster lsUOM = new ClsUnitMaster();
            UOM.unit_code = lsUOM.NextId();
            UOM.StoreUOMList = lsUOM.UOMMaster();            
            return View(UOM);
        }
                
        public ActionResult Edit(int id)
        {
            ViewBag.Titlename = "Edit";
            ClsUnitMaster lsUOM = new ClsUnitMaster();
            UnitMasterModel UOM = lsUOM.UOMMaster(Convert.ToInt32(id));
            UOM.StoreUOMList = lsUOM.UOMMaster();
            return View("Create", UOM);
        }        

        public ActionResult Details(int id)
        {
            ClsUnitMaster lsUOM = new ClsUnitMaster();
            UnitMasterModel UOMMaster = lsUOM.UOMMaster(Convert.ToInt32(id));

            return View(UOMMaster);
        }

        [HttpPost]
        public ActionResult Create(UnitMasterModel UOM)
        {
            ClsUnitMaster lsUOM = new ClsUnitMaster();
            if (UOM.unit_code != 0)
            {
                //int unit_code = UOM.unit_code;
                string short_desc = UOM.short_desc;
                string long_desc = UOM.long_desc;
                bool defunct = UOM.defunct;
                string last_edited_by = User.Identity.Name;
                DateTime last_edited_date = DateTime.Now;

                //Insert Table
                lsUOM.InsertUpdate(1, UOM.unit_code, short_desc, long_desc, defunct, last_edited_by, last_edited_date);
                ViewBag.Message = "Detail save successfully";
            }
            UOM.StoreUOMList = lsUOM.UOMMaster();
            ViewBag.Titlename = "Create";
            return View(UOM);
        }

        [HttpPost]
        public ActionResult Edit(UnitMasterModel UOM)
        {
            ClsUnitMaster lsUOM = new ClsUnitMaster();
            if (UOM.unit_code != 0)
            {
                int unit_code = UOM.unit_code;
                string short_desc = UOM.short_desc;
                string long_desc = UOM.long_desc;
                bool defunct = UOM.defunct;
                string last_edited_by = User.Identity.Name;
                DateTime last_edited_date = DateTime.Now;

                //Update Table
                lsUOM.InsertUpdate(2,unit_code, short_desc, long_desc, defunct, last_edited_by, last_edited_date);
                ViewBag.Message = "Detail save successfully";
            }
            UOM.StoreUOMList = lsUOM.UOMMaster();
            ViewBag.Titlename = "Edit";
            return View("Create", UOM);
        }
    }
}