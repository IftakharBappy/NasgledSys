﻿using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtItemSubcategoryController : Controller
    {

        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtItemSubcategory manage = new MgtItemSubcategory();
        public ActionResult Index()
        {
            try
            {
                ItemSubcategoryClass obj = new ItemSubcategoryClass();
                obj.ItemSubcategoryClassList = new List<ItemSubcategoryClass>();
                obj.ItemSubcategoryClassList = manage.ListAll();
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
        public JsonResult Add(ItemSubcategoryClass modelClass)
        {
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ItemSubcategoryClass modelClass)
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