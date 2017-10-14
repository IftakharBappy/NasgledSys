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
            ProposalReportViewModel model = new ProposalReportViewModel();

            model.ProposalKey = id;

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
    }
}