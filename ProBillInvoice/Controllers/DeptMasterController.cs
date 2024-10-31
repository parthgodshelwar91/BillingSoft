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
    public class DeptMasterController : Controller
    {        
        public ActionResult Create()
        {
            ViewBag.Titlename = "Create";
            DepartmentMasterModel Department = new DepartmentMasterModel();
            ClsDeptMaster lsDepartment = new ClsDeptMaster();
            Department.dept_id = lsDepartment.NextId();
            Department.DepartmentList = lsDepartment.DepartmentMaster();            
            return View(Department);            
        }
        
        public ActionResult Edit(int id)
        {
            ViewBag.Titlename = "Edit";
            ClsDeptMaster lsDepartment = new ClsDeptMaster();
            DepartmentMasterModel Department = lsDepartment.DepartmentMaster(id);
            Department.DepartmentList = lsDepartment.DepartmentMaster();            
            return View("Create", Department);
        }
        
        public ActionResult Details(int id)
        {
            ClsDeptMaster lsDepartment = new ClsDeptMaster();
            DepartmentMasterModel Department = lsDepartment.DepartmentMaster(Convert.ToInt32(id));
            return View(Department);
        }

        [HttpPost]
        public ActionResult Create(DepartmentMasterModel Department)
        {
            ClsDeptMaster lsDepartment = new ClsDeptMaster();
            if (Department.dept_id != 0)
            {
                int dept_id = Department.dept_id;
                string dept_name = Department.dept_name;
                bool defunct = Department.defunct;

                lsDepartment.InsertUpdate(1, dept_id,dept_name, defunct);
                ViewBag.Message = "Detail Save Sucessfully";
            }
            Department.DepartmentList = lsDepartment.DepartmentMaster();
            ViewBag.Titlename = "Create";
            return View(Department);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentMasterModel Department)
        {
            ClsDeptMaster lsDepartment = new ClsDeptMaster();
            if (Department.dept_id != 0)
            {
                int dept_id = Department.dept_id;
                string dept_name = Department.dept_name;
                bool defunct = Department.defunct;

                lsDepartment.InsertUpdate(2,dept_id, dept_name, defunct);
                ViewBag.Message = "Detail Save Sucessfully";
            }
            Department.DepartmentList = lsDepartment.DepartmentMaster();
            ViewBag.Titlename = "Edit";
            return View("Create", Department);
        }
    }
}