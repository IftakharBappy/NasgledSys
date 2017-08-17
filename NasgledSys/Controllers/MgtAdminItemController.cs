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
    public class MgtAdminItemController : Controller
    {
        // GET: MgtAdminItem
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
        public ActionResult Catelogue()
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
        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel)
        {
            IQueryable<MainProduct> query = db.MainProduct;
            var totalCount = query.Count();

            // searching and sorting
            query = SearchCity(requestModel, searchViewModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(asset => new
            {
                FixtureKey = asset.FixtureKey,
                ItemTypeKey = asset.ItemTypeKey,
                CategoryKey = asset.CategoryKey,
                SubcategoryKey = asset.SubcategoryKey,
                CatelogueKey = asset.CatelogueKey,
                ManufacturerKey = asset.ManufacturerKey,
                ProductName = asset.ProductName,
                ModelNo = asset.ModelNo,
                Watt = asset.Watt,
                ThermalEfficacy = asset.ThermalEfficacy,
                CRI = asset.CRI,
                Lumen = asset.Lumen,
                LightApparent = asset.LightApparent,
                Source = asset.Source,
                Brand = asset.Brand,
                LightOutput = asset.LightOutput,
                CCT = asset.CCT,
                Size = asset.Size,
                Location = asset.Location,
                MountingBase = asset.MountingBase,
                Category = asset.ItemCategory.TypeName,
                Catelogue = asset.ItemCatelogue.TypeName,
                Subcategory = asset.SubcategoryKey==null?" ":asset.ItemSubcategory.TypeName,
                Type = asset.ItemType.TypeName,
                Manufacturer = asset.Manufacturer.ManufacturerName
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }

        private IQueryable<MainProduct> SearchCity(IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel, IQueryable<MainProduct> query)
        {
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Manufacturer.ManufacturerName.ToLower().Contains(value.ToLower()));
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

            query = query.OrderBy(orderByString == string.Empty ? "ProductName asc" : orderByString);

            return query;

        }
    }
}