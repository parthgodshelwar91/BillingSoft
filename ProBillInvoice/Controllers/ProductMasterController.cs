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
    public class ProductMasterController : Controller
    {
        // GET: ProductMaster
        [Authorize]
        public ActionResult Create()
        {
            ClsGroupMaster lsGM = new ClsGroupMaster();            
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster_Groupwise("group_code in(03)"), "group_code", "long_desc");

            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ViewBag.SubGroupMaster = new SelectList(lsSGM.SubGroupMaster(), "sub_group_code", "long_desc");            
            
            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            //ViewBag.SubSubGroupMaster = new SelectList(lsSSGM.SubSubGroupMaster(), "sub_sub_group_code", "long_desc");
            ViewBag.SubSubGroupMaster = new SelectList(lsSSGM.FillByGroupCode("sub_sub_group_mst.group_code in(03)"), "sub_sub_group_code", "long_desc");

            

            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");
            ViewBag.AltUnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");

            ViewBag.Titlename = "Create";
            MaterialMasterModel Material = new MaterialMasterModel();
            ClsMaterialMaster MM = new ClsMaterialMaster();
            Material.group_code = "03";
            Material.sub_group_code = "0301";
            Material.material_id = MM.NextId();
            Material.MaterialMasterList = MM.MaterialMaster_Type("Sale");            
            return View(Material);
        }

        public ActionResult Edit(int id)
        {
            ClsGroupMaster lsGM = new ClsGroupMaster();            
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster_Groupwise("group_code in(03)"), "group_code", "long_desc");

            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ViewBag.SubGroupMaster = new SelectList(lsSGM.SubGroupMaster(), "sub_group_code", "long_desc");
            
            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            //ViewBag.SubSubGroupMaster = new SelectList(lsSSGM.SubSubGroupMaster(), "sub_sub_group_code", "long_desc");
            ViewBag.SubSubGroupMaster = new SelectList(lsSSGM.FillByGroupCode("sub_sub_group_mst.group_code in(03)"), "sub_sub_group_code", "long_desc");

            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");
            ViewBag.AltUnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");

            ViewBag.Titlename = "Edit";
            ClsMaterialMaster MM = new ClsMaterialMaster();
            MaterialMasterModel Material = MM.MaterialMaster(id);
            Material.MaterialMasterList = MM.MaterialMaster_Type("Sale");            
            return View("Create", Material);
        }

        public ActionResult Details(int id)
        {
            ClsMaterialMaster MM = new ClsMaterialMaster();
            MaterialMasterModel Material = MM.MaterialMaster(id);
            return View(Material);
        }

        [HttpPost]
        public ActionResult Create(MaterialMasterModel Material)
        {      
            ClsMaterialMaster lsMM = new ClsMaterialMaster();
            if (Material.material_id != 0)
            {
                Material.Mode = 1;
                Material.material_type = "Sale";
                Material.created_by = User.Identity.Name;
                Material.created_date = DateTime.Now;
                lsMM.InsertUpdate(Material);
                ViewBag.Message = "Detail saved successfully";                
            }
            ModelState.Clear();

            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster(), "group_code", "long_desc");

            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ViewBag.SubGroupMaster = new SelectList(lsSGM.SubGroupMaster(), "sub_group_code", "long_desc");

            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            ViewBag.SubSubGroupMaster = new SelectList(lsSSGM.SubSubGroupMaster(), "sub_sub_group_code", "long_desc");

            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");
            ViewBag.AltUnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");

            MaterialMasterModel model = new MaterialMasterModel();
            model.MaterialMasterList = lsMM.MaterialMaster_Type("Sale");
            ViewBag.Titlename = "Create";
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MaterialMasterModel Material)
        {            
            ClsMaterialMaster lsMM = new ClsMaterialMaster();
            if (Material.material_id != 0)
            {
                Material.Mode = 2;
                Material.material_type = "Sale";
                Material.last_edited_by = User.Identity.Name;
                Material.last_edited_date = DateTime.Now;
                lsMM.InsertUpdate(Material);
                ViewBag.Message = "Detail saved successfully. Please make stock posting before use in transaction.";
            }
            ModelState.Clear();

            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster(), "group_code", "long_desc");

            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ViewBag.SubGroupMaster = new SelectList(lsSGM.SubGroupMaster(), "sub_group_code", "long_desc");

            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            ViewBag.SubSubGroupMaster = new SelectList(lsSSGM.SubSubGroupMaster(), "sub_sub_group_code", "long_desc");

            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");
            ViewBag.AltUnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");

            MaterialMasterModel model = new MaterialMasterModel();
            model.MaterialMasterList = lsMM.MaterialMaster_Type("Sale");
            ViewBag.Titlename = "Edit";
            return View("Create", model);
        }
    }
}