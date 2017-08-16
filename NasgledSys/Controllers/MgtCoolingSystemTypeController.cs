using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtCoolingSystemTypeController : Controller
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtCoolingSystemType manage = new MgtCoolingSystemType();
        public ActionResult Index()
        {
            try
            {
                CoolingSystemTypeClass obj = new CoolingSystemTypeClass();
                obj.CoolingSystemTypeList = new List<CoolingSystemTypeClass>();
                obj.CoolingSystemTypeList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
            return View();
        }
        public JsonResult Add(CoolingSystemTypeClass CoolingSystemType)
        {
            return Json(manage.Add(CoolingSystemType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(CoolingSystemTypeClass CoolingSystemType)
        {
            return Json(manage.Update(CoolingSystemType), JsonRequestBehavior.AllowGet);
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