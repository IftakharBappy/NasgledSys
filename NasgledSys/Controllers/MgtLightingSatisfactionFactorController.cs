using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtLightingSatisfactionFactorController : Controller
    {

        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtLightingSatisfactionFactor manage = new MgtLightingSatisfactionFactor();
        public ActionResult Index()
        {
            try
            {
                LightingSatisfactionFactorClass obj = new LightingSatisfactionFactorClass();
                obj.LightingSatisfactionFactorClassList = new List<LightingSatisfactionFactorClass>();
                obj.LightingSatisfactionFactorClassList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(LightingSatisfactionFactorClass modelClass)
        {
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(LightingSatisfactionFactorClass modelClass)
        {
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
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