﻿using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtHeatingEfficiencyTypeController : Controller
    {
        // GET: HeatingEfficiencyType
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtHeatingEfficiencyType manage = new MgtHeatingEfficiencyType();
        public ActionResult Index()
        {
            try
            {
                HeatingEfficiencyTypeViewModel obj = new HeatingEfficiencyTypeViewModel();
                obj.HeatingEfficiencyTypeViewModelList = new List<HeatingEfficiencyTypeViewModel>();
                obj.HeatingEfficiencyTypeViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
            
        }
        public JsonResult Add(HeatingEfficiencyTypeViewModel HeatingEfficiencyType)
        {
            return Json(manage.Add(HeatingEfficiencyType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(HeatingEfficiencyTypeViewModel HeatingEfficiencyType)
        {
            return Json(manage.Update(HeatingEfficiencyType), JsonRequestBehavior.AllowGet);
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