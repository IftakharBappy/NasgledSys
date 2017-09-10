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
namespace NasgledSys.Controllers
{
    public class MgtProjectController : Controller
    {
        // GET: MgtProject
        private NasgledDBEntities db = new NasgledDBEntities();
        private FormValidation validate = new FormValidation();
        public ActionResult Index()
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
        public ActionResult CIndex()
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
        public ActionResult SelectCompany()
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
        public ActionResult GetCompanyForProject(Guid id)
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
        public ActionResult Create(Guid? id)
        {
            try
            {
                Session["GlobalMessege"] = "";
                ViewBag.mess = "";
                return View();
            }
            catch (Exception e)
            {

                return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
            }
        }
    }
}