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

        public ActionResult EditAreaProductDetail(FormCollection form)
        {
            Guid AreaProductId = Guid.Parse(form["AreaProductId"]);
            AreaProductDetail _apd;
            var _DetailKey = form["DetailKey"];
            if (_DetailKey == "00000000-0000-0000-0000-000000000000")
            {
                _apd = new AreaProductDetail();
            }
            else
            {
                var _DetailKeyGuid = Guid.Parse(_DetailKey);
                _apd = db.AreaProductDetail.Where(apd => apd.DetailKey == _DetailKeyGuid ).Select(apd => apd).FirstOrDefault();
            }
            var _ExistingControl = form["ExistingControl"];
            if (_ExistingControl == "true")
            {
                _apd.ExistingControl = true;
            }
            else if (_ExistingControl == "false")
            {
                _apd.ExistingControl = false;
            }

            if (form["OperationScheduleKey"] != "")
            {
                _apd.OperationScheduleKey = Guid.Parse(form["OperationScheduleKey"]);
            }

            if (form["WorkingFixtureCount"] != "")
            {
                _apd.WorkingFixtureCount = Convert.ToInt32(form["WorkingFixtureCount"]);
            }
            
            
            if (form["ReplacementCost"] == "")
            {
                _apd.ReplacementCost = 0;
            }
            else
            {
                _apd.ReplacementCost = Convert.ToDecimal(form["ReplacementCost"]);
            }

            if (form["AnnualMaintenance"] == "")
            {
                _apd.AnnualMaintenance = 0;
            }
            else
            {
                _apd.AnnualMaintenance = Convert.ToDecimal(form["AnnualMaintenance"]);
            }

            var v = form["InstallationInTime"];
            if (form["InstallationInTime"] == "")
            {
                _apd.InstallationInTime = 0;
            }
            else
            {
                _apd.InstallationInTime = Convert.ToDecimal(form["InstallationInTime"]);
            }

            _apd.Location = form["Location"];

            if (form["Year"] != "")
            {
                _apd.Year = Convert.ToInt32(form["Year"]);
            }


            if (form["LightingSatisfactionKey"] != "")
            {
                _apd.LightingSatisfactionKey = Guid.Parse(form["LightingSatisfactionKey"]);
            }
            
            if (form["MountingHeight"] == "")
            {
                _apd.MountingHeight= 0;
            }
            else
            {
                _apd.MountingHeight = Convert.ToDecimal(form["MountingHeight"]);
            }

            if (_DetailKey == "00000000-0000-0000-0000-000000000000")
            {
                _apd.DetailKey = Guid.NewGuid();
                db.AreaProductDetail.Add(_apd);
            }
            
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = AreaProductId });
        }
        public ActionResult EditAreaProductNote(FormCollection form)
        {
            Guid AreaProductId = Guid.Parse(form["AreaProductId"]);
            if (
                form["Description"] == "" &&
                form["Condition"] == "" &&
                form["InternalNotes"] == "" &&
                form["GeneralNote"] == "" 
                )
            {
                return RedirectToAction("Edit", new { id = AreaProductId });
            }

            AreaProductNote _apn;
            var _NoteKey = form["NoteKey"];
            if (_NoteKey == "00000000-0000-0000-0000-000000000000")
            {
                _apn = new AreaProductNote();
            }
            else
            {
                var _NoteKeyGuid = Guid.Parse(_NoteKey);
                _apn = db.AreaProductNote.Where(apn => apn.NoteKey == _NoteKeyGuid).Select(apd => apd).FirstOrDefault();
            }

            if (form["Description"] == "")
            {
                _apn.Description = "not required";
            }
            else
            {
                _apn.Description = form["Description"];
            }

            if (form["Condition"] == "")
            {
                _apn.Condition = "not required";
            }
            else
            {
                _apn.Condition = form["Condition"];
            }

            if (form["InternalNotes"] == "")
            {
                _apn.InternalNotes = "not required";
            }
            else
            {
                _apn.InternalNotes = form["InternalNotes"];
            }

            if (form["GeneralNote"] == "")
            {
                _apn.GeneralNote = "not required";
            }
            else
            {
                _apn.GeneralNote = form["GeneralNote"];
            }

            if (_NoteKey == "00000000-0000-0000-0000-000000000000")
            {
                _apn.NoteKey = Guid.NewGuid();
                db.AreaProductNote.Add(_apn);
            }
           
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = AreaProductId });
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

        public ActionResult GetItemList()
        {
            JsonResult result = new JsonResult();
            string obj = manage.CreateProductListHTML();
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