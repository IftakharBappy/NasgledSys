using NasgledSys.DAL;
using NasgledSys.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtManufacturerController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtManufacturer manage = new MgtManufacturer();
        public ActionResult Index()
        {
            logger.Info("MgtManufacturer Index() invoked by :  " + GlobalClass.LoginUser.PName);
            try
            {
                ManufacturerViewModel obj = new ManufacturerViewModel();
                obj.ManufacturerViewModelList = new List<ManufacturerViewModel>();
                obj.ManufacturerViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return View("Error", new HandleErrorInfo(ex, "Home", "Index"));
            }

        }
        public JsonResult Add(ManufacturerViewModel modelClass)
        {
            logger.Info("MgtManufacturer Add invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ManufacturerViewModel modelClass)
        {
            logger.Info("MgtManufacturer Update invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid ID)
        {
            logger.Info("MgtManufacturer Delete invoked by :  " + GlobalClass.LoginUser.PName);
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