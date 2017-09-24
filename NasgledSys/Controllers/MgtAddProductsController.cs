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
using System.Data.Entity;
namespace NasgledSys.Controllers
{
    public class MgtAddProductsController : Controller
    {
        // GET: MgtAddProducts
        private NasgledDBEntities db = new NasgledDBEntities();
        private ManageAreaProduct manage = new ManageAreaProduct();
        private ManageProjectArea manageArea = new ManageProjectArea();
        public ActionResult Index(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Area model = db.Area.Find(id);
                    ViewBag.heading = manageArea.GetAllAreaNamesForSubArea(id);
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

        public JsonResult GetProductList(Guid id)
        {

            var obj = (from asset in db.AreaProduct
                       where asset.AreaKey == id && asset.IsDelete == false
                       select new
                       {
                           asset.ExistingProductName,
                           asset.Count,
                           asset.Description,
                           asset.ProductKey
                       }).ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Session["GlobalMessege"] = "";
                    Area model = db.Area.Find(id);
                    AddProductClass obj = new AddProductClass();
                    obj.AreaKey = model.AreaKey;
                    obj.FixtureKey = Guid.Empty;
                    obj.AreaObj = model;
                    return View(obj);
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
        [HttpPost]
        public ActionResult Create(AddProductClass model, string Save, string Add, string AddDetails)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (model.Count == null) model.Count = 1;
                        if (string.IsNullOrEmpty(model.Description))
                        {
                            model.Description = "--";
                        }

                        AreaProduct obj = new AreaProduct();
                        obj.ProductKey = Guid.NewGuid();
                        obj.FixtureKey = model.FixtureKey;
                        obj.ProjectKey = GlobalClass.Project.ProjectKey;
                        obj.AreaKey = model.AreaKey;
                        obj.Count = model.Count;
                        obj.Description = model.Description;
                        obj.ExistingProductName = manage.GetAllFixtureNamesForExistingKey(model.FixtureKey);

                        obj.IsDelete = false;
                        db.AreaProduct.Add(obj);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Product was Saved successfully Created.";
                        if (!string.IsNullOrEmpty(Save))
                            return RedirectToAction("Index", "MgtAddProducts", new { id = obj.AreaKey });
                        else if (!string.IsNullOrEmpty(Add))
                            return RedirectToAction("Create", "MgtAddProducts", new { id = obj.AreaKey });
                        else if (!string.IsNullOrEmpty(AddDetails))
                            return RedirectToAction("Edit", "MgtAddProducts", new { id = obj.ProductKey });
                        else
                            return RedirectToAction("Index", "MgtAddProducts", new { id = obj.AreaKey });
                    }
                    return View(model);
                }
                catch (Exception e)
                {
                    Session["GlobalMessege"] = e.Message.ToString();
                    return View(model);
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public ActionResult Edit(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Session["GlobalMessege"] = "";
                    AreaProduct model = db.AreaProduct.Find(id);
                    Area area = db.Area.Find(model.AreaKey);
                    AddProductClass obj = new AddProductClass();
                    obj.ProductKey = model.ProductKey;

                    obj.AreaKey = model.AreaKey;
                    obj.FixtureKey = model.FixtureKey;
                    obj.Count = model.Count;
                    obj.Description = model.Description;
                    obj.AreaObj = area;

                    ViewBag.AreaProductId = id;
                    // Get Set AreaProductDetail >> Start
                    AreaProductDetail areaProductDetail = db.AreaProductDetail.FirstOrDefault();//# todo
                    if (areaProductDetail==null)
                    {
                        areaProductDetail = new AreaProductDetail();
                        //areaProductDetail.WorkingFixtureCount = db.AreaProduct.Find(GlobalClass.Project.ProjectKey).Count;
                        areaProductDetail.Year = DateTime.Today.Year;
                    }
                    ViewBag.areaProductDetail = areaProductDetail;

                    ViewBag.ExistingControl_SelectItems = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {Text = "YES", Value = "true" },
                        new SelectListItem {Text = "NO", Value = "false" },
                    }, "Value", "Text", areaProductDetail.ExistingControl.ToString());
                    
                    ViewBag.OperationSchedule_SelectItems = new SelectList(db.OperatingSchedule.Where(os=>os.ProjectKey == GlobalClass.Project.ProjectKey), "PKey", "OPName", areaProductDetail.OperationScheduleKey);
                    ViewBag.LightingSatisfactionFactor_SelectItems = new SelectList(db.LightingSatisfactionFactor, "PKey", "FactorName", areaProductDetail.LightingSatisfactionKey);

                    // Get Set AreaProductNote >> Start
                    AreaProductNote areaProductNote = db.AreaProductNote.FirstOrDefault();//# todo
                    if (areaProductNote == null)
                    {
                        areaProductNote = new AreaProductNote();
                    }
                    ViewBag.areaProductNote = areaProductNote;

                    return View(obj);
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
        [HttpPost]
        public ActionResult Edit(AddProductClass model)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (model.Count == null) model.Count = 1;
                        if (string.IsNullOrEmpty(model.Description))
                        {
                            model.Description = "--";
                        }

                        AreaProduct obj = db.AreaProduct.Find(model.ProductKey);
                        obj.FixtureKey = model.FixtureKey;
                        obj.Count = model.Count;
                        obj.Description = model.Description;
                        obj.ExistingProductName = manage.GetAllFixtureNamesForExistingKey(model.FixtureKey);

                        db.SaveChanges();
                        Session["GlobalMessege"] = "Product was Saved successfully Created.";

                        return RedirectToAction("Index", "MgtAddProducts", new { id = obj.AreaKey });
                    }
                    return View(model);
                }
                catch (Exception e)
                {
                    Session["GlobalMessege"] = e.Message.ToString();
                    return View(model);
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

      
        public ActionResult Delete(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    AreaProduct model = db.AreaProduct.Find(id);
                    AddProductClass obj = new AddProductClass();
                    obj.ProductKey = model.ProductKey;
                    obj.AreaKey = model.AreaKey;
                    model.IsDelete = true;
                    db.SaveChanges();
                    Session["GlobalMessege"] = "";
                    return RedirectToAction("Index", new { id = obj.AreaKey });
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

        public JsonResult GetItemList()
        {
            JsonResult result = new JsonResult();
            string obj = manage.CreateProductListHTML1();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSuggestList()
        {
            JsonResult result = new JsonResult();
            string obj = manage.GetSuggestiveItem(GlobalClass.Project.ProjectKey);
            result.Data = obj;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
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