using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;

namespace NasgledSys.Controllers
{
    public class DropdownUtilityController : Controller
    {
        // GET: DropdownUtility
        private NasgledDBEntities db = new NasgledDBEntities();
        [HttpPost]
        public ActionResult GetCity(int stateId)
        {
            
            List<SelectListItem> districtNames = new List<SelectListItem>();            
                
                List<CityList> city = db.CityList.Where(x => x.StateCode == stateId && x.IsDelete==false).ToList();
                city.ForEach(x =>
                {
                    districtNames.Add(new SelectListItem { Text = x.CityName, Value = x.CityKey.ToString() });
                });
           
            return Json(districtNames, JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult GetStateName(string query)
        {
            var users = (from u in db.StateList

                         where (u.StateCode.ToUpper().Contains(query.ToUpper()) || u.StateName.ToUpper().Contains(query.ToUpper()))

                         && u.IsDelete == false
                         orderby u.StateName
                         select new
                         {
                             label = u.StateName + "( " + u.StateCode + " )",
                             value = u.PKey

                         }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCityName(string query, int StateKey)
        {
              var users = (from u in db.CityList

                         where u.CityName.ToUpper().Contains(query.ToUpper())
                         && u.IsDelete == false
                         && u.StateCode == StateKey

                         orderby u.CityName

                         select new
                         {
                             label = u.CityName,
                             value = u.CityKey
                         }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetZipcode(string query, int CityKey)
        {
            var users = (from u in db.ZIPList

                         where u.ZIP.ToUpper().Contains(query.ToUpper()) && u.CityList.IsDelete == false
                         && u.IsDelete == false && u.CityKey == CityKey
                         orderby u.ZIP
                         select new
                         {
                             label = u.ZIP,
                             value = u.ZipKey

                         }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
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