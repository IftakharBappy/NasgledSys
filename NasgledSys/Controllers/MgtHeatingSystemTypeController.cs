﻿using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtHeatingSystemTypeController : Controller
    {
        // GET: HeatingSystemType
        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtHeatingSystemType manage = new MgtHeatingSystemType();
        // GET: MgtCoolingEfficiencyType
        public ActionResult Index()
        {
            try
            {
                HeatingSystemTypeViewModel obj = new HeatingSystemTypeViewModel();
                obj.HeatingSystemTypeViewModelList = new List<HeatingSystemTypeViewModel>();
                obj.HeatingSystemTypeViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }
        public JsonResult Add(HeatingSystemTypeViewModel HeatingSystemType)
        {
            return Json(manage.Add(HeatingSystemType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(HeatingSystemTypeViewModel HeatingSystemType)
        {
            return Json(manage.Update(HeatingSystemType), JsonRequestBehavior.AllowGet);
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