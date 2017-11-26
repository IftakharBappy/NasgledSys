using System;
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
using NasgledSys.Calculations;
using System.Linq.Dynamic;

namespace NasgledSys.Controllers
{
    public class MgtProposalController : Controller
    {
        // GET: MgtProposal
        private NasgledDBEntities db = new NasgledDBEntities();
        private CalculateProposalCosts Costs = new CalculateProposalCosts();
        public JsonResult GetFinanceCompanyList()
        {
            var list = (from x in db.FinanceCompany
                        select new
                        {
                            FinancingCompanyName = x.FinancingCompanyName,
                            FinanceKey =x.FinanceKey,
                            IntroText = x.IntroText,
                            Hyperlink =x.Hyperlink,
                            AboutUsLink = x.AboutUsLink
                        }).OrderBy(m => m.FinancingCompanyName).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Proposal model = db.Proposal.Find(id);
                    model.IsDelete = true;
                    return RedirectToAction("Index", "MgtProposal",new { id=model.ProjectKey});
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
        public async Task<ActionResult> Create(string ProposalName, string PreparedByUser,decimal? SalesTax)
        {
            try
            {
                Project proj = db.Project.Find(GlobalClass.Project.ProjectKey);
                Proposal contact = new Proposal();
                contact.ProposalKey = Guid.NewGuid();
                contact.ProposalName = ProposalName;
                contact.PreparedByText = PreparedByUser;
                contact.ProjectKey = GlobalClass.Project.ProjectKey;
                contact.CreateDate = System.DateTime.Now;
                contact.PreparedBy = GlobalClass.LoggedInUser.ProfileKey;
                contact.IsDelete = false;
                contact.MarkupPercentage =proj.MarkupPercentage;
                contact.LaborCost = proj.LaborCost;
                contact.ShippingCost = proj.ShippingCost;
                contact.MiscCost = proj.MiscCost;
                contact.TaxIncentives = proj.TaxIncentives;
                contact.ProductMargin = proj.ProductMargin;
                contact.TaxInput = SalesTax;
                db.Proposal.Add(contact);
                var task = db.SaveChangesAsync();
                await task;
                GlobalClass.ProposalGuid = contact.ProposalKey;
                Session["GlobalMessege"] = "Proposal has been Created successfully.";
                Session["counter"] = 1;

                return Content("success");
                //return RedirectToAction("Summary", "MgtProposal",new { id=contact.ProposalKey});
            }
            catch (Exception ex)
            {
                return Content("Error");
            }
        }

        public JsonResult GetProposalProductList()
        {
            var query = db.Proposal.Where(m => m.ProjectKey == GlobalClass.Project.ProjectKey && m.IsDelete == false);
            List<ProposalViewClass> data = new List<ProposalViewClass>();
            foreach (var item in query)
            {
                SummaryClass s = GetAllCosts(item.ProposalKey);
                ProposalViewClass obj = new ProposalViewClass();
                obj.ProjectKey = item.ProjectKey;
                obj.ProposalKey = item.ProposalKey;
                obj.ProposalName = item.ProposalName;
                obj.TotalPrice = (s.TotalProjectPrice).ToString();
                obj.Incentives = (s.EstimatedIncentiveRebate).ToString();
                obj.NetPrice = (s.NetProjectPrice).ToString();
                obj.AnnualSaving = (s.AnnualCostSaving).ToString();
                obj.ROI = (s.SimpleROIstring).ToString();
                data.Add(obj);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
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

            List<ProposalViewClass> data = new List<ProposalViewClass>();
            foreach(var item in query)
            {
                ProposalViewClass obj = new ProposalViewClass();
                SummaryClass s = GetAllCosts(item.ProposalKey);
                obj.ProjectKey = item.ProjectKey;
                obj.ProposalKey = item.ProposalKey;
                obj.ProposalName = item.ProposalName;
                obj.TotalPrice = (Convert.ToDecimal(s.TotalProjectPrice)).ToString("0.000");
                obj.Incentives = (Convert.ToDecimal(s.EstimatedIncentiveRebate)).ToString("0.000"); 
                obj.NetPrice = (Convert.ToDecimal(s.NetProjectPrice)).ToString("0.000"); 
                obj.AnnualSaving = (Convert.ToDecimal(s.AnnualCostSaving)).ToString("0.000"); 
                obj.ROI = (Convert.ToDecimal(s.SimpleROIstring)).ToString("0.000"); 
                data.Add(obj);
            }
           
            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }
        public SummaryClass GetAllCosts(Guid id)
        {
           
                   
                    Proposal p = db.Proposal.Find(id);
                    if (p.ProductMargin == null) p.ProductMargin = 0;
            if (p.TaxInput == null) p.TaxInput = 0;
            SummaryClass model = new SummaryClass();
                   
                    model.ProductCosts = Costs.GetProductCost(p.ProposalKey);
                    model.LaborCosts = Costs.GetLaborCost(p.ProposalKey);
                    model.ShippingCosts = Costs.GetShippingCost(p.ProposalKey);
                    model.MiscProductCost = Costs.GetMiscCost(p.ProposalKey);

                    model.EstimatedSalesTax = (model.ProductCosts + model.MiscProductCost) * (p.TaxInput / 100);
            model.CostOfGoodsSold = model.ProductCosts + model.LaborCosts + model.ShippingCosts + model.MiscProductCost + model.EstimatedSalesTax;
                    model.NetSavings = 0;
                    model.SimplePaybackYears = 0;
                    model.SimpleROI = 0;
            if (p.ProductMargin > 0)
            {
                model.GrossMarginPercentage = p.ProductMargin;
                model.GrossMargin = Costs.GetGrossMarginAmount(p.ProductMargin, model.CostOfGoodsSold);
            }
            else
            {
                model.GrossMarginPercentage = p.MarkupPercentage;
                model.GrossMargin = Costs.GetMarkupAmount(p.MarkupPercentage, model.CostOfGoodsSold);
            }
            
                    model.TotalProjectPrice = model.GrossMargin + model.CostOfGoodsSold;
                    model.EstimatedIncentiveRebate = Costs.GetEstimatedIncentiveRebate(p.ProposalKey);
                    model.NetProjectPrice = model.TotalProjectPrice - model.EstimatedIncentiveRebate;
                 

            ////////////////////////////////////////////////
            model.TotalProjectSaving = Costs.GetEstimatedIncentiveRebate(p.ProposalKey);
            model.AnnualCostSaving = Costs.GetExistingCost(p.ProjectKey) - Costs.GetNewCost(p.ProjectKey);
            model.AnnualEnergySaving = Costs.GetExistingEnergy(p.ProjectKey) - Costs.GetNewEnergy(p.ProjectKey);
            
           
          
           
            /////////////////////////////////////
            model.NetSavings = model.TotalProjectSaving + model.AnnualCostSaving;
            model.SimplePaybackYearsString = ((decimal)(model.NetProjectPrice / model.NetSavings)).ToString("0.##");
            model.SimplePaybackYears = (decimal)(model.NetProjectPrice / model.NetSavings);
            model.SimpleROIstring = ((decimal)(model.SimplePaybackYears / 100)).ToString("0.00");


            return model;
               
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
                    if (p.ProductMargin == null) p.ProductMargin = 0;
                    if (p.TaxInput == null) p.TaxInput = 0;
                    SummaryClass model = new SummaryClass();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.TotalProjectSaving = Costs.GetProjectSaving(p.ProposalKey); 
                    model.AnnualCostSaving = Costs.GetExistingCost(p.ProjectKey)- Costs.GetNewCost(p.ProjectKey);
                    model.AnnualEnergySaving = Costs.GetExistingEnergy(p.ProjectKey) - Costs.GetNewEnergy(p.ProjectKey);
                    model.ProductCosts = Costs.GetProductCost(p.ProposalKey);
                    model.LaborCosts = Costs.GetLaborCost(p.ProposalKey);
                    model.ShippingCosts = Costs.GetShippingCost(p.ProposalKey);
                    model.MiscProductCost = Costs.GetMiscCost(p.ProposalKey);

                    model.EstimatedSalesTax = (model.ProductCosts + model.MiscProductCost) * (p.TaxInput / 100);

                    model.CostOfGoodsSold = model.ProductCosts + model.LaborCosts + model.ShippingCosts + model.MiscProductCost + model.EstimatedSalesTax;
                    if (p.ProductMargin > 0)
                    {
                        model.GrossMarginPercentage = p.ProductMargin;
                        model.GrossMargin = Costs.GetGrossMarginAmount(p.ProductMargin, model.CostOfGoodsSold);
                    }
                    else
                    {
                        model.GrossMarginPercentage = p.MarkupPercentage;
                        model.GrossMargin = Costs.GetMarkupAmount(p.MarkupPercentage, model.CostOfGoodsSold);
                    }


                   
                    model.TotalProjectPrice = model.GrossMargin + model.CostOfGoodsSold;
                    model.EstimatedIncentiveRebate = Costs.GetEstimatedIncentiveRebate(p.ProposalKey);
                    model.NetProjectPrice = model.TotalProjectPrice - model.EstimatedIncentiveRebate;                   

                    /////////////////////////////////////
                    model.NetSavings = model.TotalProjectSaving+ model.AnnualCostSaving;
                    model.SimplePaybackYearsString = ((decimal)(model.NetProjectPrice / model.NetSavings)).ToString("0.##");
                    model.SimplePaybackYears = (decimal)(model.NetProjectPrice / model.NetSavings);
                    model.SimpleROIstring = ((decimal)(model.SimplePaybackYears / 100)).ToString("0.00");

                    ///////////////////////////////////////

                    model.CostOfWaitingOneMonth = model.AnnualCostSaving / 12;
                    model.CostOfWaitingOneYear = model.AnnualCostSaving; 
                    model.CostOfWaitingFiveYear = model.AnnualCostSaving * 5;

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

        public ActionResult Copy(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                   
                    Proposal p = db.Proposal.Find(id);
                    NasgledDBEntities bc = new NasgledDBEntities();
                    Proposal obj = new Proposal();
                    obj.ProposalKey = Guid.NewGuid();
                    obj.ProposalName = p.ProposalName + " _Copy";
                    obj.ProjectKey = p.ProjectKey;
                    obj.PreparedBy = p.PreparedBy;
                    obj.CreateDate = System.DateTime.Now;
                    obj.Pic = p.Pic;
                    obj.PicType = p.PicType;
                    obj.Picname = p.Picname;
                    obj.PageInfo = p.PageInfo;
                    obj.IntroductionText = p.IntroductionText;
                    obj.Team = p.Team;
                    obj.Legal = p.Legal;
                    obj.Disclaimer = p.Disclaimer;
                    obj.Reference = p.Reference;
                    obj.Billing = p.Billing;
                    obj.Notes = p.Notes;
                    obj.Summary = p.Summary;
                    obj.Estimal = p.Estimal;
                    obj.ProjectDescription = p.ProjectDescription;
                    obj.IsDelete = false;
                    obj.PreparedByText = p.PreparedByText;
                    obj.MarkupPercentage = p.MarkupPercentage;
                    obj.LaborCost = p.LaborCost;
                    obj.ShippingCost = p.ShippingCost;
                    obj.MiscCost = p.MiscCost;
                    obj.TaxIncentives = p.TaxIncentives;
                    obj.ProductMargin = p.ProductMargin;
                    obj.Incentives = p.Incentives;
                    obj.InstallationRates = p.InstallationRates;
                    obj.TaxInput = p.TaxInput;
                    bc.Proposal.Add(obj);
                    bc.SaveChanges();
                    var temp = from x in db.ProposalLoanTerms where x.ProposalKey == id select x;
                    if (temp.Count() > 0)
                    {
                        foreach(var item in temp)
                        {
                            bc = new NasgledDBEntities();
                            ProposalLoanTerms lt = new ProposalLoanTerms();
                            lt.LoanTermKey = Guid.NewGuid();
                            lt.ProjectKey = item.ProjectKey;
                            lt.ProposalKey = obj.ProposalKey;
                            lt.PercentageOfProjectCost = item.PercentageOfProjectCost;
                            lt.FinancingInterestRate = item.FinancingInterestRate;
                            lt.LoanMonth = item.LoanMonth;
                            bc.ProposalLoanTerms.Add(lt);
                            bc.SaveChanges();
                        }
                    }
                    bc.Dispose();

                    return RedirectToAction("Index",new { id=p.ProjectKey}) ;
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
                    if (p.ProductMargin == null) p.ProductMargin = 0;
                    if (p.TaxInput == null) p.TaxInput = 0;
                    SummaryClass model = new SummaryClass();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.TotalProjectSaving = Costs.GetProjectSaving(p.ProposalKey);
                    model.AnnualCostSaving = 0;
                    model.AnnualEnergySaving = 0;
                    model.ProductCosts = Costs.GetProductCost(p.ProposalKey);
                    model.LaborCosts = Costs.GetLaborCost(p.ProposalKey); 
                    model.ShippingCosts = Costs.GetShippingCost(p.ProposalKey);
                    model.MiscProductCost = Costs.GetMiscCost(p.ProposalKey);

                    model.EstimatedSalesTax = (model.ProductCosts+ model.MiscProductCost)*(p.TaxInput/100);

                    model.CostOfGoodsSold = model.ProductCosts+ model.LaborCosts+ model.ShippingCosts+ model.MiscProductCost+ model.EstimatedSalesTax;
                    model.NetSavings = 0;
                    model.SimplePaybackYears = 0;
                    model.SimpleROI = 0;
                    if (p.ProductMargin > 0)
                    {
                        model.GrossMarginPercentage = p.ProductMargin;
                        model.GrossMargin = Costs.GetGrossMarginAmount(p.ProductMargin, model.CostOfGoodsSold);
                    }
                    else
                    {
                        model.GrossMarginPercentage = p.MarkupPercentage;
                        model.GrossMargin = Costs.GetMarkupAmount(p.MarkupPercentage, model.CostOfGoodsSold);
                    }
                    model.TotalProjectPrice = model.GrossMargin+model.CostOfGoodsSold;
                    model.EstimatedIncentiveRebate = Costs.GetEstimatedIncentiveRebate(p.ProposalKey);
                    model.NetProjectPrice = model.TotalProjectPrice- model.EstimatedIncentiveRebate;
                    model.CostOfWaitingOneMonth = 0;
                    model.CostOfWaitingOneYear = 0;
                    model.CostOfWaitingFiveYear = 0;

                    if (p.TaxInput == null) model.SalesTax = 0;
                    else model.SalesTax = p.TaxInput;
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

        public ActionResult Notes(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id);
                    NoteClass model = new NoteClass();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.Notes = p.Notes;
                   
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
        public ActionResult Notes(NoteClass model)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(model.ProposalKey);
                    p.Notes = model.Notes;
                    db.SaveChanges();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    model.Notes = p.Notes;
                    Session["GlobalMessege"] = "Note has been saved.";
                    Session["counter"] = 1;
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

        public ActionResult Finance(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);
                    Proposal p = db.Proposal.Find(id);
                    ProposalLoanTerms terms = db.ProposalLoanTerms.FirstOrDefault(m=>m.ProposalKey==id);
                   
                    FinanceClass model = new FinanceClass();
                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;
                    model.ProjectKey = p.ProjectKey;
                    model.ProposalKey = p.ProposalKey;
                    if (terms == null)
                    {
                        model.PercentageOfProjectCost = 0;
                        model.FinancingInterestRate = 0;
                        model.LoanMonth = 0;
                    }
                    else
                    {
                        model.PercentageOfProjectCost =terms.PercentageOfProjectCost;
                        model.FinancingInterestRate = terms.FinancingInterestRate;
                        model.LoanMonth =terms.LoanMonth;
                    }
                    model.Principle = 0;
                    model.InterestRate = 0;
                    model.DisplayLoanMonth = 0;
                    model.MonthlyPayment = 0;
                    model.TotalPayment = 0;
                    model.CostSavings = 0;                   
                    model.MonthlyCashFlow = 0;
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
        public async Task<ActionResult> EditLoanTerms(FinanceClass model)
        {
            try
            {
                ProposalLoanTerms terms = db.ProposalLoanTerms.FirstOrDefault(m => m.ProposalKey == model.ProposalKey);

              
                if (terms == null)
                {
                    ProposalLoanTerms abc = new ProposalLoanTerms();
                    abc.ProposalKey = model.ProposalKey;
                    abc.ProjectKey = GlobalClass.Project.ProjectKey;
                    abc.LoanTermKey = Guid.NewGuid();

                    abc.PercentageOfProjectCost = model.PercentageOfProjectCost;
                    abc.FinancingInterestRate = model.FinancingInterestRate;
                    abc.LoanMonth = model.LoanMonth;
                    db.ProposalLoanTerms.Add(abc);
                    var task = db.SaveChangesAsync();
                    await task;
                }
                else
                {

                    terms.PercentageOfProjectCost = model.PercentageOfProjectCost;
                    terms.FinancingInterestRate = model.FinancingInterestRate;
                    terms.LoanMonth = model.LoanMonth;                   
                    var task = db.SaveChangesAsync();
                    await task;
                }              
              
                Session["GlobalMessege"] = "Proposal has been updated successfully.";
                Session["counter"] = 1;
                return Content("success");
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
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
                if (model.SalesTax == null) model.SalesTax = 0;

                obj.MarkupPercentage = model.MarkupPercentage;
                obj.TaxInput = model.SalesTax;
                obj.LaborCost = model.LaborCost;
                obj.ShippingCost = model.ShippingCost;
                obj.MiscCost = model.MiscCost;
                obj.TaxIncentives = model.TaxIncentives;
                obj.InstallationRates = model.InstallationLaborCharge;


                var task = db.SaveChangesAsync();
                await task;
           
                Session["GlobalMessege"] = "Proposal has been Updated successfully.";
                Session["counter"] = 1;
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