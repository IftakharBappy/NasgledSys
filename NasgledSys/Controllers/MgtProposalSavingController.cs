﻿using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Calculations;
namespace NasgledSys.Controllers
{
    public class MgtProposalSavingController : Controller
    {
        // GET: MgtProposalSaving
        private NasgledDBEntities db = new NasgledDBEntities();
        private CalculateProposalCosts Costs = new CalculateProposalCosts();
        public ActionResult Saving(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id);
                    ProposalSavingViewModel model = new ProposalSavingViewModel();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;

                    if (model.AnnualCost == null) model.AnnualCost = Costs.GetExistingCost(p.ProjectKey) - Costs.GetNewCost(p.ProjectKey);
                    if (model.AnnualEnergy == null) model.AnnualEnergy = Costs.GetExistingEnergy(p.ProjectKey) - Costs.GetNewEnergy(p.ProjectKey);
                    if (model.ReduceCarbon == null) model.ReduceCarbon = 0;

                    ViewBag.Env = (from os in db.EnvironmentalImpact
                                                  
                                                  select new EnvironmentalImpactViewModel()
                                                  {
                                                      FactorName = os.FactorName,
                                                      UnitName = os.UnitName,
                                                      QtyUsed = os.QtyUsed,
                                                      Logo = os.Logo,
                                                      
                                                  }
                                                  ).ToList();


                    return View(model);
                }
                catch (Exception e)
                {
                    return View("Error", new HandleErrorInfo(e, "Home", "Index"));
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