using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtItemCategoryController : Controller
    {

        private NasgledDBEntities db = new NasgledDBEntities();
        private MgtItemCategory manage = new MgtItemCategory();
        public ActionResult Index()
        {
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
            return Json(manage.Add(modelClass), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ItemCategoryViewModel modelClass)
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