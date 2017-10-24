using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.ComponentModel.DataAnnotations;

namespace NasgledSys.Controllers
{
    public class MgtProposalSolutionEditController : Controller
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Existing(Guid proposalKey)
        {
            ViewBag.ExistingProduct = (from ap in db.AreaProduct
                                       where ap.ProjectKey == GlobalClass.Project.ProjectKey
                                       where ap.IsDelete == false
                                       select ap).ToList();
            ViewBag.proposalKey = proposalKey;
            return View();
        }

        public ActionResult Proposed(Guid productKey, Guid proposalKey)
        {
            var areaProduct = db.AreaProduct.Where(ap => ap.ProductKey == productKey).FirstOrDefault();
            var profileProduct = db.ProfileProduct.Where(pp => pp.FixtureKey == areaProduct.FixtureKey).FirstOrDefault();
            var proposed = new ProposalSolutionEditProposedViewModel();
            proposed.ProfileProductNameText = profileProduct.ProductName;
            proposed.ProductKey = productKey;
            proposed.ProposalKey = proposalKey;

            ViewBag.profileProducts = new SelectList(db.ProfileProduct, "FixtureKey", "ProductName");

            return View(proposed);
        }
        [HttpPost]
        public ActionResult Proposed(ProposalSolutionEditProposedViewModel model)
        {
            var areaProduct = db.AreaProduct.Where(ap => ap.ProductKey == model.ProductKey).FirstOrDefault();
            //var profileProduct = db.ProfileProduct.Where(pp => pp.FixtureKey == areaProduct.FixtureKey).FirstOrDefault();

            if (model.ProposedProfileProductKey.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                var proposedAreaProdct = db.ProfileProduct.Where(ap => ap.FixtureKey == model.ProposedProfileProductKey).FirstOrDefault();
                areaProduct.ProductName = proposedAreaProdct.ProductName;
                areaProduct.ModelNo = proposedAreaProdct.ModelNo;
                areaProduct.WattPerProduct = proposedAreaProdct.Watt;
                //areaProduct.L70 = proposedAreaProdct.; //#todo
                areaProduct.ThermalEfficency = proposedAreaProdct.ThermalEfficiency;
                db.SaveChanges();

            }
            return RedirectToAction("Ratios", new { productKey = model.ProductKey, proposalKey = model.ProposalKey });
        }
        
        public ActionResult Ratios(Guid productKey, Guid proposalKey)
        {
            var areaProduct = db.AreaProduct.Where(ap => ap.ProductKey == productKey).FirstOrDefault();
            var profileProduct = db.ProfileProduct.Where(pp => pp.FixtureKey == areaProduct.FixtureKey).FirstOrDefault();
            ProposalSolutionEditRatioViewModel model = new ProposalSolutionEditRatioViewModel();
            model.ProductKey = productKey;
            model.ProposalKey = proposalKey;
            model.ProfileProductNameText = profileProduct.ProductName;
            model.ForEveryMainQty = areaProduct.ForEveryMainQty;
            model.ReplaceQty = areaProduct.ReplaceQty;
            return View(model);
        }
        [HttpPost]
        public ActionResult Ratios(ProposalSolutionEditRatioViewModel model)
        {
            var areaProduct = db.AreaProduct.Where(ap => ap.ProductKey == model.ProductKey).FirstOrDefault();
            areaProduct.ForEveryMainQty = model.ForEveryMainQty;
            areaProduct.ReplaceQty = model.ReplaceQty;
            db.SaveChanges();
            return RedirectToAction("Details", new { productKey = model.ProductKey, proposalKey = model.ProposalKey });
        }
        
        public ActionResult Details(Guid productKey, Guid proposalKey)
        {
            var areaProduct = db.AreaProduct.Where(ap => ap.ProductKey == productKey).FirstOrDefault();
            var area = db.Area.Where(a => a.AreaKey == areaProduct.AreaKey).FirstOrDefault();
            var profileProduct = db.ProfileProduct.Where(pp => pp.FixtureKey == areaProduct.FixtureKey).FirstOrDefault();
            var detail = new ProposalSolutionEditDetailViewModel();
            detail.ProductKey = productKey;
            detail.ProposalKey = proposalKey;

            detail.ProfileProductNameText = profileProduct.ProductName + ":" + area.AreaName;
            detail.SolutionName = areaProduct.SolutionName;
            detail.IncentiveAmount = areaProduct.IncentiveAmount;
            detail.IncentiveMaxAmount = areaProduct.IncentiveMaxAmount;
            detail.ProductCost = areaProduct.ProductCost;
            detail.MarkupPercentage = areaProduct.MarkupPercentage;
            detail.InstallationTime = areaProduct.InstallationTime;
            detail.InstallationCost = areaProduct.InstallationCost;
            detail.MiscCost = areaProduct.MiscCost;
            detail.ProductName = areaProduct.ProductName;
            detail.ModelNo = areaProduct.ModelNo;
            detail.WattPerProduct = areaProduct.WattPerProduct;
            detail.ThermalEfficency = areaProduct.ThermalEfficency;

            ViewBag.IncentiveTypes = new SelectList(db.IncentiveType, "PKey", "TypeName", areaProduct.IncentiveTypeKey);
            ViewBag.IncentiveMaxType = new SelectList(db.IncentiveMaxType, "PKey", "TypeName", areaProduct.IncentiveMaxTypeKey);

            return View(detail);
        }
        [HttpPost]
        public ActionResult Details(ProposalSolutionEditDetailViewModel model)
        {

            var areaProduct = db.AreaProduct.Where(ap => ap.ProductKey == model.ProductKey).FirstOrDefault();
            areaProduct.SolutionName = model.SolutionName;
            areaProduct.IncentiveTypeKey = model.IncentiveTypeKey;
            areaProduct.IncentiveAmount = model.IncentiveAmount;
            areaProduct.IncentiveMaxTypeKey = model.IncentiveMaxTypeKey;
            areaProduct.IncentiveMaxAmount = model.IncentiveMaxAmount;
            areaProduct.ProductCost = model.ProductCost;
            areaProduct.MarkupPercentage = model.MarkupPercentage;
            areaProduct.InstallationTime = model.InstallationTime;
            areaProduct.InstallationCost = model.InstallationCost;
            areaProduct.MiscCost = model.MiscCost;
            areaProduct.ProductName = model.ProductName;
            areaProduct.ModelNo = model.ModelNo;
            areaProduct.WattPerProduct = model.WattPerProduct;
            areaProduct.ThermalEfficency = model.ThermalEfficency;
            db.SaveChanges();
            return RedirectToAction("Index", "MgtProposalSolution", new { id = model.ProposalKey });
        }
        
    }
}