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
    }
}