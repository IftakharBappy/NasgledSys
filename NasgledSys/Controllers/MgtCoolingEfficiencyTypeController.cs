﻿using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtCoolingEfficiencyTypeController : Controller
    {

        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtCoolingEfficiencyType manage = new MgtCoolingEfficiencyType();
        // GET: MgtCoolingEfficiencyType
        public ActionResult Index()
        {
            try
            {
                CoolingEfficiencyTypeViewModel obj = new CoolingEfficiencyTypeViewModel();
                obj.CoolingEfficiencyTypeViewModelList = new List<CoolingEfficiencyTypeViewModel>();
                obj.CoolingEfficiencyTypeViewModelList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
            
        }
        public JsonResult Add(CoolingEfficiencyTypeViewModel CoolingEfficientyType)
        {
            return Json(manage.Add(CoolingEfficientyType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(CoolingEfficiencyTypeViewModel CoolingEfficiency)
        {
            return Json(manage.Update(CoolingEfficiency), JsonRequestBehavior.AllowGet);
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