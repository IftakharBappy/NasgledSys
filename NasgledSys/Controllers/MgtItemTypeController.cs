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
    public class MgtItemTypeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtItemType manage = new MgtItemType();
        public ActionResult Index()
        {
            logger.Info("MgtItemType Index() invoked by :  " + GlobalClass.LoginUser.PName);
            try
            {
                ItemTypeViewModel obj = new ItemTypeViewModel();
                obj.ItemTypeViewModelList = new List<ItemTypeViewModel>();
                obj.ItemTypeViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(ItemTypeViewModel modelClass)
        {
            logger.Info("MgtItemType Add() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ItemTypeViewModel modelClass)
        {
            logger.Info("MgtItemType Update() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid ID)
        {
            logger.Info("MgtItemType Delete() invoked by :  " + GlobalClass.LoginUser.PName);
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