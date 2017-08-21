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
    public class MgtProfileController : Controller
    {
        // GET: MgtProfile
        private NasgledDBEntities db = new NasgledDBEntities();
        private FormValidation validate = new FormValidation();
        public ActionResult Index()
        {
            try
            {
                List<SelectListItem> HireOutsiders = new List<SelectListItem>();
                HireOutsiders.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
                HireOutsiders.Add(new SelectListItem() { Text = "No", Value = "No" });

                ViewBag.mess="";
                ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName).Select(m=>new { PKey=m.CityKey, CityName =m.CityName+" , "+m.StateList.StateName}), "PKey", "CityName");
                ViewBag.PrimaryBusinessType = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName");
                ViewBag.HireOutsideAuditor = new SelectList(HireOutsiders, "Value", "Text"); 
                ViewBag.AnnualSalesRevenue = new SelectList(db.AnnualSalesRevenueSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName");
                ViewBag.SalesReach = new SelectList(db.SalesReachSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName");

                return View();
            }
            catch (Exception e)
            {

                return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
            }
        }
        [HttpPost]
        public ActionResult Index(ProfileClass modelClass)
        {
            ViewBag.mess = "";
            List<SelectListItem> HireOutsiders = new List<SelectListItem>();
            HireOutsiders.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
            HireOutsiders.Add(new SelectListItem() { Text = "No", Value = "No" });
            if (ModelState.IsValid)
            {
               
                try
                {
                    modelClass = validate.ValidateProfileRegistration(modelClass);
                    UserProfile asset = EM_Profile.ConvertToEntity(modelClass);
                    asset.ProfileKey = GlobalClass.ProfileUser.ProfileKey;
                    asset.StateKey = db.CityList.Find(asset.CityKey).StateCode;                  
                    UserProfile pf = db.UserProfile.Find(GlobalClass.ProfileUser.ProfileKey);
                    pf.FirstName = asset.FirstName;
                    pf.LastName = asset.LastName;
                    pf.CompanyName = asset.CompanyName;
                    pf.JobTitle = asset.JobTitle;
                    pf.CityKey = asset.CityKey;
                    pf.StateKey = asset.StateKey;
                    pf.PhoneNo = asset.PhoneNo;
                    pf.PrimaryBusinessType = asset.PrimaryBusinessType;
                    pf.HireOutsideAuditor = asset.HireOutsideAuditor;
                    pf.AnnualSalesRevenue = asset.AnnualSalesRevenue;
                    pf.SalesReach = asset.SalesReach;
                    pf.DirectManufacture = asset.DirectManufacture;
                    pf.DirectDistributor = asset.DirectDistributor;
                  
                    db.SaveChanges();
                    Session["GlobalMessege"] = "Profile is Saved";
                    return RedirectToAction("Company");
                }
                catch (Exception e)
                {

                    ViewBag.mess = e.Message.ToString();
                }
            }
            ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName).Select(m => new { PKey = m.CityKey, CityName = m.CityName + " , " + m.StateList.StateName }), "PKey", "CityName");
            ViewBag.PrimaryBusinessType = new SelectList(db.IndustryType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName",modelClass.PrimaryBusinessType);
            ViewBag.HireOutsideAuditor = new SelectList(HireOutsiders, "Value", "Text",modelClass.HireOutsideAuditor); 
            ViewBag.AnnualSalesRevenue = new SelectList(db.AnnualSalesRevenueSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName",modelClass.AnnualSalesRevenue);
            ViewBag.SalesReach = new SelectList(db.SalesReachSetup.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "TypeName", "TypeName",modelClass.SalesReach);

            return View(modelClass);
        }

        public ActionResult Company()
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
    }
}