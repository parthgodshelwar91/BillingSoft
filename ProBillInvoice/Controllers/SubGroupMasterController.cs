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
    public class SubGroupMasterController : Controller
    {
        // GET: Store_SubGroupMaster
        //public ActionResult Index()
        //{
        //    ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
        //    return View(lsSGM.SubGroupMaster());
        //}

        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster_Groupwise("defunct = 'False'"), "group_code", "long_desc");

            SubGroupMasterModel SGM = new SubGroupMasterModel();
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();            
            SGM.SubGroupMasterList = lsSGM.SubGroupMaster();            
            return View(SGM);
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Titlename = "Edit";
            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster(), "group_code", "long_desc");

            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            SubGroupMasterModel SGM = lsSGM.SubGroupMaster(id);
            SGM.SubGroupMasterList = lsSGM.SubGroupMaster();            
            return View("Create", SGM);
        }

        public ActionResult Details(string id)
        {
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            SubGroupMasterModel SubGroupMaster = lsSGM.SubGroupMaster(id);
            return View(SubGroupMaster);
        }

        [HttpPost]
        public ActionResult Create(SubGroupMasterModel SGM)
        {
            SGM.last_edited_by = Session["LoginUserName"].ToString();

            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            string SubGroupCode = string.Empty;
            SubGroupCode = lsSGM.NextSubGroupCode(SGM.group_code);

            if (SGM.sub_group_code != null)
            {
                SGM.Mode = 1;
                SGM.last_edited_date = DateTime.Now;
                lsSGM.InsertUpdate(SGM);               
                ViewBag.Message = "Detail save successfully";
            }
            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster(), "group_code", "long_desc");
            SGM.SubGroupMasterList = lsSGM.SubGroupMaster();
            return View(SGM);
        }

        [HttpPost]
        public ActionResult Edit(SubGroupMasterModel SGM)
        {
            SGM.last_edited_by = Session["LoginUserName"].ToString();

            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            if (SGM.sub_group_code != null)
            {
                SGM.Mode = 2;                
                SGM.last_edited_date = DateTime.Now;
                lsSGM.InsertUpdate(SGM);                
                ViewBag.Message = "Detail save successfully";
            }
            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster(), "group_code", "long_desc");
            SGM.SubGroupMasterList = lsSGM.SubGroupMaster();
            return View("Create", SGM);
        }


        public ActionResult NextSubGroupCode(string GroupCode, string GroupName)
        {
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            string lsSubGroupCode = lsSGM.NextSubGroupCode(GroupCode);

            try
            {
                return Json(new
                {
                    msg = lsSubGroupCode
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}