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
    public class MgtClientCompanyController : Controller
    {
        // GET: MgtClientCompany
        private NasgledDBEntities db = new NasgledDBEntities();
        private FormValidation validate = new FormValidation();
        public ActionResult Create()
        {
            try
            {               
                ViewBag.mess = "";
                ViewBag.IndustryTypeKey = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "IndustryKey", "TypeName");
             
                return View();
            }
            catch (Exception e)
            {

                return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
            }
        }
        [HttpPost]
        public ActionResult Create(ClientCompany modelClass)
        {
            ViewBag.mess = "";
           
            if (ModelState.IsValid)
            {

                try
                {
                    modelClass = validate.ValidateClient(modelClass);

                    ClientCompany pf = new ClientCompany();
                    pf.ClientCompanyKey = Guid.NewGuid();
                   
                    pf.CompanyName = modelClass.CompanyName;
                    pf.Description = modelClass.Description;
                    pf.IndustryTypeKey = modelClass.IndustryTypeKey;
                    pf.ProfileKey = modelClass.ProfileKey;
                    db.ClientCompany.Add(pf);
                    db.SaveChanges();
                    GlobalClass.CCompany = pf;
                    Session["GlobalMessege"] = "Company is Created";
                    return RedirectToAction("Index","MgtProject");
                }
                catch (Exception e)
                {

                    ViewBag.mess = e.Message.ToString();
                }
            }
            ViewBag.IndustryTypeKey = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "IndustryKey", "TypeName");

            return View(modelClass);
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