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
    public class PartyMstCustLocationController : Controller
    {
        // GET: PartyMstMustLocation
        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("C"), "party_id", "party_name");
            ClsCityMaster lsCM = new ClsCityMaster();
            ViewBag.CityList = new SelectList(lsCM.Citymaster(), "city_id", "city_name");
            PartyMstCustLocationModel model = new PartyMstCustLocationModel();
            ClsPartyMstCustLocation lsPartyCust = new ClsPartyMstCustLocation();
            model.CustomerPartyList = lsPartyCust.CustomerPartymaster();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PartyMstCustLocationModel model)
        {
            ViewBag.Titlename = "Create";
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("C"), "party_id", "party_name");
            ClsCityMaster lsCM = new ClsCityMaster();
            ViewBag.CityList = new SelectList(lsCM.Citymaster(), "city_id", "city_name");
            ClsPartyMstCustLocation lsPartyCust = new ClsPartyMstCustLocation();
            if (model.id == 0)
            {
                model.Mode = 1;
                lsPartyCust.InsertUpdate(model);                
                ViewBag.Message = "Detail Save Sucessfully";
            }
            ModelState.Clear();
            PartyMstCustLocationModel mymodel = new PartyMstCustLocationModel();
            mymodel.CustomerPartyList = lsPartyCust.CustomerPartymaster();
            return View(mymodel);
        }

        public ActionResult Edit(int id)
        {
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("C"), "party_id", "party_name");
            ClsCityMaster lsCM = new ClsCityMaster();
            ViewBag.CityList = new SelectList(lsCM.Citymaster(), "city_id", "city_name");

            ViewBag.Titlename = "Edit";
            ClsPartyMstCustLocation lsPartyCust = new ClsPartyMstCustLocation();
            PartyMstCustLocationModel CM = lsPartyCust.CustomerPartymaster(id);
            CM.CustomerPartyList = lsPartyCust.CustomerPartymaster();
            return View("Create", CM);
        }

        [HttpPost]
        public ActionResult Edit(PartyMstCustLocationModel CM)
        {
            ClsPartyMaster lsPM = new ClsPartyMaster();
            ViewBag.PartyList = new SelectList(lsPM.PartyMaster_Categorywise("C"), "party_id", "party_name");
            ClsCityMaster lsCM = new ClsCityMaster();
            ViewBag.CityList = new SelectList(lsCM.Citymaster(), "city_id", "city_name");

            ViewBag.Titlename = "Edit";
            ClsPartyMstCustLocation lsPartyCust = new ClsPartyMstCustLocation();
            if (CM.id != 0)
            {
                CM.Mode = 2;
                lsPartyCust.InsertUpdate(CM);
                
                ViewBag.Message = "Detail Save Sucessfully";
            }
            ModelState.Clear();
            PartyMstCustLocationModel mymodel = new PartyMstCustLocationModel();
            mymodel.CustomerPartyList = lsPartyCust.CustomerPartymaster();            
            return View("Create", mymodel);
        }
    }
}