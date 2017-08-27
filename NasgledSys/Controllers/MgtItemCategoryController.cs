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
    public class MgtItemCategoryController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtItemCategory manage = new MgtItemCategory();
        public ActionResult Index()
        {
            logger.Info("MgtItemCategory Index() invoked by :  " + GlobalClass.LoginUser.PName);
            try
            {
                ItemCategoryViewModel obj = new ItemCategoryViewModel();
                obj.ItemCategoryViewModelList = new List<ItemCategoryViewModel>();
                obj.ItemCategoryViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
            
        }
        public JsonResult Add(ItemCategoryViewModel modelClass)
        {
            logger.Info("MgtItemCategory Add() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ItemCategoryViewModel modelClass)
        {
            logger.Info("MgtItemCategory Update() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid ID)
        {
            logger.Info("MgtItemCategory Delete() invoked by :  " + GlobalClass.LoginUser.PName);
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