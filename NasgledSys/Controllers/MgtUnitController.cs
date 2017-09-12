using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtUnitController : Controller
    {
        // GET: MgtUnit
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {

            try
            {
                var unit = db.Unit;
                UnitViewModel model = new UnitViewModel();

                model.UnitViewModelList = unit.Select(p => new UnitViewModel()
                {
                    UnitKey = p.UnitKey,
                    UnitName = p.UnitName,
                    UnitShortname = p.UnitShortname,
                }).ToList();

                return View(model);

                //var unitList = db.Unit;
                //return View(unitList.ToList());
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }

        [HttpPost]
        public ActionResult Create(UnitViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        Unit model = new Unit();
                        model.UnitKey = Guid.NewGuid();
                        model.UnitName = obj.UnitName;
                        model.UnitShortname = obj.UnitShortname;
                        if (string.IsNullOrEmpty(obj.UnitName)) model.UnitName = obj.UnitShortname;

                        db.Unit.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Contact is Saved Successfully";
                        return RedirectToAction("Index");
                    }
                    
                    return View(obj);

                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtUnit", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(UnitViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Unit model = db.Unit.Find(obj.UnitKey);
                        model.UnitName = obj.UnitName;
                        model.UnitShortname = obj.UnitShortname;
                        if (string.IsNullOrEmpty(obj.UnitName)) model.UnitName = obj.UnitShortname;
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Unit Info is Updated";
                        return RedirectToAction("Index", new { id = obj.UnitKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtUnit", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public JsonResult Delete(Guid? ID)
        {
            Unit model = db.Unit.Where(x => x.UnitKey == ID).FirstOrDefault();
            db.Unit.Remove(model);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);

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