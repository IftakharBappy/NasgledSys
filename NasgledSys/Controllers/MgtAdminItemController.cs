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
using System.Collections.Generic;
using NasgledSys.DAL;
using System.Web;
using System.IO;

namespace NasgledSys.Controllers
{
    public class MgtAdminItemController : Controller
    {
        // GET: MgtAdminItem
        private NasgledDBEntities db = new NasgledDBEntities();
        private ManageMainProduct manage = new ManageMainProduct();
        public ActionResult Create()
        {
            try
            {
                ViewBag.MainItemKey = new SelectList(db.MainProduct.OrderBy(m => m.FixtureKey), "FixtureKey", "ProductName");
                ViewBag.ItemTypeKey = new SelectList(db.ItemType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.ItemTypeKey = new SelectList(db.ItemType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.CategoryKey = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.SubcategoryKey = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
              //  ViewBag.CatelogueKey = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.ManufacturerKey = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName");
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtAdminItem", "Catelogue"));
            }
        }
   
        [HttpPost]
        public ActionResult Create(ItemViewModel model, HttpPostedFileBase file,string Save,string Add)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.FixtureKey = Guid.NewGuid();
                    MainProduct obj = EM_MainProduct.ConvertToEntity(model);
                    if (file != null)
                    {
                        byte[] data = null;
                        using (Stream inputStream = file.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            data = memoryStream.ToArray();
                        }
                        obj.Logo = data;
                        obj.FileType = file.ContentType;
                        obj.FileName = file.FileName;
                    }
                    db.MainProduct.Add(obj);
                   db.SaveChanges();
                   if(!string.IsNullOrEmpty(Save))
                    return RedirectToAction("Details",new { id=obj.FixtureKey});
                    else return RedirectToAction("Create");
                }
                
                ViewBag.MainItemKey = new SelectList(db.MainProduct.OrderBy(m => m.FixtureKey), "FixtureKey", "ProductName",model.MainItemKey);
                ViewBag.ItemTypeKey = new SelectList(db.ItemType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName",model.ItemTypeKey);
                ViewBag.CategoryKey = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName",model.CategoryKey);
                ViewBag.SubcategoryKey = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName",model.SubcategoryKey);
               // ViewBag.CatelogueKey = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName",model.CatelogueKey);
                ViewBag.ManufacturerKey = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName",model.ManufacturerKey);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtAdminItem", "Catelogue"));
            }
        }

        public ActionResult Edit(Guid id)
        {
            try
            {
                MainProduct p = db.MainProduct.Find(id);
                ItemViewModel model = EM_MainProduct.ConvertToModel(p);
                ViewBag.MainItemKey = new SelectList(db.MainProduct.OrderBy(m => m.FixtureKey), "FixtureKey", "ProductName",model.MainItemKey);
                ViewBag.ItemTypeKey = new SelectList(db.ItemType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", model.ItemTypeKey);
                ViewBag.CategoryKey = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", model.CategoryKey);
                ViewBag.SubcategoryKey = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", model.SubcategoryKey);
               // ViewBag.CatelogueKey = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", model.CatelogueKey);
                ViewBag.ManufacturerKey = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName", model.ManufacturerKey);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtAdminItem", "Catelogue"));
            }
        }
        public ActionResult Details(Guid id)
        {
            try
            {
               
                ItemViewModel model = manage.GetroductByID(id);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtAdminItem", "Catelogue"));
            }
        }
        [HttpPost]
        public ActionResult Edit(ItemViewModel model, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    MainProduct obj = EM_MainProduct.ConvertToEntity(model);
                    MainProduct tosave = db.MainProduct.Find(obj.FixtureKey);
                    if (file != null)
                    {
                        byte[] data = null;
                        using (Stream inputStream = file.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            data = memoryStream.ToArray();
                        }
                        tosave.Logo = data;
                        tosave.FileType = file.ContentType;
                        tosave.FileName = file.FileName;
                    }
                    tosave = EM_MainProduct.FillEntityForEdit(tosave,obj);
                    db.SaveChanges();
                    return RedirectToAction("Details",new { id=tosave.FixtureKey});
                }
               
                ViewBag.MainItemKey = new SelectList(db.MainProduct.OrderBy(m => m.FixtureKey), "FixtureKey", "ProductName",model.MainItemKey);
                ViewBag.ItemTypeKey = new SelectList(db.ItemType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", model.ItemTypeKey);
                ViewBag.CategoryKey = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", model.CategoryKey);
                ViewBag.SubcategoryKey = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", model.SubcategoryKey);
              //  ViewBag.CatelogueKey = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", model.CatelogueKey);
                ViewBag.ManufacturerKey = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName", model.ManufacturerKey);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtAdminItem", "Catelogue"));
            }
        }
        public JsonResult GetCatelogue()
        {
            var data = (from asset in db.MainProduct
                        select new ItemViewModel
                        {
                            FixtureKey = asset.FixtureKey,
                            ItemTypeKey = asset.ItemTypeKey,
                            CategoryKey = asset.CategoryKey,
                            SubcategoryKey = asset.SubcategoryKey,
                            TypeCount = asset.TypeCount,
                            ManufacturerKey = asset.ManufacturerKey,
                            ProductName = asset.ProductName,
                            ModelNo = asset.ModelNo,
                            Watt = asset.Watt,
                            ThermalEfficacy = asset.ThermalEfficacy,
                            CRI = asset.CRI,
                            Lumen = asset.Lumen,
                            LampLife = asset.LampLife,
                            LightApparent = asset.LightApparent,
                            Source = asset.Source,
                            Brand = asset.Brand,
                            LightOutput = asset.LightOutput,
                            CCT = asset.CCT,
                            Size = asset.Size,
                            Location = asset.Location,
                            MountingBase = asset.MountingBase,
                            Category = asset.CategoryKey == null ? " " : asset.ItemCategory.TypeName,
                            // Catelogue = asset.CatelogueKey == null ? " " : asset.ItemCatelogue.TypeName,
                            Subcategory = asset.SubcategoryKey == null ? " " : asset.ItemSubcategory.TypeName,
                            Type = asset.ItemType.TypeName,
                            Manufacturer = asset.Manufacturer.ManufacturerName
                        }).OrderBy(m => m.ProductName).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Catelogue()
        {
            try
            {
                CatelogueViewModel model = new CatelogueViewModel();
                //model.ItemList = new List<ItemViewModel>();
                
                //model.ItemList = manage.GetItemList();
                ViewBag.mess =db.MainProduct.Count().ToString() + " Products found in " + GlobalClass.Company.CompanyName;
                ViewBag.ItemTypeKey = new SelectList(db.ItemType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.CategoryKey = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.SubcategoryKey = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
               // ViewBag.CatelogueKey = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.ManufacturerKey = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName");
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }

        }
       
        public ActionResult GetForCatelogue([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel)
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
                TypeCount = asset.TypeCount,
                ManufacturerKey = asset.ManufacturerKey,
                ProductName = asset.ProductName,
                ModelNo = asset.ModelNo,
                Watt = asset.Watt,
                ThermalEfficacy = asset.ThermalEfficacy,
                CRI = asset.CRI,
                Lumen = asset.Lumen,
                LampLife = asset.LampLife,
                LightApparent = asset.LightApparent,
                Source = asset.Source,
                Brand = asset.Brand,
                LightOutput = asset.LightOutput,
                CCT = asset.CCT,
                Size = asset.Size,
                Location = asset.Location,
                MountingBase = asset.MountingBase,
                Category = asset.ItemCategory.TypeName,
               // Catelogue = asset.ItemCatelogue.TypeName,
                Subcategory = asset.SubcategoryKey == null ? " " : asset.ItemSubcategory.TypeName,
                Type = asset.ItemType.TypeName,
                Manufacturer = asset.Manufacturer.ManufacturerName
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

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
                TypeCount = asset.TypeCount,
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
               // Catelogue = asset.ItemCatelogue.TypeName,
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