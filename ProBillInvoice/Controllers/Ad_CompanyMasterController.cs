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
    public class Ad_CompanyMasterController : Controller
    {
        // GET: Ad_CompanyMaster
        public ActionResult Index()
        {
            ClsCompanyMaster CM = new ClsCompanyMaster();
            return View(CM.CompanyMaster());
        }

        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            ClsStateMaster lsSM = new ClsStateMaster();
            ViewBag.StateMaster = new SelectList(lsSM.StateMaster(), "state_id", "state_name");

            Ad_CompanyMasterModel CM = new Ad_CompanyMasterModel();
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            CM.company_id = lsCM.NextId();
            CM.cst_valid_upto = DateTime.Now.AddYears(1);
            CM.CGCTValidityUpto = DateTime.Now.AddYears(1);
            CM.tin_valid_upto = DateTime.Now.AddYears(1);
            CM.pan_valid_upto = DateTime.Now.AddYears(1);            
            CM.CompanyMasterList = lsCM.CompanyMaster();                               
            return View(CM);
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Titlename = "Edit";
            ClsStateMaster lsSM = new ClsStateMaster();
            ViewBag.StateMaster = new SelectList(lsSM.StateMaster(), "state_id", "state_name");

            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            Ad_CompanyMasterModel CM = lsCM.CompanyMaster(id);
            CM.CompanyMasterList = lsCM.CompanyMaster();                                  
            return View("Create", CM);
        }

        public ActionResult Details(string id)
        {
            ClsCompanyMaster lsCM = new ClsCompanyMaster();
            Ad_CompanyMasterModel CM = lsCM.CompanyMaster(id);
            return View(CM);
        }

        [HttpPost]
        public ActionResult Create(Ad_CompanyMasterModel CM)
        {
            if (CM.company_id != 0)
            {
                CM.Mode = 1;                
                CM.installation_date = DateTime.Now;
                CM.trial_expire_date = DateTime.Now.AddMonths(3);
                CM.amc_expire_date = DateTime.Now.AddYears(1);                
                ClsCompanyMaster lsCM = new ClsCompanyMaster();
                lsCM.InsertUpdate(CM);
                                
                ClsStateMaster lsSM = new ClsStateMaster();
                ViewBag.StateMaster = new SelectList(lsSM.StateMaster(), "state_id", "state_name");

                CM.CompanyMasterList = lsCM.CompanyMaster();
                ViewBag.Message = "Detail saved successfully";
            }

            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult Edit(Ad_CompanyMasterModel CM)
        {
            if (CM.company_id != 0)
            {
                CM.Mode = 2;                
                ClsCompanyMaster lsCM = new ClsCompanyMaster();
                lsCM.InsertUpdate(CM);
                                
                ClsStateMaster lsSM = new ClsStateMaster();
                ViewBag.StateMaster = new SelectList(lsSM.StateMaster(), "state_id", "state_name");

                CM.CompanyMasterList = lsCM.CompanyMaster();
                ViewBag.Message = "Detail saved successfully";
            }

            return RedirectToAction("Create");
        }

    }
}