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
    public class MgtItemCatelogueController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtItemCatelogue manage = new MgtItemCatelogue();
        public ActionResult Index()
        {
            logger.Info("MgtItemCatelogue Index() invoked by :  " + GlobalClass.LoginUser.PName);
            try
            {
                ItemCatelogueViewModel obj = new ItemCatelogueViewModel();
                obj.ItemCatelogueViewModelList = new List<ItemCatelogueViewModel>();
                obj.ItemCatelogueViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(ItemCatelogueViewModel modelClass)
        {
            logger.Info("MgtItemCatelogue Add() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ItemCatelogueViewModel modelClass)
        {
            logger.Info("MgtItemCatelogue Update() invoked by :  " + GlobalClass.LoginUser.PName);
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid ID)
        {
            logger.Info("MgtItemCatelogue Delete() invoked by :  " + GlobalClass.LoginUser.PName);
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