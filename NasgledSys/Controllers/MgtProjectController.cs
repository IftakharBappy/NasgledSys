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
using NasgledSys.DAL;

namespace NasgledSys.Controllers
{
    public class MgtProjectController : Controller
    {
        // GET: MgtProject
        private NasgledDBEntities db = new NasgledDBEntities();
        private FormValidation validate = new FormValidation();
        private ManageProject manage = new ManageProject();
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
        public ActionResult DeleteProject(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Project p = db.Project.Find(id);
                    p.IsDelete = true;
                    db.SaveChanges();
                    Session["GlobalMessege"] = "Project has been DELETED successfully.";
                    return RedirectToAction("Userhome","Home");
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
                        //ViewBag.ExpectedClosingMonth = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem()
                        //{
                        //    Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")",
                        //    Value = x.ToString()

                        //}), "Value", "Text");
                        //ViewBag.EspectedClosingYear = new SelectList(Enumerable.Range(DateTime.Today.Year, 50).Select(x => new SelectListItem()
                        //{
                        //    Text = x.ToString(),
                        //    Value = x.ToString()
                        //}), "Value", "Text");
                        model.PreparedByDetail = GlobalClass.LoggedInUser.FirstName + " " + GlobalClass.LoggedInUser.LastName + " " + GlobalClass.LoggedInUser.Email;
                        model.PreparedBy = GlobalClass.LoggedInUser.ProfileKey;
                        Session["GlobalMessege"] = "";
                        ViewBag.mess = "";
                        
                    }
                    else
                    {
                        Project project = db.Project.Find(id);
                        GlobalClass.Project = project;
                        GlobalClass.CCompany = db.ClientCompany.Find(project.CompanyKey);                  
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
        [HttpPost]
        public ActionResult Create(ProjectClass model)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (model.CityKey == null) model.CityKey = 1;
                    if (model.StateKey == null) model.StateKey = 1;
                    if (ModelState.IsValid)
                    {
                        if (model.ProjectKey == null || model.ProjectKey == Guid.Empty)
                        {
                            DataReturn dt = manage.SaveNewProject(model);
                            Session["GlobalMessege"] = dt.mess;
                            if (dt.flag == 1)
                            {
                                return RedirectToAction("Created");
                            }
                           
                        }
                        else
                        {
                            DataReturn dt = manage.UpdateProject(model);
                            Session["GlobalMessege"] = dt.mess;
                            if (dt.flag == 1)
                            {
                                return RedirectToAction("Created");
                            }
                        }                        
                        
                    }
                    ViewBag.ExpectedClosingMonth = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem()
                    {
                        Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")",
                        Value = x.ToString()

                    }), "Value", "Text",model.ExpectedClosingMonth);

                    ViewBag.EspectedClosingYear = new SelectList(Enumerable.Range(DateTime.Today.Year, 50).Select(x => new SelectListItem()
                    {
                        Text = x.ToString(),
                        Value = x.ToString()
                    }), "Value", "Text",model.EspectedClosingYear);
                    model.PreparedByDetail = GlobalClass.LoggedInUser.FirstName + " " + GlobalClass.LoggedInUser.LastName + " " + GlobalClass.LoggedInUser.Email;
                    model.PreparedBy = GlobalClass.LoggedInUser.ProfileKey;
                    Session["GlobalMessege"] = "";
                    ViewBag.mess = "";
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


        public ActionResult Created()
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
        [HttpPost]
        public async Task<ActionResult> SaveNewContact(string FirstName,string LastName,string Email)
        {
            try
            {
                ClientContact contact = new ClientContact();
                contact.ContactKey = Guid.NewGuid();
                contact.FirstName = FirstName;
                if (string.IsNullOrEmpty(LastName)) contact.LastName = "--";
                else contact.LastName = LastName;
                if (string.IsNullOrEmpty(Email)) contact.Email = "a@email.com";
                else contact.Email = Email;
                contact.OfficePhone = "(000) 000-0000";
                contact.Address = "n/a";
                contact.Address2 = "n/a";
                contact.JobTitle = "n/a";
                contact.WebAddress = "n/a";
                contact.CellPhone = "n/a";
                contact.FaxPhone = "n/a";
                contact.Zipcode = "n/a";
                contact.InternalNote = "n/a";
                contact.GeneralNote = "n/a";
                contact.IsActive = true;
                contact.IsDefault = true;
                contact.ProfileKey = GlobalClass.ProfileUser.ProfileKey;
                contact.CityKey = 1;
                contact.StateKey = 1;
                contact.Address = "n/a";
                contact.Address = "n/a";

                db.ClientContact.Add(contact);
                var task = db.SaveChangesAsync();
                await task;
                return Content("success");
            }
            catch(Exception ex)
            {
                return Content(ex.Message.ToString());
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