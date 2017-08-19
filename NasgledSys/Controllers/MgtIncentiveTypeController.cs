using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtIncentiveTypeController : Controller
    {
        // GET: MgtIncentiveType
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtIncentiveType manage = new MgtIncentiveType();
        public ActionResult Index()
        {
            try
            {
                IncentiveTypeClass obj = new IncentiveTypeClass();
                obj.IncentiveTypeList = new List<IncentiveTypeClass>();
                obj.IncentiveTypeList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(IncentiveTypeClass IncentiveType)
        {
            return Json(manage.Add(IncentiveType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(IncentiveTypeClass IncentiveType)
        {
            return Json(manage.Update(IncentiveType), JsonRequestBehavior.AllowGet);
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