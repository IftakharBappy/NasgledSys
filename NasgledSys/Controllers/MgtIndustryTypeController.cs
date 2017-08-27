using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtIndustryType manage = new MgtIndustryType();
        public ActionResult Index()
        {
            logger.Info("MgtIndustryType Index() invoked by :  " + GlobalClass.LoginUser.PName);
            try
            {
                IndustryTypeViewModel obj = new IndustryTypeViewModel();
                obj.IndustryTypeViewModelList = new List<IndustryTypeViewModel>();
                obj.IndustryTypeViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
            
        }
        public JsonResult Add(IndustryTypeViewModel modelClass)
        {
            logger.Info("MgtIndustryType Add() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(IndustryTypeViewModel modelClass)
        {
            logger.Info("MgtIndustryType Update() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid ID)
        {
            logger.Info("MgtIndustryType Delete() invoked by :  " + GlobalClass.LoginUser.PName);
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