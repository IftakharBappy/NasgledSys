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
    public class MgtRoleController : Controller
    {
        // GET: MgtRole
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(RoleViewModel cityVM)
        {
            cityVM.IsDelete = false;
            if (!ModelState.IsValid)
            {               
                return View( cityVM);
            }

            UserRole asset = EM_Role.ConvertToEntity(cityVM);

            UserRole exsisting = db.UserRole.Find(asset.RoleKey);
            if(exsisting==null)db.UserRole.Add(asset);
            else
            {
                exsisting.RoleName = asset.RoleName;
                exsisting.Rlevel = asset.Rlevel;
            }
            var task = db.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to add the Role");              
            }

            return View(cityVM);
        }
        public ActionResult Edit(Guid id)
        {
           
            JsonResult result = new JsonResult();
            var asset = db.UserRole.Find(id);
            RoleViewModel obj = EM_Role.ConvertToModel(asset);
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }


      

        public async Task<ActionResult> Delete(Guid id)
        {
            var asset = db.UserRole.Find(id);

            asset.IsDelete = true;

            var task = db.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to Delete the Role");
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
            IQueryable<UserRole> query = db.UserRole.Where(m=>m.IsDelete==false);
            var totalCount = query.Count();

            // searching and sorting
            query = Search(requestModel, searchViewModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(asset => new
            {
                RoleKey = asset.RoleKey,
                RoleName = asset.RoleName,
                Rlevel = asset.Rlevel,
                IsDelete = asset.IsDelete
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }

        private IQueryable<UserRole> Search(IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel, IQueryable<UserRole> query)
        {
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.RoleName.ToUpper().Contains(value.ToUpper()));
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

            query = query.OrderBy(orderByString == string.Empty ? "RoleName asc" : orderByString);

            return query;

        }
    }
}