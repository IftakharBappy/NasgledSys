using NasgledSys.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace NasgledSys.Controllers
{
    public class MgtProfileProductController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private NasgledDBEntities db = new NasgledDBEntities();
        //# to Index
        public ActionResult Index()
        {
            if (GlobalClass.MasterSession)
            {
                //logger.Info("MgtClientCompany IndexClientCompany() invoked by :  " + GlobalClass.ProfileUser.FirstName+" "+ GlobalClass.ProfileUser.LastName);
                return View();
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
            }
        }
        public ActionResult QPL()
        {
            if (GlobalClass.MasterSession)
            {
                return View();
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
            }
        }
        public ActionResult LightingFacts()
        {
            if (GlobalClass.MasterSession)
            {
                return View();
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "Userhome"));
            }
        }
        public JsonResult GetProfileProductList()
        {
            var list = (from asset in db.ProfileProduct

                        select new
                        {
                            Brand = asset.Brand == null ? " " : asset.Brand,
                            Source = asset.Source == null ? " " : asset.Source,
                            FixtureKey = asset.FixtureKey,
                            ItemTypeKey = asset.ItemTypeKey,
                            CategoryKey = asset.CategoryKey,
                            CatelogueKey = asset.CatelogueKey,
                            SubcategoryKey = asset.SubcategoryKey,
                            TypeCount = asset.TypeCount,
                            ManufacturerKey = asset.ManufacturerKey,
                            ProductName = asset.ProductName,
                            ModelNo = asset.ModelNo,
                            Watt = asset.Watt,
                            LightOutput = asset.LightOutput,
                            ThermalEfficacy = asset.ThermalEfficacy,
                            Lumen = asset.Lumen,                         
                            LampLife = asset.LampLife,
                            CRI=asset.CRI,
                            CCT = asset.CCT,
                            Size = asset.Size,
                            Location = asset.Location,
                            MountingBase = asset.MountingBase,
                            LightApparent = asset.LightApparent,                           
                            Category = asset.CategoryKey == null ? " " : asset.ItemCategory.TypeName,
                            Catelogue = asset.CatelogueKey == null ? " " : asset.ItemCatelogue.TypeName,
                            Subcategory = asset.SubcategoryKey == null ? " " : asset.ItemSubcategory.TypeName,
                            Type = asset.ItemType.TypeName,
                            Manufacturer = asset.Manufacturer.ManufacturerName,
                        MainProductDetail = asset.MainItemKey == null ? " " : db.ProfileProduct.FirstOrDefault(m=>m.MainItemKey==asset.MainItemKey).ProductName
        }).OrderBy(m => m.ProductName).ToList();
       
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //# to create
        public ActionResult Create()
        {
            if (GlobalClass.MasterSession)
            {
                ViewBag.MainItemKey = new SelectList(db.ProfileProduct.OrderBy(m => m.FixtureKey), "FixtureKey", "ProductName");
                ViewBag.ItemTypeSelectList = new SelectList(db.ItemType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.CategorySelectList = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.SubcategorySelectList = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.CatelogueSelectList = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName");
                ViewBag.ManufacturerSelectList = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName");

                return View();
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Logo")]ProfileProductViewModel viewModel, HttpPostedFileBase Logo)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ProfileProduct model = new ProfileProduct();
                        model.FixtureKey = Guid.NewGuid();
                        model.ItemTypeKey = viewModel.ItemTypeKey;
                        model.CategoryKey = viewModel.CategoryKey;
                        model.SubcategoryKey = viewModel.SubcategoryKey;
                        model.CatelogueKey = viewModel.CatelogueKey;
                        model.Source = viewModel.Source;
                        model.Brand = viewModel.Brand;
                        model.ManufacturerKey = viewModel.ManufacturerKey;
                        model.ProductName = viewModel.ProductName;
                        model.ModelNo = viewModel.ModelNo;
                        model.Watt = viewModel.Watt;
                        model.Watt = viewModel.Watt;

                        if (viewModel.ThermalEfficacy == null) { model.ThermalEfficacy = 0; }
                        else { model.ThermalEfficacy = viewModel.ThermalEfficacy; }

                        if (viewModel.CRI == null) { model.CRI = 0; }
                        else { model.CRI = viewModel.CRI; }

                        if (viewModel.Lumen == null) { model.Lumen = 0; }
                        else { model.Lumen = viewModel.Lumen; }

                        if (viewModel.LightApparent == null) { model.LightApparent = 0; }
                        else { model.LightApparent = viewModel.LightApparent; }

                        if (viewModel.LightOutput == null) { model.LightOutput = 0; }
                        else { model.LightOutput = viewModel.LightOutput; }

                        if (viewModel.CCT == null) { model.CCT = 0; }
                        else { model.CCT = viewModel.CCT; }

                        if (viewModel.Size != "") { model.Size = "n/a"; }
                        else { model.Size = viewModel.Size; }

                        if (viewModel.Location != "") { model.Location = "n/a"; }
                        else { model.Location = viewModel.Location; }

                        model.MountingBase = viewModel.MountingBase;

                        if (viewModel.LampLife == null) { model.LampLife = 0; }
                        else { model.LampLife = viewModel.LampLife; }


                        if (viewModel.TypeCount == null) { model.TypeCount = 1; }
                        else { model.TypeCount = viewModel.TypeCount; }

                        model.ProfileKey = GlobalClass.ProfileUser.ProfileKey;



                        byte[] imgBinaryData = new byte[Logo.ContentLength];
                        int readresult = Logo.InputStream.Read(imgBinaryData, 0, Logo.ContentLength);
                        model.Logo = imgBinaryData;
                        model.FileName = Logo.FileName;
                        model.FileType = Logo.ContentType;
                        model.MainItemKey = viewModel.MainItemKey;
                        db.ProfileProduct.Add(model);
                        db.SaveChanges();
                        TempData["mess"] = "Profile Product is successfully created.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.MainItemKey = new SelectList(db.ProfileProduct.OrderBy(m => m.FixtureKey), "FixtureKey", "ProductName",viewModel.MainItemKey);
                        ViewBag.ItemTypeSelectList = new SelectList(db.ItemType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", viewModel.ItemTypeKey);
                        ViewBag.CategorySelectList = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", viewModel.CategoryKey);
                        ViewBag.SubcategorySelectList = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", viewModel.SubcategoryKey);
                        ViewBag.CatelogueSelectList = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", viewModel.CatelogueKey);
                        ViewBag.ManufacturerSelectList = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName", viewModel.ManufacturerKey);

                        return View(viewModel);
                    }

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

        //# to Edit
        public ActionResult Edit(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                ProfileProduct profileProduct = (from pp in db.ProfileProduct where pp.FixtureKey == id select pp).FirstOrDefault();

                ProfileProductViewModel profileProductviewModel = new ProfileProductViewModel();
                profileProductviewModel.FixtureKey = profileProduct.FixtureKey;
                profileProductviewModel.ItemTypeKey = profileProduct.ItemTypeKey;
                profileProductviewModel.CategoryKey = profileProduct.CategoryKey;
                profileProductviewModel.SubcategoryKey = profileProduct.SubcategoryKey;
                profileProductviewModel.CatelogueKey = profileProduct.CatelogueKey;
                profileProductviewModel.Source = profileProduct.Source;
                profileProductviewModel.Brand = profileProduct.Brand;
                profileProductviewModel.ManufacturerKey = profileProduct.ManufacturerKey;
                profileProductviewModel.ProductName = profileProduct.ProductName;
                profileProductviewModel.ModelNo = profileProduct.ModelNo;
                profileProductviewModel.Watt = profileProduct.Watt;
                profileProductviewModel.Watt = profileProduct.Watt;
                profileProductviewModel.ThermalEfficacy = profileProduct.ThermalEfficacy; 
                profileProductviewModel.CRI = profileProduct.CRI; 
                profileProductviewModel.Lumen = profileProduct.Lumen; 
                profileProductviewModel.LightApparent = profileProduct.LightApparent; 
                profileProductviewModel.LightOutput = profileProduct.LightOutput; 
                profileProductviewModel.CCT = profileProduct.CCT; 
                profileProductviewModel.Size = profileProduct.Size; 
                profileProductviewModel.Location = profileProduct.Location; 
                profileProductviewModel.MountingBase = profileProduct.MountingBase;
                profileProductviewModel.LampLife = profileProduct.LampLife; 
                profileProductviewModel.TypeCount = profileProduct.TypeCount;
                profileProductviewModel.Logo = profileProduct.Logo;
                profileProductviewModel.MainItemKey = profileProduct.MainItemKey;

                ViewBag.MainItemKey = new SelectList(db.ProfileProduct.OrderBy(m => m.FixtureKey), "FixtureKey", "ProductName", profileProduct.MainItemKey);
                ViewBag.ItemTypeSelectList = new SelectList(db.ItemType.Where(m => m.IsDelete == false ).OrderBy(m => m.TypeName), "PKey", "TypeName", profileProduct.ItemTypeKey);
                ViewBag.CategorySelectList = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false ).OrderBy(m => m.TypeName), "PKey", "TypeName", profileProduct.CategoryKey);
                ViewBag.SubcategorySelectList = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false ).OrderBy(m => m.TypeName), "PKey", "TypeName", profileProduct.SubcategoryKey);
                ViewBag.CatelogueSelectList = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false ).OrderBy(m => m.TypeName), "PKey", "TypeName", profileProduct.CatelogueKey);
                ViewBag.ManufacturerSelectList = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false ).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName", profileProduct.ManufacturerKey);

                return View(profileProductviewModel);
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Logo")]ProfileProductViewModel viewModel, HttpPostedFileBase Logo)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ProfileProduct model = (from pp in db.ProfileProduct where pp.FixtureKey == viewModel.FixtureKey select pp).FirstOrDefault();
                        model.ItemTypeKey = viewModel.ItemTypeKey;
                        model.CategoryKey = viewModel.CategoryKey;
                        model.SubcategoryKey = viewModel.SubcategoryKey;
                        model.CatelogueKey = viewModel.CatelogueKey;
                        model.Source = viewModel.Source;
                        model.Brand = viewModel.Brand;
                        model.ManufacturerKey = viewModel.ManufacturerKey;
                        model.ProductName = viewModel.ProductName;
                        model.ModelNo = viewModel.ModelNo;
                        model.Watt = viewModel.Watt;
                        model.Watt = viewModel.Watt;
                        model.MainItemKey = viewModel.MainItemKey;

                        if (viewModel.ThermalEfficacy == null) { model.ThermalEfficacy = 0; }
                        else { model.ThermalEfficacy = viewModel.ThermalEfficacy; }

                        if (viewModel.CRI == null) { model.CRI = 0; }
                        else { model.CRI = viewModel.CRI; }

                        if (viewModel.Lumen == null) { model.Lumen = 0; }
                        else { model.Lumen = viewModel.Lumen; }

                        if (viewModel.LightApparent == null) { model.LightApparent = 0; }
                        else { model.LightApparent = viewModel.LightApparent; }

                        if (viewModel.LightOutput == null) { model.LightOutput = 0; }
                        else { model.LightOutput = viewModel.LightOutput; }

                        if (viewModel.CCT == null) { model.CCT = 0; }
                        else { model.CCT = viewModel.CCT; }

                        if (viewModel.Size != "") { model.Size = "n/a"; }
                        else { model.Size = viewModel.Size; }

                        if (viewModel.Location != "") { model.Location = "n/a"; }
                        else { model.Location = viewModel.Location; }

                        model.MountingBase = viewModel.MountingBase;

                        if (viewModel.LampLife == null) { model.LampLife = 0; }
                        else { model.LampLife = viewModel.LampLife; }


                        if (viewModel.TypeCount == null) { model.TypeCount = 1; }
                        else { model.TypeCount = viewModel.TypeCount; }

                        model.ProfileKey = GlobalClass.ProfileUser.ProfileKey;


                        if (viewModel.KeepOldfile == false)
                        {
                            byte[] imgBinaryData = new byte[Logo.ContentLength];
                            int readresult = Logo.InputStream.Read(imgBinaryData, 0, Logo.ContentLength);
                            model.Logo = imgBinaryData;
                            model.FileName = Logo.FileName;
                            model.FileType = Logo.ContentType;
                        }
                        

                        db.ProfileProduct.Attach(model);
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["mess"] = "Profile Product is successfully created.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ProfileProduct model = (from pp in db.ProfileProduct where pp.FixtureKey == viewModel.FixtureKey select pp).FirstOrDefault();
                        viewModel.Logo = model.Logo;
                        ViewBag.MainItemKey = new SelectList(db.ProfileProduct.OrderBy(m => m.FixtureKey), "FixtureKey", "ProductName", viewModel.MainItemKey);
                        ViewBag.ItemTypeSelectList = new SelectList(db.ItemType.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", viewModel.ItemTypeKey);
                        ViewBag.CategorySelectList = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", viewModel.CategoryKey);
                        ViewBag.SubcategorySelectList = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", viewModel.SubcategoryKey);
                        ViewBag.CatelogueSelectList = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false).OrderBy(m => m.TypeName), "PKey", "TypeName", viewModel.CatelogueKey);
                        ViewBag.ManufacturerSelectList = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName", viewModel.ManufacturerKey);

                        return View(viewModel);
                    }

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

        //# to View
        public ActionResult View(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                ProfileProduct profileProduct = (from pp in db.ProfileProduct where pp.FixtureKey == id select pp).FirstOrDefault();
                ProfileProductViewModel viewModel = new ProfileProductViewModel();
                viewModel.ItemTypeKey = profileProduct.ItemTypeKey;
                viewModel.CategoryKey = profileProduct.CategoryKey;
                viewModel.SubcategoryKey = profileProduct.SubcategoryKey;
                viewModel.CatelogueKey = profileProduct.CatelogueKey;
                viewModel.Source = profileProduct.Source;
                viewModel.Brand = profileProduct.Brand;
                viewModel.ManufacturerKey = profileProduct.ManufacturerKey;
                viewModel.ProductName = profileProduct.ProductName;
                viewModel.ModelNo = profileProduct.ModelNo;
                viewModel.Watt = profileProduct.Watt;
                viewModel.Watt = profileProduct.Watt;
                viewModel.ThermalEfficacy = profileProduct.ThermalEfficacy;
                viewModel.CRI = profileProduct.CRI;
                viewModel.Lumen = profileProduct.Lumen;
                viewModel.LightApparent = profileProduct.LightApparent;
                viewModel.LightOutput = profileProduct.LightOutput;
                viewModel.CCT = profileProduct.CCT;
                viewModel.Size = profileProduct.Size;
                viewModel.Location = profileProduct.Location;
                viewModel.MountingBase = profileProduct.MountingBase;
                viewModel.LampLife = profileProduct.LampLife;
                viewModel.TypeCount = profileProduct.TypeCount;
                viewModel.Logo = profileProduct.Logo;
                viewModel.MainItemKey = profileProduct.MainItemKey;

                ViewBag.MainItemKey = new SelectList(db.ProfileProduct.OrderBy(m => m.FixtureKey), "FixtureKey", "ProductName", profileProduct.MainItemKey);
                ViewBag.ItemTypeSelectList = new SelectList(db.ItemType.Where(m => m.IsDelete == false && m.PKey == profileProduct.ItemTypeKey).OrderBy(m => m.TypeName), "PKey", "TypeName", profileProduct.ItemTypeKey);
                ViewBag.CategorySelectList = new SelectList(db.ItemCategory.Where(m => m.IsDelete == false && m.PKey == profileProduct.CategoryKey).OrderBy(m => m.TypeName), "PKey", "TypeName", profileProduct.CategoryKey);
                ViewBag.SubcategorySelectList = new SelectList(db.ItemSubcategory.Where(m => m.IsDelete == false && m.PKey == profileProduct.SubcategoryKey).OrderBy(m => m.TypeName), "PKey", "TypeName", profileProduct.SubcategoryKey);
                ViewBag.CatelogueSelectList = new SelectList(db.ItemCatelogue.Where(m => m.IsDelete == false && m.PKey == profileProduct.CatelogueKey).OrderBy(m => m.TypeName), "PKey", "TypeName", profileProduct.CatelogueKey);
                ViewBag.ManufacturerSelectList = new SelectList(db.Manufacturer.Where(m => m.IsDelete == false && m.PKey == profileProduct.ManufacturerKey).OrderBy(m => m.ManufacturerName), "PKey", "ManufacturerName", profileProduct.ManufacturerKey);

                return View(viewModel);
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        //# Delete
        public ActionResult Delete(Guid id)
        {

            if (GlobalClass.MasterSession)
            {
                try
                {
                    ProfileProduct profileProduct = (from pp in db.ProfileProduct where pp.FixtureKey == id select pp).FirstOrDefault();
                    db.ProfileProduct.Remove(profileProduct);
                    db.SaveChanges();
                    TempData["mess"] = "Profile Product is successfully deleted.";
                    return RedirectToAction("Index");
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