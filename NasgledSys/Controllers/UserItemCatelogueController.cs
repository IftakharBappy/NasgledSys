using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class UserItemCatelogueController : Controller
    {
        // GET: UserItemCatelogue
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            try
            {
                var ItemCatelogue = db.ItemCatelogue;
                ItemCatelogueViewModel model = new ItemCatelogueViewModel();

                model.ItemCatelogueViewModelList = ItemCatelogue.Select(p => new ItemCatelogueViewModel()
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
        public ActionResult Create(ItemCatelogueViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        ItemCatelogue model = new ItemCatelogue();
                        model.PKey = Guid.NewGuid();
                        model.TypeName = obj.TypeName;
                        model.Description = obj.Description;

                        db.ItemCatelogue.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Item Catelogue is Saved Successfully";
                        return RedirectToAction("Index");
                    }
                    return View(obj);

                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "UserItemCatelogue", "Index"));
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