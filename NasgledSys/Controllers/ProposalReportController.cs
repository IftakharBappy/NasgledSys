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


                    IQueryable<ReportTemplate> query = db.ReportTemplate;

                    var data = query.Select(asset => new ReportTemplateModel()
                    {
                        TemplateKey = asset.TemplateKey,
                        FactorName = asset.FactorName,
                        TLevel = asset.TLevel
                    }).OrderBy(m=>m.TLevel).ToList();

                    model.FromReportTemplateList = data;
                    model.ToReportTemplateList = new List<ReportTemplateModel>();

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
        public ActionResult Report(FormCollection fomr,ProposalReportViewModel model)
        {

            if (GlobalClass.MasterSession)
            {
                try
                {
                    ClientCompany company = db.ClientCompany.Find(GlobalClass.Project.CompanyKey);

                    model.CompanyKey = company.ClientCompanyKey;
                    model.CompanyName = company.CompanyName;

                    IQueryable<ReportTemplate> query = db.ReportTemplate;

                    var data = query.Select(asset => new ReportTemplateModel()
                    {
                        TemplateKey = asset.TemplateKey,
                        FactorName = asset.FactorName,
                        TLevel = asset.TLevel
                    }).ToList();

                    model.FromReportTemplateList = data;
                    model.ToReportTemplateList = new List<ReportTemplateModel>();

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