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
    public class CityController : Controller
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        // GET: City
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new CityViewModel();
            return View("_CreatePartial", model);
        }


        [HttpPost]
        public async Task<ActionResult> Create(CityViewModel cityVM)
        {
            if (!ModelState.IsValid)
                return View("_CreatePartial", cityVM);

            CityList asset = EM_City.ConvertToEntity(cityVM);  

            db.CityList.Add(asset);
            var task = db.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to add the City");
                return View("_CreatePartial", cityVM);
            }

            return Content("success");
        }



        public ActionResult Edit(int id)
        {
            var asset = db.CityList.FirstOrDefault(x => x.CityKey == id);

            CityViewModel assetViewModel = EM_City.ConvertToModel(asset);

            if (Request.IsAjaxRequest())
                return PartialView("_EditPartial", assetViewModel);
            return View(assetViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(CityViewModel cityVM)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(Request.IsAjaxRequest() ? "_EditPartial" : "Edit", cityVM);
            }

            CityList asset = EM_City.ConvertToEntity(cityVM);

            db.CityList.Attach(asset);
            db.Entry(asset).State = EntityState.Modified;
            var task = db.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to update the City");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(Request.IsAjaxRequest() ? "_EditPartial" : "Edit", cityVM);
            }

            if (Request.IsAjaxRequest())
            {
                return Content("success");
            }

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Details(int id)
        {
            var asset = await db.CityList.FirstOrDefaultAsync(x => x.CityKey == id);
            var cityVM = EM_City.ConvertToModel(asset);

            if (Request.IsAjaxRequest())
                return PartialView("_detailsPartial", cityVM);

            return View(cityVM);
        }


        public ActionResult Delete(int id)
        {
            var asset = db.CityList.FirstOrDefault(x => x.CityKey == id);

            CityViewModel assetViewModel = EM_City.ConvertToModel(asset);

            if (Request.IsAjaxRequest())
                return PartialView("_DeletePartial", assetViewModel);
            return View(assetViewModel);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteAsset(int AssetID)
        {
            var asset = new CityList { CityKey = AssetID };
            db.CityList.Attach(asset);
            db.CityList.Remove(asset);

            var task = db.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to Delete the Asset");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                CityViewModel cityVM = EM_City.ConvertToModel(asset);
                return View(Request.IsAjaxRequest() ? "_DeletePartial" : "Delete", cityVM);
            }

            if (Request.IsAjaxRequest())
            {
                return Content("success");
            }

            return RedirectToAction("Index");

        }

        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel)
        {
            IQueryable<CityList> query = db.CityList;
            var totalCount = query.Count();

            // searching and sorting
            query = SearchCity(requestModel, searchViewModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(asset => new
            {
                CityKey = asset.CityKey,
                CityName = asset.CityName,
                StateName = asset.StateList.StateName,
                StateCode = asset.StateList.StateCode
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }

        private IQueryable<CityList> SearchCity(IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel, IQueryable<CityList> query)
        {
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.CityName.Contains(value) || p.StateList.StateName.Contains(value));
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
  
    }
}