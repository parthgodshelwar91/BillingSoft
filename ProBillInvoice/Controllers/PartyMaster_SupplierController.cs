using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Controllers
{
    [Authorize]
    public class PartyMaster_SupplierController : Controller
    {
        public ActionResult Index()
        {            
            ClsPartyMaster lsParty = new ClsPartyMaster();
            PartyMasterModel PM = new PartyMasterModel();
            PM.CustomerList = lsParty.FillByCategoryPartyMaster("S");
            return View(PM);
        }

        // GET: PartyMaster_Supplier
        public ActionResult Create() //Country State City
        {
            ClsCountryMaster lsCountry = new ClsCountryMaster();
            ViewBag.CountryList = new SelectList(lsCountry.CountryMaster(), "country_id", "country_name");
            ClsStateMaster lsState = new ClsStateMaster();
            ViewBag.StateList = new SelectList(lsState.StateMaster(), "state_id", "state_name");
            ClsCityMaster lsCity = new ClsCityMaster();
            ViewBag.CityList = new SelectList(lsCity.Citymaster(), "city_id", "city_name");

            ClsPartyType lsPY = new ClsPartyType();
            ViewBag.PartyType = new SelectList(lsPY.PartyTypeCategory("Supplier"), "type_id", "type_name");

            ViewBag.Titlename = "Create";
            PartyMasterModel PM = new PartyMasterModel();
            ClsPartyMaster lsParty = new ClsPartyMaster();
            PM.party_id = lsParty.NextId();
            PM.party_code = string.Format("{0}{1}", "S", lsParty.NextNo("S").PadLeft(5, '0'));
            PM.country_id = 1;
            PM.state_id = 15;
            //PM.city_id = 1;
            PM.type_id = 1;
            PM.CustomerList = lsParty.FillByCategoryPartyMaster("S");
            return View(PM);
        }

        public ActionResult Edit(string id)
        {
            ClsCountryMaster lsCountry = new ClsCountryMaster();
            ViewBag.CountryList = new SelectList(lsCountry.CountryMaster(), "country_id", "country_name");
            ClsStateMaster lsState = new ClsStateMaster();
            ViewBag.StateList = new SelectList(lsState.StateMaster(), "state_id", "state_name");
            ClsCityMaster lsCity = new ClsCityMaster();
            ViewBag.CityList = new SelectList(lsCity.Citymaster(), "city_id", "city_name");
            ClsPartyType lsPY = new ClsPartyType();
            ViewBag.PartyType = new SelectList(lsPY.PartyTypeCategory("Supplier"), "type_id", "type_name");

            ViewBag.Titlename = "Edit";
            ClsPartyMaster lsParty = new ClsPartyMaster();
            PartyMasterModel PM = lsParty.PartyMaster(id);
            PM.CustomerList = lsParty.FillByCategoryPartyMaster("S");
            return View("Create", PM);
        }

        public ActionResult Details(string id)
        {
            ClsPartyMaster lsParty = new ClsPartyMaster();
            PartyMasterModel Party = lsParty.PartyMasterList("party_mst.party_id = " + id + " ");
            return View(Party);
        }

        [HttpPost]
        public ActionResult Create(PartyMasterModel PM)
        {
            ClsCountryMaster lsCountry = new ClsCountryMaster();
            ViewBag.CountryList = new SelectList(lsCountry.CountryMaster(), "country_id", "country_name");
            ClsStateMaster lsState = new ClsStateMaster();
            ViewBag.StateList = new SelectList(lsState.StateMaster(), "state_id", "state_name");
            ClsCityMaster lsCity = new ClsCityMaster();
            ViewBag.CityList = new SelectList(lsCity.Citymaster(), "city_id", "city_name");
            ClsPartyType lsPY = new ClsPartyType();
            ViewBag.PartyType = new SelectList(lsPY.PartyTypeCategory("Supplier"), "type_id", "type_name");

            if (PM.party_id != 0)
            {
                PM.Mode = 1; 
                PM.party_category = "S";
                PM.last_edited_by = User.Identity.Name;
                PM.last_edited_date = DateTime.Now;

                //Insert Table
                ClsPartyMaster lsParty = new ClsPartyMaster();
                lsParty.InsertUpdate(PM);
                PM.CustomerList = lsParty.FillByCategoryPartyMaster("S");
                ViewBag.Message = "Detail Save Sucessfully";
            }            
            return View(PM);
        }       

        [HttpPost]
        public ActionResult Edit(PartyMasterModel PM)
        {
            ClsCountryMaster lsCountry = new ClsCountryMaster();
            ViewBag.CountryList = new SelectList(lsCountry.CountryMaster(), "country_id", "country_name");
            ClsStateMaster lsState = new ClsStateMaster();
            ViewBag.StateList = new SelectList(lsState.StateMaster(), "state_id", "state_name");
            ClsCityMaster lsCity = new ClsCityMaster();
            ViewBag.CityList = new SelectList(lsCity.Citymaster(), "city_id", "city_name");
            ClsPartyType lsPY = new ClsPartyType();
            ViewBag.PartyType = new SelectList(lsPY.PartyTypeCategory("Supplier"), "type_id", "type_name");

            if (PM.party_id != 0)
            {                
                PM.Mode = 2;                
                PM.party_category = "S";
                PM.last_edited_by = User.Identity.Name;
                PM.last_edited_date = DateTime.Now;

                //Update Table
                ClsPartyMaster lsParty = new ClsPartyMaster();
                lsParty.InsertUpdate(PM);
                PM.CustomerList = lsParty.FillByCategoryPartyMaster("S");
                ViewBag.Message = "Detail Save Sucessfully";
            }           
            return View("Create", PM);
        }              

        public ActionResult CountryPin(string CountryID, string country)
        {
            string CountryPin = "*";
            if (CountryID != null && !string.IsNullOrEmpty(CountryID))
            {
                ClsCountryMaster lsParty = new ClsCountryMaster();
                CountryPin = lsParty.CountryPin(Convert.ToInt32(CountryID));
            }                       

            try
            {
                return Json(new
                {
                    msg = CountryPin
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult StateCode(string StateID, string State)
        {
            string StateCode = "*";
            if (StateID != null && !string.IsNullOrEmpty(StateID))
            {
                ClsStateMaster lsParty = new ClsStateMaster();
                StateCode = lsParty.StateCode(Convert.ToInt32(StateID));
            }                       

            try
            {
                return Json(new
                {
                    msg = StateCode
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public JsonResult VerifyGST(string gstNumber)
        {
            string apiURL = "https://commonapi.mastersindia.co/commonapis/searchgstin";
            string authorizationToken = "25f507b9bccee2907617bef6ba884c4306698adf";
            string clientID = "aBQACnQatzPQwoSTwg";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorizationToken}");
                client.DefaultRequestHeaders.Add("Client-ID", clientID);

                try
                {
                    HttpResponseMessage response = client.GetAsync($"{apiURL}?gstin={gstNumber}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, error = "Failed to verify GST number." }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}