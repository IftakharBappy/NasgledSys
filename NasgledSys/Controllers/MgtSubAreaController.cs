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

namespace NasgledSys.Controllers
{
    public class MgtSubAreaController : Controller
    {
        // GET: MgtSubArea
        private NasgledDBEntities db = new NasgledDBEntities();

        private ManageProjectArea manage = new ManageProjectArea();
        public ActionResult Index(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Area model = db.Area.Find(id);
                    
                   return View(model);
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

        public ActionResult Delete(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Area p = db.Area.Find(id);
                    p.IsDelete = true;
                    db.SaveChanges();
                    Session["GlobalMessege"] = "Area has been DELETED successfully.";
                    return RedirectToAction("Created", "MgtArea",new { id=p.ParentAreaKey});
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

        public ActionResult Edit(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Area model = db.Area.Find(id);
                    if(model.ParentAreaKey==null || model.ParentAreaKey == Guid.Empty)
                    {
                        return RedirectToAction("Edit","MgtArea",new { id=model.AreaKey});
                    }
                    else
                    {
                        AreaClass obj = new AreaClass();
                       
                        obj = EM_Area.ConvertToModelForEditArea(model);
                        obj.IsParent = false;
                        obj.IsSubEdit = true;
                        obj.ParentAreaKey = model.ParentAreaKey;
                        obj.ParentAreaName= manage.GetAllAreaNamesForSubArea(model.AreaKey);

                        GlobalClass.AreaHeading= obj.ParentAreaName;
                        ViewBag.heading = obj.ParentAreaName;
                        return View(obj);
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
        [HttpPost]
        public ActionResult Edit(AreaClass model)
        {
            if (GlobalClass.MasterSession)
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                       
                            if (model.SquareFeet == null) model.SquareFeet = 0;
                            if (model.OperationScheduleKey == null || model.OperationScheduleKey == Guid.Empty)
                            {
                                model.OperationScheduleKey = manage.GetOperatingScheduleKey();
                            }
                            if (model.NewRateScheduleKey == null || model.NewRateScheduleKey == Guid.Empty)
                            {
                                model.NewRateScheduleKey = manage.GetNewRateSchedule();
                            }
                            if (model.CoolingSystemKey == null || model.CoolingSystemKey == Guid.Empty)
                            {
                                model.CoolingSystemKey = manage.GetCoolingSystem();
                            }
                            if (model.HeatingSystemKey == null || model.HeatingSystemKey == Guid.Empty)
                            {
                                model.HeatingSystemKey = manage.GetHeatingSystem();
                            }
                            Area obj = db.Area.Find(model.AreaKey);
                            obj.AreaName = model.AreaName;
                            obj.Reception = model.Reception;
                            obj.SquareFeet = model.SquareFeet;
                            obj.OperationScheduleKey = model.OperationScheduleKey;
                            obj.NewRateScheduleKey = model.NewRateScheduleKey;
                            obj.CoolingSystemKey = model.CoolingSystemKey;
                            obj.HeatingSystemKey = model.HeatingSystemKey;
                        obj.ParentAreaKey = model.ParentAreaKey;
                        db.SaveChanges();
                            Session["GlobalMessege"] = "Subarea was saved successfully.";
                        GlobalClass.AreaGuidForSubArea = (Guid)model.ParentAreaKey;
                        GlobalClass.AreaHeading = model.ParentAreaName;
                        return RedirectToAction("Created", "MgtArea", new { id = model.AreaKey });
                     
                    }
                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Created", "MgtProject"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult Create(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Session["GlobalMessege"] = "";
                    GlobalClass.AreaGuidForSubArea = Guid.Empty;
                    GlobalClass.AreaHeading = null;
                    GlobalClass.IsSubArea = false;
                    GlobalClass.SubAreaLevel = 0;
                    Area model = db.Area.Find(id);
                    AreaClass obj = new AreaClass();
                    obj.AreaKey = Guid.NewGuid();
                    obj.ParentAreaKey = id;
                    obj.ParentAreaName = manage.GetAllAreaNamesForSubArea(model.AreaKey);
                    obj.SquareFeet =0;
                    obj.AreaName = "";
                    obj.OperationScheduleKey = model.OperationScheduleKey;
                    obj.NewRateScheduleKey = model.NewRateScheduleKey;
                    obj.CoolingSystemKey = model.CoolingSystemKey;
                    obj.HeatingSystemKey = model.HeatingSystemKey;
                    obj.ParentAreaKey = model.AreaKey;
                    obj.ProjectKey = model.ProjectKey;
                    obj.IsParent = false;
                    obj.IsSubEdit = false;
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
        public ActionResult Create(AreaClass model)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (model.SquareFeet == null) model.SquareFeet = 0;
                        if (model.OperationScheduleKey == null || model.OperationScheduleKey == Guid.Empty)
                        {
                            model.OperationScheduleKey = manage.GetOperatingScheduleKey();
                        }
                        if (model.NewRateScheduleKey == null || model.NewRateScheduleKey == Guid.Empty)
                        {
                            model.NewRateScheduleKey = manage.GetNewRateSchedule();
                        }
                        if (model.CoolingSystemKey == null || model.CoolingSystemKey == Guid.Empty)
                        {
                            model.CoolingSystemKey = manage.GetCoolingSystem();
                        }
                        if (model.HeatingSystemKey == null || model.HeatingSystemKey == Guid.Empty)
                        {
                            model.HeatingSystemKey = manage.GetHeatingSystem();
                        }
                        db = new NasgledDBEntities();
                        Area obj = new Area();
                        obj.AreaKey = Guid.NewGuid();
                        obj.AreaName = model.AreaName;
                        obj.Reception = "--";
                        obj.SquareFeet = model.SquareFeet;
                        obj.OperationScheduleKey = model.OperationScheduleKey;
                        obj.NewRateScheduleKey = model.NewRateScheduleKey;
                        obj.CoolingSystemKey = model.CoolingSystemKey;
                        obj.HeatingSystemKey = model.HeatingSystemKey;
                        obj.ProjectKey = GlobalClass.Project.ProjectKey;
                        obj.ParentAreaKey = model.ParentAreaKey;
                        obj.IsDelete = false;
                        db.Area.Add(obj);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Sub-Area was successfully Created.";
                        GlobalClass.AreaGuidForSubArea =(Guid)model.ParentAreaKey;
                        GlobalClass.AreaHeading = model.ParentAreaName;
                        return RedirectToAction("Created", "MgtArea", new { id = obj.AreaKey });
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

        public JsonResult GetSubAreaList(Guid id)
        {
            List<ViewSubAreaList> list = new List<ViewSubAreaList>();
            var obj = from asset in db.Area where asset.ParentAreaKey == id select asset;
            if (obj.Count() > 0)
            {
                foreach (var item in obj)
                {
                    ViewSubAreaList m = new ViewSubAreaList();
                    m.AreaKey = item.AreaKey;
                    m.SubAreaName = manage.GetAllAreaNamesForSubArea(item.AreaKey);
                    m.AreaName = " in " + db.Area.FirstOrDefault(d => d.AreaKey == item.ParentAreaKey).AreaName;
                    m.ProductCount = db.AreaProduct.Where(d => d.AreaKey == item.AreaKey).Count();
                    list.Add(m);
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProfileProductList()
        {
            var list = (from asset in db.ProfileProduct
                        where asset.ProfileKey == GlobalClass.ProfileUser.ProfileKey
                        && asset.MainItemKey == null
                        select new
                        {
                           
                            FixtureKey = asset.FixtureKey,                           
                            TypeCount = asset.TypeCount,                          
                            ProductName = asset.ProductName,
                            ModelNo = asset.ModelNo
                        }).OrderBy(m => m.ProductName).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
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