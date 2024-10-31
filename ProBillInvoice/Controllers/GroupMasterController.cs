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
    public class GroupMasterController : Controller
    {
        // GET: GroupMaster        
        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            GroupMasterModel GM = new GroupMasterModel();
            ClsGroupMaster lsGM = new ClsGroupMaster();
            GM.group_code = lsGM.NextGroupCode();
            GM.GroupMasterList = lsGM.GroupMaster();            
            return View(GM);
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Titlename = "Edit";
            ClsGroupMaster lsGM = new ClsGroupMaster();
            GroupMasterModel GM = lsGM.GroupMaster(id);
            GM.GroupMasterList = lsGM.GroupMaster();            
            return View("Create", GM);
        }

        public ActionResult Details(string id)
        {
            ClsGroupMaster lsGM = new ClsGroupMaster();
            GroupMasterModel GroupMaster = lsGM.GroupMaster(id);
            return View(GroupMaster);
        }

        [HttpPost]
        public ActionResult Create(GroupMasterModel GM)
        {
            
            ClsGroupMaster lsGM = new ClsGroupMaster();
            if (GM.group_code != null)
            {
                GM.Mode = 1;
                GM.last_edited_by = Session["LoginUserName"].ToString();
                GM.last_edited_date = DateTime.Now;                
                lsGM.InsertUpdate(GM);                
                ViewBag.Message = "Detail save successfully";
            }
            GM.GroupMasterList = lsGM.GroupMaster();
            return View(GM);
        }

        [HttpPost]
        public ActionResult Edit(GroupMasterModel GM)
        {      
            ClsGroupMaster lsGM = new ClsGroupMaster();
            if (GM.group_code != null)
            {
                GM.Mode = 2;
                GM.last_edited_by = Session["LoginUserName"].ToString();
                GM.last_edited_date = DateTime.Now;                
                lsGM.InsertUpdate(GM);              
                ViewBag.Message = "Detail save successfully";
            }
            GM.GroupMasterList = lsGM.GroupMaster();
            return View("Create", GM);
        }
    }
}