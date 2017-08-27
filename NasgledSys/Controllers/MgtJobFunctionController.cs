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
    public class MgtJobFunctionController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtJobFunction manage = new MgtJobFunction();
        public ActionResult Index()
        {
            logger.Info("MgtJobFunction Index() invoked by :  " + GlobalClass.LoginUser.PName);
            try
            {
                JobFunctionViewModel obj = new JobFunctionViewModel();
                obj.JobFunctionViewModelList = new List<JobFunctionViewModel>();
                obj.JobFunctionViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(JobFunctionViewModel modelClass)
        {
            logger.Info("MgtJobFunction Add() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(JobFunctionViewModel modelClass)
        {
            logger.Info("MgtJobFunction Update() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid ID)
        {
            logger.Info("MgtJobFunction Delete() invoked by :  " + GlobalClass.LoginUser.PName);
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