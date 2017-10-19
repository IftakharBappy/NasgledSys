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
            var proposed = new MgtProposalSolutionEditProposedViewModel();
            proposed.ProfileProductNameText = profileProduct.ProductName;
            proposed.ProductKey = productKey;
            proposed.ProposalKey = proposalKey;

            ViewBag.profileProducts = new SelectList(db.ProfileProduct, "FixtureKey", "ProductName");

            return View(proposed);
        }
        [HttpPost]
        public ActionResult Proposed(MgtProposalSolutionEditProposedViewModel model)
        {
            var areaProduct = db.AreaProduct.Where(ap => ap.ProductKey == model.ProductKey).FirstOrDefault();
            //var profileProduct = db.ProfileProduct.Where(pp => pp.FixtureKey == areaProduct.FixtureKey).FirstOrDefault();

            if (model.ProposedProfileProductKey != null)
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
        public class MgtProposalSolutionEditProposedViewModel
        {
            public Guid ProductKey { get; set; }
            public Guid ProposalKey { get; set; }
            [Display(Name = "Existing")]
            public string ProfileProductNameText { get; set; }

            [Display(Name = "Proposed Product")]
            public Guid ProposedProfileProductKey { get; set; }
        }



        public ActionResult Ratios(Guid productKey, Guid proposalKey)
        {
            var areaProduct = db.AreaProduct.Where(ap => ap.ProductKey == productKey).FirstOrDefault();
            var profileProduct = db.ProfileProduct.Where(pp => pp.FixtureKey == areaProduct.FixtureKey).FirstOrDefault();
            MgtProposalSolutionEditRatioViewModel model = new MgtProposalSolutionEditRatioViewModel();
            model.ProductKey = productKey;
            model.ProposalKey = proposalKey;
            model.ProfileProductNameText = profileProduct.ProductName;
            model.ForEveryMainQty = areaProduct.ForEveryMainQty;
            model.ReplaceQty = areaProduct.ReplaceQty;
            return View(model);
        }
        [HttpPost]
        public ActionResult Ratios(MgtProposalSolutionEditRatioViewModel model)
        {
            var areaProduct = db.AreaProduct.Where(ap => ap.ProductKey == model.ProductKey).FirstOrDefault();
            areaProduct.ForEveryMainQty = model.ForEveryMainQty;
            areaProduct.ReplaceQty = model.ReplaceQty;
            db.SaveChanges();
            return RedirectToAction("Details", new { productKey = model.ProductKey, proposalKey = model.ProposalKey });
        }
        public class MgtProposalSolutionEditRatioViewModel
        {
            public Guid ProductKey { get; set; }
            public Guid ProposalKey { get; set; }
            public string ProfileProductNameText { get; set; }

            public int? ForEveryMainQty { get; set; }
            public int? ReplaceQty { get; set; }

        }


        
    }
}