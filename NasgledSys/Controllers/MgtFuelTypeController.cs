using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtFuelTypeController : Controller
    {
      
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtFuelType manage = new MgtFuelType();

        // GET: MgtFuelType
        public ActionResult Index()
        {
            try
            {
                FuelTypeClass obj = new FuelTypeClass();
                obj.FuelTypeClassList = new List<FuelTypeClass>();
                obj.FuelTypeClassList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
            
        }
        public JsonResult Add(FuelTypeClass FuelType)
        {
            return Json(manage.Add(FuelType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(FuelTypeClass FuelType)
        {
            return Json(manage.Update(FuelType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid ID)
        {
            return Json(manage.Delete(ID), JsonRequestBehavior.AllowGet);
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