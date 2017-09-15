using DataTables.Mvc;
using NasgledSys.Models;
using System;
using System.Linq.Dynamic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;
using NasgledSys.EM;

namespace NasgledSys.Controllers
{
    public class MgtStateController : Controller
    {
        // GET: MgtState
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            try
            {
                
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }

        [HttpPost]
        public async Task<ActionResult> Index(string sname, string scode,int? PKey)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (!ModelState.IsValid)
            {
                return View();
            }
            if (PKey == null)
            {
                if (string.IsNullOrEmpty(scode)) scode = sname;
                StateList asset = new StateList();
                asset.IsDelete = false;
                asset.StateCode = scode;
                asset.StateName = sname;
                db.StateList.Add(asset);
            }
            else
            {
                StateList asset =db.StateList.Find(PKey);               
                asset.StateCode = scode;
                asset.StateName = sname;
            }
            var task = db.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to add the State");
            }
                    return View();
                }
                catch (Exception e)
                {
                    return View("Error", new HandleErrorInfo(e, "Home", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Sorry, your Session has Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "Login"));
            }
        }
        public ActionResult Edit(long id)
        {

            JsonResult result = new JsonResult();
            StateList asset = db.StateList.Find(id);
            StateModel obj = EM_Role.ConvertToStateModel(asset);
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }




        public async Task<ActionResult> Delete(long id)
        {
            var asset = db.StateList.Find(id);

            asset.IsDelete = true;

            var task = db.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to Delete the State");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(Request.IsAjaxRequest() ? "Index" : "Index");
            }

            if (Request.IsAjaxRequest())
            {
                return Content("Data has been removed successfully.");
            }

            return RedirectToAction("Index");

        }

        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel)
        {
            IQueryable<StateList> query = db.StateList.Where(m => m.IsDelete == false);
            var totalCount = query.Count();

            // searching and sorting
            query = Search(requestModel, searchViewModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(asset => new
            {
                PKey = asset.PKey,
                StateName = asset.StateName,
                StateCode = asset.StateCode,
                IsDelete = asset.IsDelete
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }

        private IQueryable<StateList> Search(IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel, IQueryable<StateList> query)
        {
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.StateName.ToUpper().Contains(value.ToUpper()));
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

            query = query.OrderBy(orderByString == string.Empty ? "StateName asc" : orderByString);

            return query;

        }
    }
}