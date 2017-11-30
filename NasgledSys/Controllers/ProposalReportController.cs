using NasgledSys.EM;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class ProposalReportController : BaseController
    {
        public ActionResult Report(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ProposalReportViewModel model = new ProposalReportViewModel();

                    model.ProposalKey = id;

                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);

                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;


                    IQueryable<ReportTemplate> reportTemplatequery = db.ReportTemplate;

                    var reportTemplateData = reportTemplatequery.Select(asset => new ReportTemplateModel()
                    {
                        TemplateKey = asset.TemplateKey,
                        FactorName = asset.FactorName,
                        TLevel = asset.TLevel
                    }).OrderBy(m=>m.TLevel).ToList();

                    model.FromReportTemplateList = reportTemplateData;


                    model.ToReportTemplateList = new List<ReportTemplateModel>();

                    IQueryable<ProposalTemplate> proposalTemplatequery = db.ProposalTemplate.Where(m=>m.ProposalKey == model.ProposalKey);

                    var proposalTemplateData = proposalTemplatequery.Select(asset => new ReportTemplateModel()
                    {
                        TemplateKey = asset.ProposalKey,
                        FactorName = asset.FactorName,
                        TLevel = asset.DisplayIndex
                    }).OrderBy(m => m.TLevel).ToList();


                    model.ToReportTemplateList = proposalTemplateData;


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
        [ValidateInput(false)]
        public ActionResult Report(ProposalReportViewModel model)
        {

            if (GlobalClass.MasterSession)
            {
                try
                {
                    string[] splitProposalNameList;
                    string m_string = UtilityExtention.StripHTML(model.DropedHTML);

                    splitProposalNameList = m_string.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    List<string> proposalList = new List<string>();

                    foreach (var item in splitProposalNameList)
                    {
                        if (!string.IsNullOrWhiteSpace(item.ToString()))
                        {
                            proposalList.Add(item.ToString().TrimStart().TrimEnd());
                        }
                    }

                    List<ProposalTemplate> proposalTemplatequery = db.ProposalTemplate.Where(m => m.ProposalKey == model.ProposalKey).ToList();

                    if (proposalTemplatequery.Count>0)
                    {
                        db.ProposalTemplate.RemoveRange(db.ProposalTemplate.Where(c => c.ProposalKey == model.ProposalKey));
                        db.SaveChanges();
                    }

                    List<ProposalTemplateModel> list = new List<ProposalTemplateModel>();

                    int labelIndex = 1;
                    foreach (var item in proposalList)
                    {
                        ProposalTemplateModel proposalModel = new ProposalTemplateModel();

                        proposalModel.ReportKey = Guid.NewGuid();
                        proposalModel.ProjectKey = GlobalClass.Project.ProjectKey;
                        proposalModel.ProposalKey = model.ProposalKey;
                        proposalModel.FactorName = item.ToString();
                        proposalModel.DisplayIndex = labelIndex;

                        list.Add(proposalModel);

                        labelIndex++;
                    }

                    if (list.Count>0)
                    {
                        List<ProposalTemplate> proposalTemplateentList = new List<ProposalTemplate>();
                        proposalTemplateentList = list.Select(n => EM_ProposalTemplate.ConvertToEntity(n)).ToList<ProposalTemplate>();

                        // Batch Insert

                        using (var db = new NasgledDBEntities())
                        {
                            // Insert  
                            var template = proposalTemplateentList;
                            db.ProposalTemplate.AddRange(template);
                            db.SaveChanges();
                        }
                    }

                   

                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);

                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;

                    IQueryable<ReportTemplate> reportTemplatequery = db.ReportTemplate;

                    var reportTemplateData = reportTemplatequery.Select(asset => new ReportTemplateModel()
                    {
                        TemplateKey = asset.TemplateKey,
                        FactorName = asset.FactorName,
                        TLevel = asset.TLevel
                    }).OrderBy(m => m.TLevel).ToList();

                    model.FromReportTemplateList = reportTemplateData;


                    model.ToReportTemplateList = new List<ReportTemplateModel>();

                    IQueryable<ProposalTemplate> proposalTemplatequeryy = db.ProposalTemplate.Where(m => m.ProposalKey == model.ProposalKey);

                    var proposalTemplateData = proposalTemplatequeryy.Select(asset => new ReportTemplateModel()
                    {
                        TemplateKey = asset.ProposalKey,
                        FactorName = asset.FactorName,
                        TLevel = asset.DisplayIndex
                    }).OrderBy(m => m.TLevel).ToList();


                    model.ToReportTemplateList = proposalTemplateData;

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
    }
}