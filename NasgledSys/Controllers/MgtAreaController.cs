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
    public class MgtAreaController : Controller
    {
        // GET: MgtArea
        private NasgledDBEntities db = new NasgledDBEntities();
      
        private ManageProjectArea manage = new ManageProjectArea();
        public ActionResult GetArea([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel)
        {
            IQueryable<Area> query = db.Area.Where(m => m.ProjectKey==GlobalClass.Project.ProjectKey && m.IsDelete==false);
            var totalCount = query.Count();

            // searching and sorting
            query = Search(requestModel, searchViewModel, query);
            var filteredCount = query.Count();

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(asset => new
            {
                AreaKey = asset.AreaKey,
                AreaName = asset.AreaName,
                SubArea =db.Area.Where(m => m.ParentAreaKey == asset.AreaKey).Count()
            }).ToList();








            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }

        private IQueryable<Area> Search(IDataTablesRequest requestModel, AdvancedSearchViewModel searchViewModel, IQueryable<Area> query)
        {
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.AreaName.ToUpper().Contains(value.ToUpper()));
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

            query = query.OrderBy(orderByString == string.Empty ? "AreaName asc" : orderByString);

            return query;

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Session["GlobalMessege"] = "";
                    GlobalClass.AreaGuidForSubArea =Guid.Empty;
                    GlobalClass.AreaHeading = null;
                    AreaClass obj = new AreaClass();
                    obj.OperationScheduleKey = Guid.Empty;
                    obj.NewRateScheduleKey = Guid.Empty;
                    obj.CoolingSystemKey = Guid.Empty;
                    obj.HeatingSystemKey = Guid.Empty;
                  
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
                        obj.IsDelete = false;
                        db.Area.Add(obj);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Area was successfully Created.";
                        GlobalClass.AreaGuidForSubArea = Guid.Empty;
                        GlobalClass.AreaHeading = model.AreaName;
                        return RedirectToAction("Created","MgtArea",new { id=obj.AreaKey});
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
                    Area p = db.Area.Find(id);
                    p.IsDelete = true;
                    db.SaveChanges();
                    Session["GlobalMessege"] = "Area has been DELETED successfully.";
                    return RedirectToAction("Created", "MgtProject");
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
                AreaClass obj = new AreaClass();
                try
                {
                    if(GlobalClass.AreaGuidForSubArea==null|| GlobalClass.AreaGuidForSubArea == Guid.Empty)
                    {
                        Area ar = db.Area.Find(id);
                        ViewBag.heading = ar.AreaName;                    
                        obj = EM_Area.ConvertToModelForEditArea(ar);
                    }
                    else
                    {

                    }
                    Session["GlobalMessege"] = "";                    
                    return View(obj);
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
        [HttpPost]
        public ActionResult Edit(AreaClass model)
        {
            if (GlobalClass.MasterSession)
            {
                
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (model.IsParent == true)
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
                            obj.Reception =model.Reception;
                            obj.SquareFeet = model.SquareFeet;
                            obj.OperationScheduleKey = model.OperationScheduleKey;
                            obj.NewRateScheduleKey = model.NewRateScheduleKey;
                            obj.CoolingSystemKey = model.CoolingSystemKey;
                            obj.HeatingSystemKey = model.HeatingSystemKey;                            
                            db.SaveChanges();
                            Session["GlobalMessege"] = "Area was saved successfully.";
                            return RedirectToAction("Created","MgtArea",new { id=model.AreaKey});
                        }
                        else
                        {

                        }
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
        public ActionResult Created(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Area obj = new Area();
                    obj.AreaKey = id;
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
        public async Task<ActionResult> SaveNewOperatingSchedule(string ScheduleName, decimal? OptHour)
        {
            try
            {
                OperatingSchedule contact = new OperatingSchedule();
                contact.PKey = Guid.NewGuid();
                contact.OPName = ScheduleName;               
                contact.AnnualOperationHour = OptHour;
                contact.ProjectKey =GlobalClass.Project.ProjectKey;
                contact.InternalNote = "n/a";
               
                contact.GeneralNote = "n/a";
                var temp = (from x in db.OperatingSchedule where x.ProjectKey == GlobalClass.Project.ProjectKey select x).ToList();
                if (temp.Count() > 0)
                {
                    contact.IsDefault = false;
                }
                else
                {
                    contact.IsDefault = true;
                }
                db.OperatingSchedule.Add(contact);
                var task = db.SaveChangesAsync();
                await task;
                return Content("success");
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }
        [HttpPost]
        public async Task<ActionResult> SaveNewRateSchedule(string RateName, decimal? Rate)
        {
            try
            {
                NewRateSchedule contact = new NewRateSchedule();
                contact.PKey = Guid.NewGuid();
                contact.NRName = RateName;
                contact.BlendElectricityRate = Rate;
                contact.ProjectKey = GlobalClass.Project.ProjectKey;
                contact.InternalNote = "n/a";

                contact.GeneralNote = "n/a";
                var temp = (from x in db.NewRateSchedule where x.ProjectKey == GlobalClass.Project.ProjectKey select x).ToList();
                if (temp.Count() > 0)
                {
                    contact.IsDefault = false;
                }
                else
                {
                    contact.IsDefault = true;
                }
                db.NewRateSchedule.Add(contact);
                var task = db.SaveChangesAsync();
                await task;
                return Content("success");
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveNewCoolingSystem(string SystemName, decimal? RunTime,Guid? SystemTypeKey,Guid? FuelTypeKey,decimal? FCost,Guid? EfficiencyTypeKey,decimal? EfficiencyValue)
        {
            try
            {
                CoolingSystem contact = new CoolingSystem();
                contact.CoolingSystemKey = Guid.NewGuid();
                contact.SystemName = SystemName;
                contact.AnnualRunTime = RunTime;
                contact.ProjectKey = GlobalClass.Project.ProjectKey;
                contact.CoolingSystemTypeKey = SystemTypeKey;
                contact.FuelTypeKey = FuelTypeKey;
                contact.FuelCost = FCost;
                contact.EfficiencyType = EfficiencyTypeKey;
                contact.EfficiencyValue = EfficiencyValue;               
                contact.InternalNote = "n/a";
                contact.GeneralNote = "n/a";
                var temp = (from x in db.CoolingSystem where x.ProjectKey == GlobalClass.Project.ProjectKey select x).ToList();
                if (temp.Count() > 0)
                {
                    contact.IsDefault = false;
                }
                else
                {
                    contact.IsDefault = true;
                }
                db.CoolingSystem.Add(contact);
                var task = db.SaveChangesAsync();
                await task;
                return Content("success");
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveNewHeatingSystem(string SystemName1, decimal? RunTime1, Guid? SystemTypeKey1, Guid? FuelTypeKey1, decimal? FCost1, Guid? EfficiencyTypeKey1, decimal? EfficiencyValue1)
        {
            try
            {
                HeatingSystem contact = new HeatingSystem();
                contact.HeatingSystemKey = Guid.NewGuid();
                contact.SystemName = SystemName1;
                contact.AnnualRunTime = RunTime1;
                contact.ProjectKey = GlobalClass.Project.ProjectKey;
                contact.HeatingSystemTypeKey = SystemTypeKey1;
                contact.FuelTypeKey = FuelTypeKey1;
                contact.FuelCost = FCost1;
                contact.EfficiencyType = EfficiencyTypeKey1;
                contact.EfficiencyValue = EfficiencyValue1;
                contact.InternalNote = "n/a";
                contact.GeneralNote = "n/a";
                var temp = (from x in db.HeatingSystem where x.ProjectKey == GlobalClass.Project.ProjectKey select x).ToList();
                if (temp.Count() > 0)
                {
                    contact.IsDefault = false;
                }
                else
                {
                    contact.IsDefault = true;
                }
                db.HeatingSystem.Add(contact);
                var task = db.SaveChangesAsync();
                await task;
                return Content("success");
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
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