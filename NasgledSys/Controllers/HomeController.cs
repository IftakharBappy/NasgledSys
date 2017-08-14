using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.Web.Security;

namespace NasgledSys.Controllers
{
    public class HomeController : Controller
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            LoginViewModel model = new LoginViewModel();
            if (Request.Cookies["UserLogin"] != null)
            {
                model.Username = Request.Cookies["UserLogin"].Values["Username"];
                model.Password = Request.Cookies["UserLogin"].Values["Password"];
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            try
            {
                StaffList obj = db.StaffList.SingleOrDefault(m => m.Username == model.Username && m.Password == model.Password && m.IsDelete == false && m.IsUser == true);
                if (obj == null)
                {
                    Exception e = new Exception("Incorrect user access. Unauthorized Access.");
                    return View("Error", new HandleErrorInfo(e, "Home", "Login"));
                }
                else
                {
                    if (model.RememberMe)
                    {
                        HttpCookie cookie = new HttpCookie("UserLogin");
                        cookie.Values.Add("Username", model.Username);
                        cookie.Values.Add("Password", model.Password);
                        cookie.Expires = DateTime.Now.AddDays(715);
                        Response.Cookies.Add(cookie);

                    }
                    GlobalClass.LoginUser = obj;
                    GlobalClass.Company = db.Company.SingleOrDefault(m => m.CompanyKey == obj.CompanyKey);                 
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (DivideByZeroException e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Login"));
            }
        }
        public ActionResult Index()
        {
            
                return View();
        }
       
        public ActionResult AIndex()
        {
            if (GlobalClass.MasterSession)
            {
                return View();
            }
            else
            {
                Exception e = new Exception("Sorry, your Session has Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "AdminLogin"));
            }
        }
        [AllowAnonymous]
        public ActionResult AdminLogin()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            LoginViewModel model = new LoginViewModel() ;
            if (Request.Cookies["Login"] != null)
            {
                model.Username = Request.Cookies["Login"].Values["Username"];
                model.Password = Request.Cookies["Login"].Values["Password"];
            }
            return View(model);           
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AdminLogin(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {              
                  
                    if (model.Username == "sa" && model.Password=="ntDomain")
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                        Session["Username"] = model.Username;
                        Session["Password"] = model.Password;
                        if (model.RememberMe)
                        {
                            HttpCookie cookie = new HttpCookie("Login");
                            cookie.Values.Add("Username", model.Username);
                            cookie.Values.Add("Password", model.Password);
                            cookie.Expires = DateTime.Now.AddDays(715);
                            Response.Cookies.Add(cookie);

                        }
                    GlobalClass.MasterSession = true;
                        return RedirectToAction("AIndex", "Home");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Login data is incorrect!");
                    }



                
            }
            return View(model);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}