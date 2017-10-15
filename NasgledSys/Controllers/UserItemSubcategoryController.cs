using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class UserItemSubcategoryController : Controller
    {
        // GET: UserItemSubcategory
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            try
            {
                var ItemSubCategory = db.ItemSubcategory;
                ItemSubcategoryViewModel model = new ItemSubcategoryViewModel();

                model.ItemSubcategoryViewModelList = ItemSubCategory.Select(p => new ItemSubcategoryViewModel()
                {
                    PKey = p.PKey,
                    TypeName = p.TypeName,
                    Description = p.Description,
                }).ToList();

                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }
        [HttpPost]
        public ActionResult Create(ItemSubcategoryViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        ItemSubcategory model = new ItemSubcategory();
                        model.PKey = Guid.NewGuid();
                        model.TypeName = obj.TypeName;
                        model.Description = obj.Description;

                        db.ItemSubcategory.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Item Sub Category is Saved Successfully";
                        Session["counter"] = 1;
                        return RedirectToAction("Index");
                    }
                    return View(obj);

                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "UserItemSubcategory", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
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