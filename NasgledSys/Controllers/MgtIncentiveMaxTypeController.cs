using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtIncentiveMaxTypeController : Controller
    {
        // GET: MgtIncentiveMaxType
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtIncentiveMaxType manage = new MgtIncentiveMaxType();
        public ActionResult Index()
        {
            try
            {
                IncentiveMaxTypeClass obj = new IncentiveMaxTypeClass();
                obj.IncentiveMaxTypeList = new List<IncentiveMaxTypeClass>();
                obj.IncentiveMaxTypeList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(IncentiveMaxTypeClass IncentiveMaxType)
        {
            return Json(manage.Add(IncentiveMaxType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(IncentiveMaxTypeClass IncentiveMaxType)
        {
            return Json(manage.Update(IncentiveMaxType), JsonRequestBehavior.AllowGet);
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
