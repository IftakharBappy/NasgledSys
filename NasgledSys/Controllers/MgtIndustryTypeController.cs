using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtIndustryTypeController : Controller
    {

        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtIndustryType manage = new MgtIndustryType();
        public ActionResult Index()
        {
            try
            {
                IndustryTypeClass obj = new IndustryTypeClass();
                obj.IndustryTypeClassList = new List<IndustryTypeClass>();
                obj.IndustryTypeClassList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
            
        }
        public JsonResult Add(IndustryTypeClass modelClass)
        {
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(IndustryTypeClass modelClass)
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