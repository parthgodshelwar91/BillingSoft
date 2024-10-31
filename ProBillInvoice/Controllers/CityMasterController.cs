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
    public class CityMasterController : Controller
    {        
        // GET: CityMaster
        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            CityMasterModel CM = new CityMasterModel();
            ClsCityMaster lsCity = new ClsCityMaster();
            CM.city_id = lsCity.NextId();
            CM.CityList = lsCity.Citymaster();
            return View(CM);
        }
               
        public ActionResult Edit(int id)
        {
            ViewBag.Titlename = "Edit";
            ClsCityMaster lsCity = new ClsCityMaster();            
            CityMasterModel CM = lsCity.Citymaster(id);
            CM.CityList = lsCity.Citymaster();

            return View("Create", CM);
        }       

        [HttpPost]
        public ActionResult Create(CityMasterModel CM)
        {
            if (CM.city_id != 0)
            {
                ClsCityMaster lsCity = new ClsCityMaster();

                //Insert Table
                lsCity.InsertUpdate(1,CM);
                CM.CityList = lsCity.Citymaster();
                ViewBag.Message = "Detail Save Sucessfully";
            }
            return View("Create", CM);
        }

        [HttpPost]
        public ActionResult Edit(CityMasterModel CM)
        {
            if (CM.city_id != 0)
            {
                ClsCityMaster lsCity = new ClsCityMaster();
                //Update Table
                lsCity.InsertUpdate(2, CM);
                CM.CityList = lsCity.Citymaster();
                ViewBag.Message = "Detail Save Sucessfully";
            }
            return View("Create", CM);
        }
    }
}