using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    return View("Error", new HandleErrorInfo(ex, "MgtClientContact", "Index"));
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