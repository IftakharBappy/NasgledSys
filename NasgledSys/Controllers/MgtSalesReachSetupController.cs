﻿using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtSalesReachSetupController : Controller
    {

        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtSalesReachSetup manage = new MgtSalesReachSetup();
        public ActionResult Index()
        {
            try
            {
                SalesReachSetupViewModel obj = new SalesReachSetupViewModel();
                obj.SalesReachSetupViewModelList = new List<SalesReachSetupViewModel>();
                obj.SalesReachSetupViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(SalesReachSetupViewModel modelClass)
        {
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(SalesReachSetupViewModel modelClass)
        {
            return Json(manage.Update(modelClass), JsonRequestBehavior.AllowGet);
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