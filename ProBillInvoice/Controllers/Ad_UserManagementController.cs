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
    public class Ad_UserManagementController : Controller
    {
        // GET: UserManagement        
        public ActionResult Index()
        {
            ClsUserManagement lsUM = new ClsUserManagement();
            return View(lsUM.UserManagement());
        }

        public ActionResult Create()
        {
            ClsRoleMaster RM = new ClsRoleMaster();
            ViewBag.RoleList = new SelectList(RM.AspRoleList(), "ROLE_ID", "ROLE_NAME");

            ClsDeptMaster DM = new ClsDeptMaster();
            ViewBag.DeptList = new SelectList(DM.DepartmentMaster(), "dept_id", "dept_name");

            Ad_UserManagementModel UserManagement = new Ad_UserManagementModel();
            ClsUserManagement lsUM = new ClsUserManagement();
            UserManagement.userId = lsUM.NextId();
            return View(UserManagement);
        }

        public ActionResult Edit(string id)
        {
            ClsRoleMaster RM = new ClsRoleMaster();
            ViewBag.RoleList = new SelectList(RM.AspRoleList(), "ROLE_ID", "ROLE_NAME");

            ClsDeptMaster DM = new ClsDeptMaster();
            ViewBag.DeptList = new SelectList(DM.DepartmentMaster(), "dept_id", "dept_name");

            ClsUserManagement lsUM = new ClsUserManagement();
            Ad_UserManagementModel UserManagement = lsUM.UserManagement(id);

            //if (UserManagement.role_id != 0)
            //{
            //    ViewBag.RoleList = new SelectList(RM.RoleMaster(), "ROLE_ID", "ROLE_NAME", UserManagement.role_id);
            //}

            if (UserManagement.dept_id != 0)
            {
                ViewBag.DeptList = new SelectList(DM.DepartmentMaster(), "dept_id", "dept_name", UserManagement.dept_id);
            }

            return View(UserManagement);
        }

        public ActionResult Details(string id)
        {
            ClsUserManagement lsUM = new ClsUserManagement();
            Ad_UserManagementModel UserManagement = lsUM.UserManagement(id);
            return View(UserManagement);
        }

        [HttpPost]
        public ActionResult Create(Ad_UserManagementModel User)
        {
            string finacialYear = (string)Session["FinacialYear"];
            if (User.userId != 0)
            {
                int userId = User.userId;
                string AspNetUserId = User.AspNetUserId;
                string user_name = User.user_name;
                string pass_word = User.pass_word;
                string pass_word_R1 = User.pass_word_R1;
                bool admin_user = User.admin_user;
                bool primary_user = User.primary_user;
                string AspNetRoleId = User.AspNetRoleId;
                string user_fname = User.user_fname;
                string user_lname = User.user_lname;
                string job_title = User.job_title;
                int dept_id = User.dept_id;
                string phone_no = User.phone_no;
                string email = User.email;
                bool mobile_alerts = User.mobile_alerts;
                bool email_alerts = User.email_alerts;
                bool IsActive = User.IsActive;
                DateTime CreateDate = DateTime.Now;
                DateTime InactiveDate = DateTime.Now;
                DateTime LastLoginDate = DateTime.Now;
                string LoginIpAddress = User.LoginIpAddress;
                bool IsLoggedIn = User.IsLoggedIn;
                string default_fin_year = finacialYear;

                ClsUserManagement lsUser = new ClsUserManagement();
                lsUser.Insert(AspNetUserId, user_name, pass_word, pass_word_R1, admin_user, primary_user, AspNetRoleId, user_fname, user_lname, job_title, dept_id, phone_no, email, mobile_alerts, email_alerts, IsActive, CreateDate, InactiveDate, LastLoginDate, LoginIpAddress, IsLoggedIn, default_fin_year);
                ViewBag.Message = "Detail save successfully";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Ad_UserManagementModel User)
        {
            if (User.userId != 0)
            {
                int userId = User.userId;
                string user_name = User.user_name;
                string pass_word = User.pass_word;
                string pass_word_R1 = User.pass_word_R1;
                bool admin_user = User.admin_user;
                bool primary_user = User.primary_user;
                string AspNetRoleId = User.AspNetRoleId;
                string user_fname = User.user_fname;
                string user_lname = User.user_lname;
                string job_title = User.job_title;
                int dept_id = User.dept_id;
                string phone_no = User.phone_no;
                string email = User.email;
                bool mobile_alerts = User.mobile_alerts;
                bool email_alerts = User.email_alerts;
                bool IsActive = User.IsActive;
                DateTime CreateDate = DateTime.Now;
                DateTime InactiveDate = DateTime.Now;
                DateTime LastLoginDate = DateTime.Now;
                string LoginIpAddress = User.LoginIpAddress;
                bool IsLoggedIn = User.IsLoggedIn;
                string default_fin_year = User.default_fin_year;

                ClsUserManagement lsUser = new ClsUserManagement();
                lsUser.Update(userId, user_name, pass_word, pass_word_R1, admin_user, primary_user, AspNetRoleId, user_fname, user_lname, job_title, dept_id, phone_no, email, mobile_alerts, email_alerts, IsActive, CreateDate, InactiveDate, LastLoginDate, LoginIpAddress, IsLoggedIn, default_fin_year);
                ViewBag.Message = "Detail save successfully";
            }

            return RedirectToAction("Index");
        }
    }
}