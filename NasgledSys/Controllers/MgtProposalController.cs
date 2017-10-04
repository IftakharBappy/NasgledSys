﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.Web.Security;
using System.Web.Mail;
using System.Threading.Tasks;
using NasgledSys.EM;
using NasgledSys.Validation;
using System.Globalization;
using NasgledSys.DAL;
using DataTables.Mvc;
using System.Linq.Dynamic;

namespace NasgledSys.Controllers
{
    public class MgtProposalController : Controller
    {
        // GET: MgtProposal
        private NasgledDBEntities db = new NasgledDBEntities();
     
        public ActionResult Index(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Proposal model = new Proposal();
                    model.ProjectKey = id;
                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "MgtProject", "Created"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

      
        public JsonResult GetProfileUsername(string id)
        {
            UserProfile p = db.UserProfile.Find(GlobalClass.LoggedInUser.ProfileKey);
            string list = "";
            if (string.IsNullOrEmpty(p.Email))
            {
                list = p.FirstName + " " + p.LastName + "-" + p.UserRole.RoleName;
            }
            else
            {
                list = p.FirstName + " " + p.LastName + "-" + p.UserRole.RoleName + "-" + p.Email;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> Create(string ProposalName, string PreparedByUser)
        {
            try
            {
                Proposal contact = new Proposal();
                contact.ProposalKey = Guid.NewGuid();
                contact.ProposalName = ProposalName;
                contact.PreparedByText = PreparedByUser;
                contact.ProjectKey = GlobalClass.Project.ProjectKey;
                contact.CreateDate = System.DateTime.Now;
                contact.PreparedBy = GlobalClass.LoggedInUser.ProfileKey;
                contact.IsDelete = false;
                contact.MarkupPercentage = 0;
                contact.LaborCost = 0;
                contact.ShippingCost = 0;
                contact.MiscCost = 0;
                contact.TaxIncentives = 0;
                contact.ProductMargin = 0;
                db.Proposal.Add(contact);
                var task = db.SaveChangesAsync();
                await task;
                GlobalClass.ProposalGuid = contact.ProposalKey;
                Session["GlobalMessege"] = "Proposal has been Created successfully.";
               
                return Content("success");
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }
        public ActionResult GetProposal([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel)
        {
            IQueryable<Proposal> query = db.Proposal.Where(m => m.ProjectKey == GlobalClass.Project.ProjectKey && m.IsDelete == false);
            var totalCount = query.Count();

            // searching and sorting
            query = Search(requestModel, searchViewModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(asset => new
            {
                ProjectKey = asset.ProjectKey,
                ProposalKey = asset.ProposalKey,
                ProposalName =asset.ProposalName,
                TotalPrice=asset.ProjectDescription,
                Incentives=asset.Legal,
                NetPrice = asset.Legal,
                AnnualSaving = asset.Legal,
                ROI = asset.Legal
            }).ToList();
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }

        private IQueryable<Proposal> Search(IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel, IQueryable<Proposal> query)
        {
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.ProposalName.ToUpper().Contains(value.ToUpper()));
            }

            var filteredCount = query.Count();

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            query = query.OrderBy(orderByString == string.Empty ? "ProposalName asc" : orderByString);

            return query;

        }

        public ActionResult Summary(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id);
                    SummaryClass model = new SummaryClass();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.TotalProjectSaving = 0;
                    model.AnnualCostSaving = 0;
                    model.AnnualEnergySaving = 0;
                    model.ProductCosts = 0;
                    model.LaborCosts = 0;
                    model.ShippingCosts = 0;
                    model.MiscProductCost = 0;
                    model.EstimatedSalesTax = 0;
                    model.CostOfGoodsSold = 0;
                    model.NetSavings = 0;
                    model.SimplePaybackYears = 0;
                    model.SimpleROI = 0;
                    model.GrossMargin = 0;
                    model.TotalProjectPrice = 0;
                    model.EstimatedIncentiveRebate = 0;
                    model.NetProjectPrice = 0;
                    model.CostOfWaitingOneMonth = 0;
                    model.CostOfWaitingOneYear = 0;
                    model.CostOfWaitingFiveYear = 0;
                   
                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "MgtProject", "Created"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public ActionResult Cost(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id);
                    SummaryClass model = new SummaryClass();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.TotalProjectSaving = 0;
                    model.AnnualCostSaving = 0;
                    model.AnnualEnergySaving = 0;
                    model.ProductCosts = 0;
                    model.LaborCosts = 0;
                    model.ShippingCosts = 0;
                    model.MiscProductCost = 0;
                    model.EstimatedSalesTax = 0;
                    model.CostOfGoodsSold = 0;
                    model.NetSavings = 0;
                    model.SimplePaybackYears = 0;
                    model.SimpleROI = 0;
                    model.GrossMargin = 0;
                    model.GrossMarginPercentage = 0;
                    model.TotalProjectPrice = 0;
                    model.EstimatedIncentiveRebate = 0;
                    model.NetProjectPrice = 0;
                    model.CostOfWaitingOneMonth = 0;
                    model.CostOfWaitingOneYear = 0;
                    model.CostOfWaitingFiveYear = 0;

                    model.MarkupPercentage = p.MarkupPercentage;
                    model.LaborCost = p.LaborCost;
                    model.ShippingCost = p.ShippingCost;
                    model.MiscCost = p.MiscCost;
                    model.TaxIncentives = p.TaxIncentives;
                    model.InstallationLaborCharge = p.InstallationRates;
                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "MgtProject", "Created"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditCost(SummaryClass model)
        {
            try
            {
                Proposal obj = db.Proposal.Find(model.ProposalKey);
                if (model.MarkupPercentage == null) model.MarkupPercentage = 0;
                if (model.LaborCost == null) model.LaborCost = 0;
                if (model.ShippingCost == null) model.ShippingCost = 0;
                if (model.MiscCost == null) model.MiscCost = 0;
                if (model.TaxIncentives == null) model.TaxIncentives = 0;
                if (model.InstallationLaborCharge == null) model.InstallationLaborCharge = 0;

                obj.MarkupPercentage = model.MarkupPercentage;
                obj.LaborCost = model.LaborCost;
                obj.ShippingCost = model.ShippingCost;
                obj.MiscCost = model.MiscCost;
                obj.TaxIncentives = model.TaxIncentives;
                obj.InstallationRates = model.InstallationLaborCharge;


                var task = db.SaveChangesAsync();
                await task;
           
                Session["GlobalMessege"] = "Proposal has been Updated successfully.";

                // return Content("success");
                return RedirectToAction("Cost","MgtProposal",new { id=model.ProposalKey});
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
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