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
    public class RecipeMasterController : Controller
    {
        // GET: RecipeMaster

        //public ActionResult Index()
        //{
        //    RecipeMasterModel RM = new RecipeMasterModel();
        //    ClsRecipeHeader clsRecipe = new ClsRecipeHeader();
        //    RM.RecipeHeaderList = clsRecipe.RecipeMaster();
        //    return View(RM);
        //}
        public ActionResult Create()
        {
            int psSiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsPartyMaster clsParty = new ClsPartyMaster();
            ViewBag.CustomerList = new SelectList(clsParty.PartyMaster_Categorywise("C"), "party_id", "party_name");
            //ClsMaterialMaster clsMaterial = new ClsMaterialMaster();
            //ViewBag.MaterialList = new SelectList(clsMaterial.MaterialMaster_Type("Purchase"), "material_id", "material_name");
            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            ViewBag.MaterialList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + psSiteId + " AND material_mst.group_code in('01') AND material_mst.material_type = 'Purchase' "), "material_id", "material_name");


            RecipeMasterModel mymodel = new RecipeMasterModel();
            ClsRecipeHeader lsHeader = new ClsRecipeHeader();
            ClsRecipeDetail lsDetail = new ClsRecipeDetail();
            mymodel.RecipeHeader= lsHeader.RecipeMaster(0);
            //mymodel.RecipeDetails= lsDetail.RecipeDetail(0);
            mymodel.RecipeDetails = new List<RecipeDetailModel>();
            mymodel.RecipeDetails.Add(new RecipeDetailModel { recipe_detail_id = 0, recipe_id = 0, material_id = 0, material_recipe_name = " ", quantity = Convert.ToDecimal("0.00") });

            mymodel.RecipeHeader.recipe_id = lsHeader.NextId();
            mymodel.RecipeHeader.recipe_no = lsHeader.NextNo();
            mymodel.RecipeHeader.party_id = lsHeader.PartyName(); 

            //Session["HeaderTable"] = mymodel.RecipeHeader;
            //Session["DetailTable"] = mymodel.RecipeDetails;
            mymodel.RecipeHeaderList = lsHeader.RecipeMaster();
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Create(RecipeMasterModel RH, FormCollection form, string btnAdd,string btndeleteGD)
        {
            int psSiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsPartyMaster clsParty = new ClsPartyMaster();
            ViewBag.CustomerList = new SelectList(clsParty.PartyMaster_Categorywise("C"), "party_id", "party_name");
            //ClsMaterialMaster clsMaterial = new ClsMaterialMaster();
            //ViewBag.MaterialList = new SelectList(clsMaterial.MaterialMaster_Type("Purchase"), "material_id", "material_name");
            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            ViewBag.MaterialList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + psSiteId + " AND material_mst.group_code in('01') AND material_mst.material_type = 'Purchase' "), "material_id", "material_name");

            ClsRecipeHeader lsHeader = new ClsRecipeHeader();
            RecipeMasterModel mymodel = new RecipeMasterModel();

            switch (btndeleteGD)
            {
                case string number when int.TryParse(number, out var index):
                    if (RH.RecipeDetails.Count > 0)
                    {
                        RH.RecipeDetails.RemoveAt(index);
                    }
                    ModelState.Clear();                    
                    mymodel.RecipeHeader = RH.RecipeHeader;
                    mymodel.RecipeDetails = RH.RecipeDetails;
                    mymodel.RecipeHeaderList = lsHeader.RecipeMaster();
                    break;
            }

            if (btnAdd == "Add")
            {
                List<RecipeDetailModel> DetailTable = new List<RecipeDetailModel>();
                if (RH.RecipeDetails != null)
                {
                    foreach (var row in RH.RecipeDetails)
                    {
                        RecipeDetailModel tRow = new RecipeDetailModel();
                        tRow.recipe_detail_id = row.recipe_detail_id;
                        tRow.recipe_id = row.recipe_id;
                        tRow.material_id = row.material_id;
                        tRow.material_recipe_name = row.material_recipe_name;
                        tRow.quantity = row.quantity;                      
                        DetailTable.Add(tRow);
                    }
                }
                DetailTable.Add(new RecipeDetailModel { recipe_detail_id = 0, recipe_id = 0, material_id = 0, material_recipe_name = " ", quantity = Convert.ToDecimal("0.00") });
             
                mymodel.RecipeHeader = RH.RecipeHeader;
                mymodel.RecipeDetails = DetailTable;
                mymodel.RecipeHeaderList = lsHeader.RecipeMaster();

            }            
            else if (btnAdd == "Save")
            {
                string lsUsername = Convert.ToString(System.Web.HttpContext.Current.Session["LoginUserName"]);
                RecipeHeaderModel HeaderTable = RH.RecipeHeader;
                List<RecipeDetailModel> DetailTable = RH.RecipeDetails;
                //HeaderTable
                
                HeaderTable.recipe_id = lsHeader.NextId();
                HeaderTable.recipe_no = lsHeader.NextNo();                

                if (HeaderTable.recipe_id != 0)
                {
                    HeaderTable.Mode = 1;                                      
                   
                    //insert table
                    int lirecipe_id = lsHeader.InsertUpdate(HeaderTable);

                    //DetailTable
                    if (DetailTable.Count > 0)
                    {
                        foreach (RecipeDetailModel dRow in DetailTable)
                        {
                            dRow.Mode = 1;
                            dRow.rate = Convert.ToDecimal("0.00");
                            dRow.recipe_id = HeaderTable.recipe_id;
                            ClsRecipeDetail lsDetail = new ClsRecipeDetail();
                            lsDetail.InsertUpdate(dRow);
                        }
                    }

                    ViewBag.Message = "Detail save successfully";
                }
                mymodel.RecipeHeaderList = lsHeader.RecipeMaster();
                mymodel.RecipeDetails = DetailTable;
                mymodel.RecipeHeaderList = lsHeader.RecipeMaster();
            }
                       
            return View(mymodel);
        }

        public ActionResult Edit(int id)
        {
            int psSiteId = Convert.ToInt32(Session["LoginSiteId"].ToString());

            ClsPartyMaster clsParty = new ClsPartyMaster();
            ViewBag.CustomerList = new SelectList(clsParty.PartyMaster_Categorywise("C"), "party_id", "party_name");
            //ClsMaterialMaster clsMaterial = new ClsMaterialMaster();
            //ViewBag.MaterialList = new SelectList(clsMaterial.MaterialMaster_Type("Purchase"), "material_id", "material_name");
            ClsStStockHeader lsStStockHeader = new ClsStStockHeader();
            ViewBag.MaterialList = new SelectList(lsStStockHeader.StockPosting_Material("st_stock_header.site_id = " + psSiteId + " AND material_mst.group_code in('01') AND material_mst.material_type = 'Purchase' "), "material_id", "material_name");


            RecipeMasterModel mymodel = new RecipeMasterModel();
            ClsRecipeHeader lsHeader = new ClsRecipeHeader();
            ClsRecipeDetail lsDetail = new ClsRecipeDetail();
            mymodel.RecipeHeader = lsHeader.RecipeMaster(id);
            mymodel.RecipeDetails = lsDetail.RecipeDetail(id);
           
            mymodel.RecipeHeaderList = lsHeader.RecipeMaster();
            //Session["HeaderTable"] = mymodel.RecipeHeader;
            //Session["DetailTable"] = mymodel.RecipeDetails;
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Edit(RecipeMasterModel RH, FormCollection form, string btnAdd, string btndeleteGD)
        {            
            ClsPartyMaster clsParty = new ClsPartyMaster();
            ViewBag.CustomerList = new SelectList(clsParty.PartyMaster(), "party_id", "party_name");
            ClsMaterialMaster clsMaterial = new ClsMaterialMaster();
            ViewBag.MaterialList = new SelectList(clsMaterial.MaterialMaster_Type("Purchase"), "material_id", "material_name");
            ClsRecipeHeader lsHeader = new ClsRecipeHeader();

            RecipeMasterModel mymodel = new RecipeMasterModel();

            switch (btndeleteGD)
            {
                case string number when int.TryParse(number, out var index):
                    if (RH.RecipeDetails.Count > 0)
                    {
                        RH.RecipeDetails.RemoveAt(index);
                    }
                    ModelState.Clear();
                    mymodel.RecipeHeader = RH.RecipeHeader;
                    mymodel.RecipeDetails = RH.RecipeDetails;
                    mymodel.RecipeHeaderList = lsHeader.RecipeMaster();
                    break;
            }

            if (btnAdd == "Add")
            {
                List<RecipeDetailModel> DetailTable = new List<RecipeDetailModel>();
                if (RH.RecipeDetails != null)
                {
                    foreach (var row in RH.RecipeDetails)
                    {
                        RecipeDetailModel tRow = new RecipeDetailModel();
                        tRow.recipe_detail_id = row.recipe_detail_id;
                        tRow.recipe_id = row.recipe_id;
                        tRow.material_id = row.material_id;
                        tRow.material_recipe_name = row.material_recipe_name;
                        tRow.quantity = row.quantity;
                        DetailTable.Add(tRow);
                    }
                }
                DetailTable.Add(new RecipeDetailModel { recipe_detail_id = 0, recipe_id = 0, material_id = 0, material_recipe_name = " ", quantity = Convert.ToDecimal("0.00") });

                mymodel.RecipeHeader = RH.RecipeHeader;
                mymodel.RecipeDetails = DetailTable;
                mymodel.RecipeHeaderList = lsHeader.RecipeMaster();

                
            }

            else if (btnAdd == "Save")
            {
                string lsUsername = Convert.ToString(System.Web.HttpContext.Current.Session["LoginUserName"]);

                RecipeHeaderModel HeaderTable = RH.RecipeHeader;
                List<RecipeDetailModel> DetailTable = RH.RecipeDetails;
                //HeaderTable
              
                if (HeaderTable.recipe_id != 0)
                {
                    HeaderTable.Mode = 2;
                                     
                    //insert table
                    int lirecipe_id = lsHeader.InsertUpdate(HeaderTable);

                    //DetailTable
                    if (DetailTable.Count > 0)
                    {
                        foreach (RecipeDetailModel dRow in DetailTable)
                        {
                            dRow.Mode = 2;
                            dRow.rate = Convert.ToDecimal("0.00");
                            
                            ClsRecipeDetail lsDetail = new ClsRecipeDetail();
                            lsDetail.InsertUpdate(dRow);                            
                        }
                    }

                    ViewBag.Message = "Detail save successfully";
                }
                mymodel.RecipeHeaderList = lsHeader.RecipeMaster();
                mymodel.RecipeDetails = DetailTable;
            }
            return View(mymodel);
        }

       

        public ActionResult RecipeName(int id, string name)
        {
            ClsMaterialMaster lsMaterial = new ClsMaterialMaster();
            string lsMId = lsMaterial.MaterialMasterList(id);

            try
            {
                return Json(new
                {
                    msg = lsMId
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}