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
namespace NasgledSys.Controllers
{
    public class MgtProfileController : Controller
    {
        // GET: MgtProfile
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            try
            {
                ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName).Select(m=>new { PKey=m.CityKey, CityName =m.CityName+" , "+m.StateList.StateName}), "PKey", "CityName");
                return View();
            }
            catch (Exception e)
            {

                return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
            }
        }

        public async Task<ActionResult> AddProfile(ProfileClass modelClass)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName).Select(m => new { PKey = m.CityKey, CityName = m.CityName + " , " + m.StateList.StateName }), "PKey", "CityName");
                return View("Index", modelClass);
            }

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


            var task = db.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
               
                ModelState.AddModelError("", "Unable to add the Profile due to "+task.Exception.Message.ToString());
                return View("Index", modelClass);
            }

            return Content("Profile has been Successfully Updated.");
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