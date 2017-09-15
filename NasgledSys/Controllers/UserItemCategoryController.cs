using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class UserItemCategoryController : Controller
    {
        // GET: UserItemCategory
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            try
            {
                var ItemCategory = db.ItemCategory;
                ItemCategoryViewModel model = new ItemCategoryViewModel();

                model.ItemCategoryViewModelList = ItemCategory.Select(p => new ItemCategoryViewModel()
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
        public ActionResult Create(ItemCategoryViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        ItemCategory model = new ItemCategory();
                        model.PKey = Guid.NewGuid();
                        model.TypeName = obj.TypeName;
                        model.Description = obj.Description;

                        db.ItemCategory.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "ItemCategory is Saved Successfully";
                        return RedirectToAction("Index");
                    }

                    return View(obj);

                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "UserItemCategory", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
    }
}