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
using System.Threading.Tasks;
namespace NasgledSys.Controllers
{
    public class MgtAreaController : Controller
    {
        // GET: MgtArea
        private NasgledDBEntities db = new NasgledDBEntities();
      
        private ManageProject manage = new ManageProject();
        public ActionResult GetArea([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel)
        {
            IQueryable<Area> query = db.Area.Where(m => m.ProjectKey==GlobalClass.Project.ProjectKey);
            var totalCount = query.Count();

            // searching and sorting
            query = Search(requestModel, searchViewModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(asset => new
            {
                AreaKey = asset.AreaKey,
                AreaName = asset.AreaName,
                SubArea = db.Area.Where(m => m.ParentAreaKey == asset.AreaKey).Count()
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }

        private IQueryable<Area> Search(IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel, IQueryable<Area> query)
        {
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.AreaName.ToUpper().Contains(value.ToUpper()));
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

            query = query.OrderBy(orderByString == string.Empty ? "AreaName asc" : orderByString);

            return query;

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    return View();
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
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