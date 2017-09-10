using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.Web.Security;
using System.Web.Mail;
using System.Threading.Tasks;
using NasgledSys.EM;
using NasgledSys.Validation;
using System.Globalization;
using NasgledSys.EM;

namespace NasgledSys.Controllers
{
    public class MgtProjectController : Controller
    {
        // GET: MgtProject
        private NasgledDBEntities db = new NasgledDBEntities();
        private FormValidation validate = new FormValidation();
        public ActionResult Index()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ViewBag.mess = "";
                    return View();
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult CIndex()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ViewBag.mess = "";
                    return View();
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult SelectCompany()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {

                    return View();
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult GetCompanyForProject(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    GlobalClass.CCompany = db.ClientCompany.SingleOrDefault(m => m.ClientCompanyKey == id);
                    return RedirectToAction("Create","MgtProject");
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult Create(Guid? id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ProjectClass model = new ProjectClass();
                    if (id == null || id == Guid.Empty)
                    {
                        model.CityKey = -1;
                        model.StateKey= - 1;
                        model.PrimaryContactKey = Guid.Empty;
                        model.ProjectStatusKey = Guid.Empty;
                        ViewBag.ExpectedClosingMonth = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem()
                        {
                            Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")",
                            Value = x.ToString()

                        }), "Value", "Text");

                        ViewBag.EspectedClosingYear = new SelectList(Enumerable.Range(DateTime.Today.Year, 50).Select(x => new SelectListItem()
                        {
                            Text = x.ToString(),
                            Value = x.ToString()
                        }), "Value", "Text");
                        model.PreparedByDetail = GlobalClass.LoggedInUser.FirstName + " " + GlobalClass.LoggedInUser.LastName + " " + GlobalClass.LoggedInUser.Email;
                        model.PreparedBy = GlobalClass.LoggedInUser.ProfileKey;
                        Session["GlobalMessege"] = "";
                        ViewBag.mess = "";
                        
                    }
                    else
                    {
                        Project project = db.Project.Find(id);
                        model = EM_Project.ConvertToModel(project);
                    }
                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
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