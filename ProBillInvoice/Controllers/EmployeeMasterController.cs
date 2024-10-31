using ProBillInvoice.DAL;
using ProBillInvoice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProBillInvoice.Controllers
{
    public class EmployeeMasterController : Controller
    {
        // GET: EmployeeMaster
        public ActionResult Index()
        {
            ClsEmployeeMaster lsEmployee = new ClsEmployeeMaster();
            return View(lsEmployee.EmployeeMaster_Categorywise());
        }

        public ActionResult Create()
        {
            ClsDeptMaster lsDepartment = new ClsDeptMaster();
            ViewBag.Department = new SelectList(lsDepartment.DepartmentMaster(), "dept_id", "dept_name");
            EmployeeMasterModel model = new EmployeeMasterModel();
            ClsEmployeeMaster lsEmployee = new ClsEmployeeMaster();
            model.emp_id=lsEmployee.NextId();          
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeMasterModel model, HttpPostedFileBase image_path)
        {          
            if (image_path != null)
            {
                var file = image_path;
                var filename = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("~/ImageUpload/" + filename));
            }
            ClsEmployeeMaster lsEmployee = new ClsEmployeeMaster();
            if(model.emp_id !=0)
            { 
            model.Mode = 1;
            model.emp_id = 1;
            model.acct_id = 1;
            model.age = 24;
            model.last_edited_by = "ADMIN";
            model.last_edited_date = DateTime.Now;
            model.site_id = 1;
            model.image_path = image_path.FileName;
            lsEmployee.InsertUpdate(model);
             ViewBag.Message = "Detail Save Sucessfully";
            }
            ModelState.Clear();
            ClsDeptMaster lsDepartment = new ClsDeptMaster();
            ViewBag.Department = new SelectList(lsDepartment.DepartmentMaster(), "dept_id", "dept_name");
            return View();
        }

        [HttpPost]
        public ActionResult Search(EmployeeMasterModel model)
        {
            
            ClsTicketsRecipeSummary lsTicketsRecipe = new ClsTicketsRecipeSummary();
            List<TicketRecipeSummeryModel> list = lsTicketsRecipe.TicketRecipeSummary();
            //foreach(TicketRecipeSummeryModel trow in list)
            //{
              
            //    for (int i=1; i<=trow.mcub;i++)
            //    {

            //    }
            //}
            ClsDeptMaster lsDepartment = new ClsDeptMaster();
            ViewBag.Department = new SelectList(lsDepartment.DepartmentMaster(), "dept_id", "dept_name");
            return View(model);
        }
     }
}