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
    public class MgtLightingSatisfactionFactorController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtLightingSatisfactionFactor manage = new MgtLightingSatisfactionFactor();
        public ActionResult Index()
        {
            logger.Info("MgtLightingSatisfactionFactor Index() invoked by :  " + GlobalClass.LoginUser.PName);
            try
            {
                LightingSatisfactionFactorViewModel obj = new LightingSatisfactionFactorViewModel();
                obj.LightingSatisfactionFactorViewModelList = new List<LightingSatisfactionFactorViewModel>();
                obj.LightingSatisfactionFactorViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(LightingSatisfactionFactorViewModel modelClass)
        {
            logger.Info("MgtLightingSatisfactionFactor Add() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(LightingSatisfactionFactorViewModel modelClass)
        {
            logger.Info("MgtLightingSatisfactionFactor Update() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid ID)
        {
            logger.Info("MgtLightingSatisfactionFactor Delete() invoked by :  " + GlobalClass.LoginUser.PName);
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