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
    public class MaterialMasterController : Controller
    {
        public ActionResult Index()
        {
            ClsMaterialMaster MM = new ClsMaterialMaster();
            MaterialMasterModel Material = new MaterialMasterModel();
            Material.MaterialMasterList = MM.MaterialMasterList("Purchase");
            return View(Material);
        }

        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster_Groupwise("group_code in('02','01')"), "group_code", "long_desc");          
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ViewBag.SubGroupMaster = new SelectList(lsSGM.SubGroupMaster(), "sub_group_code", "long_desc");
            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            ViewBag.SubSubGroupMaster = new SelectList(lsSSGM.SubSubGroupMaster(), "sub_sub_group_code", "long_desc");

            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");            
            ViewBag.AltUnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");

            MaterialMasterModel Material = new MaterialMasterModel();
            ClsMaterialMaster MM = new ClsMaterialMaster();
            Material.material_id = MM.NextId();
            Material.material_code = "*";
            Material.MaterialMasterList = MM.MaterialMaster_Type("Purchase");             
            return View(Material);
        }        

        public ActionResult Edit(int id)
        {
            ViewBag.Titlename = "Edit";
            ClsGroupMaster lsGM = new ClsGroupMaster();
            ViewBag.GroupMaster = new SelectList(lsGM.GroupMaster_Groupwise("group_code in('02','01')"), "group_code", "long_desc");
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            ViewBag.SubGroupMaster = new SelectList(lsSGM.SubGroupMaster(), "sub_group_code", "long_desc");                        
            ClsSubSubGroupMaster lsSSGM = new ClsSubSubGroupMaster();
            ViewBag.SubSubGroupMaster = new SelectList(lsSSGM.SubSubGroupMaster(), "sub_sub_group_code", "long_desc");

            ClsUnitMaster lsUnit = new ClsUnitMaster();
            ViewBag.UnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");
            ViewBag.AltUnitList = new SelectList(lsUnit.UOMMaster(), "unit_code", "long_desc");

            ClsMaterialMaster MM = new ClsMaterialMaster();
            MaterialMasterModel Material = MM.MaterialMaster(id);            
            Material.MaterialMasterList = MM.MaterialMaster_Type("Purchase");            
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
            int SiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());
            ClsMaterialMaster lsMM = new ClsMaterialMaster();
            if (Material.material_id != 0)
            {
                Material.Mode = 1;
                Material.material_type = "Purchase";
                Material.created_by = (string)Session["LoginUserName"];
                Material.created_date = DateTime.Now;

                lsMM.InsertUpdate(Material);
                ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
                StStockHeaderModel stStockHeader = new StStockHeaderModel();
                ClsSiteMaster lsSiteMaster = new ClsSiteMaster();
                List<SiteMasterModel> SiteModel = lsSiteMaster.SiteMasterList();
                foreach (var item in SiteModel)
                {
                    stStockHeader.Mode = 1;
                    stStockHeader.material_id = Material.material_id;
                    stStockHeader.unit_code = Material.unit_code;
                    stStockHeader.site_id = item.site_id;
                    stStockHeader.created_by = (string)Session["LoginUserName"];
                    stStockHeader.created_date = DateTime.Now;
                    lsStStockHeader.InsertUpdate(stStockHeader);
                }
                ViewBag.Message += "Detail saved successfully."+ Environment.NewLine + "Please make stock posting before use in transaction.";
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
            model.MaterialMasterList = lsMM.MaterialMaster_Type("Purchase");
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
                Material.material_type = "Purchase";
                Material.last_edited_by = "ADMIN";
                Material.last_edited_date = DateTime.Now;
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
            model.MaterialMasterList = lsMM.MaterialMaster_Type("Purchase"); 

            ViewBag.Titlename = "Edit";
            return View("Create", model);
        }

        //------------------------------------------
        public JsonResult FillByGroupCode(string GroupCode, string GroupName)
        {
            ClsSubGroupMaster lsSGM = new ClsSubGroupMaster();
            List<SubGroupMasterModel> SubGroupMaster = new List<SubGroupMasterModel>();
            SubGroupMaster = lsSGM.FillByGroupCode(GroupCode);
            var result = new { List = new SelectList(SubGroupMaster, "sub_group_code", "long_desc") };
            return Json(result);
        }

        public JsonResult FillBySubGroupCode(string SubGroupCode, string SubGroupName)
        {
            ClsSubSubGroupMaster lsSGM = new ClsSubSubGroupMaster();
            List<SubSubGroupMasterModel> SubSubGroupMaster = new List<SubSubGroupMasterModel>();
            SubSubGroupMaster = lsSGM.FillBySubGroupMaster(SubGroupCode);
            var result = new { List = new SelectList(SubSubGroupMaster, "sub_sub_group_code", "long_desc") };
            return Json(result);
        }

        public ActionResult NextItemCode(string SubSubGroupCode, string SubSubGroupName)
        {
            ClsMaterialMaster lsIM = new ClsMaterialMaster();
            string ItemCode = lsIM.NextItemCode(SubSubGroupCode);

            try
            {
                return Json(new
                {
                    msg = ItemCode
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult UnitCode(int id, string name)
        {
            ClsUnitMaster lsUnit = new ClsUnitMaster();
            UnitMasterModel UM = new UnitMasterModel();
            UM = lsUnit.UOMMaster(id);
            var result = new { UM.short_desc };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}