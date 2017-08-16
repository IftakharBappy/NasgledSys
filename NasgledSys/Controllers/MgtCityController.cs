using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.IO;
using NasgledSys.DAL;
using System.Web.Script.Serialization;
namespace NasgledSys.Controllers
{
    public class MgtCityController : Controller
    {
        // GET: MgtCity
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtCityList manage = new MgtCityList();
        // GET: Home  
        public ActionResult Index()
        {
            try
            {
                CityClass obj = new CityClass();
                obj.CityList = new List<CityClass>();
                obj.CityList = manage.ListAll();
                ViewBag.StateCode = new SelectList(db.StateList.Where(m => m.IsDelete == false).OrderBy(m => m.StateName), "PKey", "StateName");
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                CityList model = db.CityList.Find(id);
                model.IsDelete = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtCity", "Index"));
            }
        }
        public JsonResult Add(CityClass city)
        {
            return Json(manage.Add(city), JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult Update(CityClass city)
        {
            return Json(manage.Update(city), JsonRequestBehavior.AllowGet);
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