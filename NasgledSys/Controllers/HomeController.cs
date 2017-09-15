using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.Web.Security;
using System.Web.Mail;

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
                    GlobalClass.MasterSession = true;
                    GlobalClass.LoginUser = obj;
                    GlobalClass.Company = db.Company.SingleOrDefault(m => m.CompanyKey == obj.CompanyKey);                 
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Login"));
            }
        }
        public ActionResult Register()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            RegisterViewModel model = new RegisterViewModel();
           
            return View(model);
        }
        public ActionResult ForgotPassword()
        {          

            return View();
        }
      
        public ActionResult PasswordRecovery(string Username)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                try
                {
                   
                    UserProfile member = db.UserProfile.FirstOrDefault(m=>m.Username==Username);
                    if (member == null)
                    {
                        result.Data = "Invalid Username.";
                      
                    }
                    else
                    {
                        try
                        {
                            DefaultEmail email = db.DefaultEmail.FirstOrDefault();
                            string HostIP = email.SMTPServer.ToString();
                            string From = email.EmailAddress.ToString();
                            int PortNumber = int.Parse(email.SMTPPort);
                            string smtpUserName = email.SMTPUsername.ToString();
                            string smtpPassword = email.SMTPPassword.ToString();
                            string SSL = email.IsSMTPssl.ToString();


                            MailMessage Mail = new MailMessage();
                            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"] = HostIP;
                            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = 2;

                            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = PortNumber;

                            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpusessl"] = SSL;

                            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;

                            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = smtpUserName;
                            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = smtpPassword;
                            Mail.Body = "Sir/Madam" + "<br/><br/>" + email.SenderName + "<br/>" + email.Detail+"<br/>Password : "+member.Password;
                            Mail.BodyFormat = MailFormat.Html;

                            Mail.To = member.Email;
                            Mail.From = From;
                            Mail.Subject = "Password Recovery for Nasgled login profile";


                            SmtpMail.SmtpServer = HostIP;
                            SmtpMail.Send(Mail);
                            string ThisMessge = "Email Sent";
                            result.Data = "Please Check your email";
                        }
                        catch(Exception ex)
                        {
                            result.Data = ex.Message.ToString();
                        }

                       
                    }
                       
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }
                catch (Exception ex)
                {
                    result.Data= "Unable to send email due to" + ex.Message.ToString();
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return result;
                }

            }
            else
            {
                return result;
            }
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserProfile profile = new UserProfile();
                    profile.ProfileKey = Guid.NewGuid();
                    profile.FirstName = "-";
                    profile.LastName = "-";
                    profile.CompanyName = "-";
                    profile.JobTitle = "-";
                    profile.Username = model.Username;
                    profile.Password = model.Password;
                    profile.Email = model.Email;
                    profile.UserStatus = true;
                    profile.RoleKey = (from x in db.UserRole where x.IsDelete == false && x.Rlevel==1 select x).OrderByDescending(m => m.Rlevel).FirstOrDefault().RoleKey;
                    db.UserProfile.Add(profile);db.SaveChanges();
                    GlobalClass.MasterSession = true;
                    GlobalClass.ProfileUser = profile;

                    
                    return RedirectToAction("Index", "MgtProfile");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Unable to Create Profile due to"+ex.Message.ToString());
                    return View(model);
                }

            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Userhome()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    GlobalClass.AreaGuidForSubArea = Guid.Empty;
                    GlobalClass.AreaHeading = null;
                    return View();
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult ProjectByCompanyReroute()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    return RedirectToAction("ProjectByCompany",new { id=GlobalClass.CCompany.ClientCompanyKey});
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult ProjectByCompany(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    GlobalClass.CCompany = db.ClientCompany.SingleOrDefault(m=>m.ClientCompanyKey==id);
                    return View();
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public JsonResult GetProjectListData()
        {
            var list = (from cc in db.Project where cc.ProfileKey==GlobalClass.ProfileUser.ProfileKey && cc.IsDelete==false
                        select new ProjectThumbnailClass
                        {
                           ProjectName= cc.ProjectName,
                           ProjectKey= cc.ProjectKey,
                           ProjectStatus=cc.ProjectStatus.TypeName,
                           CompanyName= cc.ClientCompany.CompanyName,
                           AdminName=cc.UserProfile.FirstName+" "+ cc.UserProfile.LastName,
                           AreaNum=db.Area.Where(m=>m.ProjectKey==cc.ProjectKey).Count(),
                           ExsistingProduct= db.AreaProduct.Where(m => m.ProjectKey == cc.ProjectKey).Select(m=>m.Count).DefaultIfEmpty(0).Sum()
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectListDataByCompany()
        {
            var list = (from cc in db.Project
                        where cc.ProfileKey == GlobalClass.ProfileUser.ProfileKey && cc.CompanyKey==GlobalClass.CCompany.ClientCompanyKey
                        && cc.IsDelete == false
                        select new ProjectThumbnailClass
                        {
                            ProjectName = cc.ProjectName,
                            ProjectKey = cc.ProjectKey,
                            ProjectStatus = cc.ProjectStatus.TypeName,
                            CompanyName = cc.ClientCompany.CompanyName,
                            AdminName = cc.UserProfile.FirstName + " " + cc.UserProfile.LastName,
                            AreaNum = db.Area.Where(m => m.ProjectKey == cc.ProjectKey).Count(),
                            ExsistingProduct = db.AreaProduct.Where(m => m.ProjectKey == cc.ProjectKey).Select(m => m.Count).DefaultIfEmpty(0).Sum()
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserLogin()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            LoginViewModel model = new LoginViewModel();
            if (Request.Cookies["PLogin"] != null)
            {
                model.Username = Request.Cookies["PLogin"].Values["Username"];
                model.Password = Request.Cookies["PLogin"].Values["Password"];
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult UserLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Session["GlobalMessege"] = "";
                    string temp = "Successfully Logged in";
                    Exception e = new Exception();
                    UserProfile obj = db.UserProfile.SingleOrDefault(m => m.Username == model.Username && m.Password == model.Password);
                    if (obj == null)
                    {
                         e = new Exception("Incorrect user access. Unauthorized Access.");
                        temp = e.Message.ToString();
                    }
                    else
                    {
                        if (model.RememberMe)
                        {
                            HttpCookie cookie = new HttpCookie("PLogin");
                            cookie.Values.Add("Username", model.Username);
                            cookie.Values.Add("Password", model.Password);
                            cookie.Expires = DateTime.Now.AddDays(715);
                            Response.Cookies.Add(cookie);

                        }
                        GlobalClass.MasterSession = true;
                        GlobalClass.LoggedInUser = obj;
                        if(obj.UserRole.Rlevel == 1)GlobalClass.ProfileUser=obj ;
                        else
                        {
                            ClientCompanyProfileRequest probj = db.ClientCompanyProfileRequest.SingleOrDefault(m=>m.ProfileKey==obj.ProfileKey&& m.IsConfirmed==true && m.IsCancelled==false);
                            if (probj == null)
                            {
                                e = new Exception("Incorrect user access. Unauthorized Access.");
                                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
                            }
                            else
                            {
                                GlobalClass.ProfileUser = db.UserProfile.SingleOrDefault(m=>m.ProfileKey==probj.AdminProfileKey);
                            }
                        }
                        
                        return RedirectToAction("Userhome", "Home");
                    }
                    return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
                }
                catch (Exception e)
                {
                    return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
                }
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Index()
        {
            if (GlobalClass.MasterSession)
            {
                return View();
            }
            else
            {
                Exception e = new Exception("Sorry, your Session has Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "Login"));
            }
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}