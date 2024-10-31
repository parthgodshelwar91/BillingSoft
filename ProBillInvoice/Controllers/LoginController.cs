using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using ProBillInvoice.DAL;
using ProBillInvoice.Models;

namespace CrusherManagement.Controllers
{   
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            //ClsStoreMaster lsSM = new ClsStoreMaster();
            //ViewBag.StoreList = new SelectList(lsSM.StoreMaster(), "store_id", "store_name");
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string lsUsername = string.Empty;
                    string lsPassword = string.Empty;

                    DataTable dt, dt_Company, dt_FYear = new DataTable();                    
                    ClsLogin lsLogin = new ClsLogin();
                    dt = lsLogin.GetLogin(login.Username, login.Password);
                    dt_Company = lsLogin.GetCompany(1);
                    dt_FYear = lsLogin.GetFinancialYear();

                    if (dt.Rows.Count > 0)
                    {
                        lsUsername = dt.Rows[0]["user_name"].ToString();
                        lsPassword = dt.Rows[0]["pass_word"].ToString();                                            
                        if (lsUsername.ToUpper() == login.Username.ToUpper() && lsPassword == login.Password.Trim())
                        {
                            Session["LoginUserId"] = (int)dt.Rows[0]["userId"];
                            Session["LoginUserName"] = dt.Rows[0]["user_name"].ToString();
                            Session["LoginRoleId"] = (int)dt.Rows[0]["role_id"];
                            Session["LoginStore"] = (int)dt.Rows[0]["store_id"];
                            Session["DeptId"] = (int)dt.Rows[0]["dept_id"];
                            Session["FinancialYear"] = login.Fyear;

                            Session["CompanyCode"] = dt_Company.Rows[0]["company_code"].ToString();
                            Session["CompanyStateId"] = dt_Company.Rows[0]["state_id"].ToString();
                            Session["LoginSiteId"] = "2";
                            Session["RoleID"] = "2";
                            string strHostName = string.Empty;
                            string strIPAddress = string.Empty;

                            strHostName = Dns.GetHostName();
                            IPAddress[] hosts = Dns.GetHostAddresses(strHostName);
                            foreach (IPAddress address in hosts)
                            {
                                if(address.AddressFamily == AddressFamily.InterNetwork)
                                {
                                    strIPAddress = address.MapToIPv4().ToString();
                                }
                            }
                            ClsUserLoginTracking lsLoginTracking = new ClsUserLoginTracking();
                            int id = 0;
                            string user_email = Session["LoginUserName"].ToString();
                            string ip_address = strIPAddress;
                            DateTime log_in_time = DateTime.Now;
                            DateTime log_out_time = DateTime.Now;
                            string session_id = Session["LoginRoleId"].ToString();
                            lsLoginTracking.Insert(id, user_email, ip_address, log_in_time, log_out_time, session_id);

                            if (lsUsername == "ADMIN")
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else if(lsUsername == "HS")
                            {
                                return RedirectToAction("IndexAdmin", "Home");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Wrong Username or Password !";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Please enter Valid Username and Password.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Please check your network connection or contact your software support or helpdesk. '" + ex.Message.Replace("'", "") + " ";
                }
            }
            return View();
        }


        public void AddCookie_For_API_Validation(int ID)
        {
            string cookieToken;
            string formToken;
            System.Web.Helpers.AntiForgery.GetTokens(null, out cookieToken, out formToken);
            ViewBag.cookieToken = cookieToken;
            ViewBag.formToken = formToken;
            Random rm = new Random();
            var cookie = new HttpCookie("ProBillInvoice");
            cookie.Value = ID + "*" + cookieToken + "*" + formToken + "*" + DateTime.Now.Date.ToShortDateString() + "*" + DateTime.Now.Date.ToShortTimeString();
            Response.Cookies.Add(cookie);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (Request.Cookies["ProBillInvoice"] != null)
            {
                var cookie = new HttpCookie("ProBillInvoice");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }

        [NonAction]
        public void remove_Anonymous_Cookies()
        {
            if (Request.Cookies["ProBillInvoice"] != null)
            {
                var cookie = new HttpCookie("ProBillInvoice");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
        }    
    }
}