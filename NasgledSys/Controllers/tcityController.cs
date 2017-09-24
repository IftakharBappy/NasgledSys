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
    public class tcityController : Controller
    {
        // GET: tcity
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    ViewBag.StateCode = new SelectList(db.StateList.Where(m => m.IsDelete == false).OrderBy(m => m.StateName), "PKey", "StateName");
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
        [HttpPost]
        public async Task<ActionResult> Index(CityViewModel cityVM)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    cityVM.IsDelete = false;
                    if (!ModelState.IsValid)
                    {
                        return View(cityVM);
                    }

                    CityList asset = EM_Role.ConvertCityToEntity(cityVM);

                    CityList exsisting = db.CityList.Find(asset.CityKey);
                    if (exsisting == null) db.CityList.Add(asset);
                    else
                    {
                        exsisting.CityName = asset.CityName;
                        exsisting.StateCode = asset.StateCode;
                    }
                    var task = db.SaveChangesAsync();
                    await task;

                    if (task.Exception != null)
                    {
                        ModelState.AddModelError("", "Unable to add the City");
                    }
                    ViewBag.StateCode = new SelectList(db.StateList.Where(m => m.IsDelete == false).OrderBy(m => m.StateName), "PKey", "StateName");
                    return View(cityVM);
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
            var asset = db.CityList.Find(id);
            CityViewModel obj = EM_Role.ConvertToCityModel(asset);
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }




        public async Task<ActionResult> Delete(long id)
        {
            var asset = db.CityList.Find(id);

            asset.IsDelete = true;

            var task = db.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to Delete the City");
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
            IQueryable<CityList> query = db.CityList.Where(m => m.IsDelete == false);
            var totalCount = query.Count();

            // searching and sorting
            query = Search(requestModel, searchViewModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(asset => new
            {
                CityKey = asset.CityKey,
                CityName = asset.CityName,
                StateCode = asset.StateCode,
                IsDelete = asset.IsDelete,
                StateName=asset.StateList.StateName
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }

        private IQueryable<CityList> Search(IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel, IQueryable<CityList> query)
        {
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.CityName.ToUpper().Contains(value.ToUpper()) || p.StateList.StateName.ToUpper().Contains(value.ToUpper()));
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

            query = query.OrderBy(orderByString == string.Empty ? "CityName asc" : orderByString);

            return query;

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