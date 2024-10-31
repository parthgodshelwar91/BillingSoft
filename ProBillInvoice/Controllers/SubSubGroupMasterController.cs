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
    public class SubSubGroupMasterController : Controller
    {
        // GET: SubSubGroupMaster        
        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";      
            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster_Groupwise("defunct = 'False'"), "group_code", "long_desc");
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ViewBag.SubGroupMaster = new SelectList(lsSGM.FillByGroupCode("01"), "sub_group_code", "long_desc");

            SubSubGroupMasterModel SSGM = new SubSubGroupMasterModel();
            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();            
            SSGM.SubSubGroupMasterList = lsSSGM.SubSubGroupMaster();            
            return View(SSGM);
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Titlename = "Edit";
            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster(), "group_code", "long_desc");
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ViewBag.SubGroupMaster = new SelectList(lsSGM.SubGroupMaster(), "sub_group_code", "long_desc");

            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            SubSubGroupMasterModel SSGM = lsSSGM.SubSubGroupMaster(id);
            SSGM.SubSubGroupMasterList = lsSSGM.SubSubGroupMaster();            
            return View("Create", SSGM);
        }

        public ActionResult Details(string id)
        {
            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            SubSubGroupMasterModel SubSubGroupMaster = lsSSGM.SubSubGroupMaster(id);
            return View(SubSubGroupMaster);
        }


        [HttpPost]
        public ActionResult Create(SubSubGroupMasterModel SSGM)
        {
            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            string SubSubGroupCode = string.Empty;
            SubSubGroupCode = lsSSGM.NextSubSubGroupCode(SSGM.sub_group_code);
            SSGM.last_edited_by = Session["LoginUserName"].ToString();

            if (SSGM.sub_sub_group_code != null)
            {
                SSGM.Mode = 1;               
                SSGM.last_edited_date = DateTime.Now;
                lsSSGM.InsertUpdate(SSGM);
              
                ViewBag.Message = "Detail save successfully";
            }

            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster(), "group_code", "long_desc");

            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ViewBag.SubGroupMaster = new SelectList(lsSGM.SubGroupMaster(), "sub_group_code", "long_desc");
            SSGM.SubSubGroupMasterList = lsSSGM.SubSubGroupMaster();
            ViewBag.Titlename = "Create";
            return View(SSGM);
        }

        [HttpPost]
        public ActionResult Edit(SubSubGroupMasterModel SSGM)
        {
            SSGM.last_edited_by = Session["LoginUserName"].ToString();

            ClsGroupMaster lsGM = new ClsGroupMaster();
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            if (SSGM.sub_sub_group_code != null)
            {
                SSGM.Mode = 2;               
                SSGM.last_edited_date = DateTime.Now;
                lsSSGM.InsertUpdate(SSGM);                
                ViewBag.Message = "Detail save successfully";
            }

            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster(), "group_code", "long_desc");
            ViewBag.SubGroupMaster = new SelectList(lsSGM.SubGroupMaster(), "sub_group_code", "long_desc");

            SSGM.SubSubGroupMasterList = lsSSGM.SubSubGroupMaster();
            ViewBag.Titlename = "Edit";
            return View("Create", SSGM);
        }


        public ActionResult FillByGroupCode(string GroupCode, string GroupName)
        {
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();           
            List<SubGroupMasterModel> subGroups = new List<SubGroupMasterModel>();
            var result = new { subGroups = new SelectList(lsSGM.FillByGroupCode(GroupCode), "sub_group_code", "long_desc") };
            return Json(result, JsonRequestBehavior.AllowGet);            
        }

        public ActionResult NextSubSubGroupCode(string SubGroupCode, string SubGroupName)
        {
            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            string lsSubSubGroupCode = lsSSGM.NextSubSubGroupCode(SubGroupCode);

            try
            {
                return Json(new
                {
                    msg = lsSubSubGroupCode
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}