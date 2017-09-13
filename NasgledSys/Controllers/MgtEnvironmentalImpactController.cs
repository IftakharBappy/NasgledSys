using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtEnvironmentalImpactController : Controller
    {
        // GET: MgtEnvironmentalImpact
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            try
            {
                var environmentalImpact = db.EnvironmentalImpact;
                EnvironmentalImpactViewModel model = new EnvironmentalImpactViewModel();

                model.EnvironmentalImpactViewModelList = environmentalImpact.Select(p => new EnvironmentalImpactViewModel()
                {
                    FactorKey = p.FactorKey,
                    FactorName = p.FactorName,
                    UnitName = p.UnitName,
                    QtyUsed = p.QtyUsed,
                    KilowattSaved = p.KilowattSaved,
                    Logo = p.Logo
                }).ToList();

                ViewBag.Unit = new SelectList(db.Unit.OrderBy(m => m.UnitName), "UnitKey", "UnitName");
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }
    }
}