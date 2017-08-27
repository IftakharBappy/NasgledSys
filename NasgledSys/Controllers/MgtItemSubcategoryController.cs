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
    public class MgtItemSubcategoryController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtItemSubcategory manage = new MgtItemSubcategory();
        public ActionResult Index()
        {
            logger.Info("MgtItemSubcategory Index() invoked by :  " + GlobalClass.LoginUser.PName);
            try
            {
                ItemSubcategoryViewModel obj = new ItemSubcategoryViewModel();
                obj.ItemSubcategoryViewModelList = new List<ItemSubcategoryViewModel>();
                obj.ItemSubcategoryViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(ItemSubcategoryViewModel modelClass)
        {
            logger.Info("MgtItemSubcategory Add() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ItemSubcategoryViewModel modelClass)
        {
            logger.Info("MgtItemSubcategory Update() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid ID)
        {
            logger.Info("MgtItemSubcategory Delete() invoked by :  " + GlobalClass.LoginUser.PName);
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