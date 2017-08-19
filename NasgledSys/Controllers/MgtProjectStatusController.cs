using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtProjectStatusController : Controller
    {
        // GET: MgtProjectStatus
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtProjectStatus manage = new MgtProjectStatus();
        public ActionResult Index()
        {
            try
            {
                ProjectStatusClass obj = new ProjectStatusClass();
                obj.ProjectStatusList = new List<ProjectStatusClass>();
                obj.ProjectStatusList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(ProjectStatusClass ProjectStatus)
        {
            return Json(manage.Add(ProjectStatus), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ProjectStatusClass ProjectStatus)
        {
            return Json(manage.Update(ProjectStatus), JsonRequestBehavior.AllowGet);
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