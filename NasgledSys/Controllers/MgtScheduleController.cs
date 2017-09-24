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
using System.Net;

namespace NasgledSys.Controllers
{
    public class MgtScheduleController : Controller
    {
        // GET: MgtSchedule
        private NasgledDBEntities db = new NasgledDBEntities();
        
        public ActionResult Index()
        {
            if (GlobalClass.MasterSession)
            {
                try
                {

                    var ProjectKey = GlobalClass.Project.ProjectKey;

                    ViewBag.OperatingSchedules = (from os in db.OperatingSchedule where os.ProjectKey == ProjectKey
                                                  select new ScheduleViewModel()
                                                  {
                                                      PKey = os.PKey,
                                                      OPName = os.OPName,
                                                      AnnualOperationHour = os.AnnualOperationHour,
                                                      InternalNote = os.InternalNote,
                                                      GeneralNote = os.GeneralNote,
                                                      IsDefault = os.IsDefault
                                                  }
                                                  ).ToList();
                    ViewBag.RateSchedule = (from os in db.NewRateSchedule
                                            where os.ProjectKey == ProjectKey
                                            select new NewRateScheduleViewModel()
                                                  {
                                                      PKey = os.PKey,
                                                      NRName = os.NRName,
                                                      BlendElectricityRate = os.BlendElectricityRate,
                                                      InternalNote = os.InternalNote,
                                                      GeneralNote = os.GeneralNote,
                                                      IsDefault = os.IsDefault
                                                  }
                                                  ).ToList();
                    ViewBag.HeatingSystem = (from os in db.HeatingSystem
                                            where os.ProjectKey == ProjectKey
                                            select new HeatingSystemViewModel()
                                            {
                                                HeatingSystemKey = os.HeatingSystemKey,
                                                SystemName = os.SystemName,
                                                AnnualRunTime = os.AnnualRunTime,
                                                HeatingSystemType = os.HeatingSystemType,
                                                FuelType = os.FuelType,
                                                FuelCost = os.FuelCost,
                                                EfficiencyType = os.EfficiencyType,
                                                EfficiencyValue = os.EfficiencyValue,
                                                InternalNote = os.InternalNote,
                                                GeneralNote = os.GeneralNote,
                                                IsDefault = os.IsDefault
                                            }
                                                  ).ToList();
                    ViewBag.CoolingSchedule = (from os in db.CoolingSystem
                                            where os.ProjectKey == ProjectKey
                                            select new CoolingSystemViewModel()
                                            {
                                                CoolingSystemKey = os.CoolingSystemKey,
                                                SystemName = os.SystemName,
                                                AnnualRunTime = os.AnnualRunTime,
                                                InternalNote = os.InternalNote,
                                                GeneralNote = os.GeneralNote,
                                                IsDefault = os.IsDefault
                                            }
                                                  ).ToList();

                    ViewBag.ProjectKey = GlobalClass.Project.ProjectKey;
                    

                    return View();
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

        public ActionResult CreateOS()
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    var model = new ScheduleViewModel();
                    return View("CreateOS", model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult CreateOS(ScheduleViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        OperatingSchedule model = new OperatingSchedule();
                        model.PKey = Guid.NewGuid();
                        model.ProjectKey = GlobalClass.Project.ProjectKey;
                        model.OPName = obj.OPName;
                        model.AnnualOperationHour = obj.AnnualOperationHour;
                        model.InternalNote = obj.InternalNote;
                        model.GeneralNote = obj.GeneralNote;
                        if (obj.InternalNote == null) { model.InternalNote = "N/A"; }
                        else { model.InternalNote = obj.InternalNote; }
                        if (obj.GeneralNote == null) { model.GeneralNote = "N/A"; }
                        else { model.GeneralNote = obj.GeneralNote; }

                        model.IsDefault = false;
                        

                        db.OperatingSchedule.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Operating Schedule is Saved Successfully";
                        return RedirectToAction("Index", "MgtSchedule", new { ProjectKey = model.ProjectKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult EditOS(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                ScheduleViewModel model = new ScheduleViewModel();
                model.PKey = id;
                try
                {
                    Session["GlobalMessege"] = " ";
                    if (db.OperatingSchedule.Any(o => o.PKey == id))
                    {
                        var OperatingSchedule = db.OperatingSchedule.FirstOrDefault(m => m.PKey == id);
                        model.OPName = OperatingSchedule.OPName;
                        model.AnnualOperationHour = OperatingSchedule.AnnualOperationHour;
                        model.InternalNote = OperatingSchedule.InternalNote;
                        model.GeneralNote = OperatingSchedule.GeneralNote;
                    }
                    else
                    {
                        model.PKey = id;
                       
                      
                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult EditOS(ScheduleViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        OperatingSchedule model = db.OperatingSchedule.Find(obj.PKey);
                        model.ProjectKey = GlobalClass.Project.ProjectKey;
                        model.OPName = obj.OPName;
                        model.AnnualOperationHour = obj.AnnualOperationHour;
                        model.InternalNote = obj.InternalNote;
                        model.GeneralNote = obj.GeneralNote;
                        if (obj.InternalNote == null) { model.InternalNote = "N/A"; }
                        else { model.InternalNote = obj.InternalNote; }
                        if (obj.GeneralNote == null) { model.GeneralNote = "N/A"; }
                        else { model.GeneralNote = obj.GeneralNote; }

                        model.IsDefault = false;

                        db.SaveChanges();
                        Session["GlobalMessege"] = "Operating Schedule is Update Successfully";
                        return RedirectToAction("Index", "MgtSchedule", new { ProjectKey = model.ProjectKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public ActionResult CreateHS()
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    var model = new HeatingSystemViewModel();
                    return View("CreateHS", model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult CreateHS(HeatingSystemViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        HeatingSystem model = new HeatingSystem();
                        model.HeatingSystemKey = Guid.NewGuid();
                        model.SystemName = obj.SystemName;
                        model.AnnualRunTime = obj.AnnualRunTime;
                        model.ProjectKey = GlobalClass.Project.ProjectKey;
                        model.HeatingSystemTypeKey = obj.HeatingSystemTypeKey;
                        model.FuelTypeKey = obj.FuelTypeKey;
                        model.FuelCost = obj.FuelCost;
                        model.EfficiencyType = obj.EfficiencyType;
                        model.EfficiencyValue = obj.EfficiencyValue;
                        model.InternalNote = obj.InternalNote;
                        model.GeneralNote = obj.GeneralNote;
                        if (obj.InternalNote == null) { model.InternalNote = "N/A"; }
                        else { model.InternalNote = obj.InternalNote; }
                        if (obj.GeneralNote == null) { model.GeneralNote = "N/A"; }
                        else { model.GeneralNote = obj.GeneralNote; }

                        model.IsDefault = false;


                        db.HeatingSystem.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Heating Schedule is Saved Successfully";
                        return RedirectToAction("Index", "MgtSchedule", new { ProjectKey = model.ProjectKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public ActionResult EditHS(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                HeatingSystemViewModel model = new HeatingSystemViewModel();
                model.HeatingSystemKey = id;
                try
                {
                    Session["GlobalMessege"] = " ";
                    if (db.HeatingSystem.Any(o => o.HeatingSystemKey == id))
                    {
                        var HeatingSchedule = db.HeatingSystem.FirstOrDefault(m => m.HeatingSystemKey == id);
                        model.SystemName = HeatingSchedule.SystemName;
                        model.AnnualRunTime = HeatingSchedule.AnnualRunTime;
                        model.InternalNote = HeatingSchedule.InternalNote;
                        model.GeneralNote = HeatingSchedule.GeneralNote;
                        model.HeatingSystemTypeKey = HeatingSchedule.HeatingSystemTypeKey;
                        model.FuelTypeKey = HeatingSchedule.FuelTypeKey;
                        model.FuelCost = HeatingSchedule.FuelCost;
                        model.EfficiencyType = HeatingSchedule.EfficiencyType;
                        model.EfficiencyValue = HeatingSchedule.EfficiencyValue;
                    }
                    else
                    {
                        model.HeatingSystemKey = Guid.Empty;


                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult EditHS(HeatingSystemViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        HeatingSystem model = db.HeatingSystem.Find(obj.HeatingSystemKey);
                        model.SystemName = obj.SystemName;
                        model.AnnualRunTime = obj.AnnualRunTime;
                        model.ProjectKey = GlobalClass.Project.ProjectKey;
                        model.HeatingSystemTypeKey = obj.HeatingSystemTypeKey;
                        model.FuelTypeKey = obj.FuelTypeKey;
                        model.FuelCost = obj.FuelCost;
                        model.EfficiencyType = obj.EfficiencyType;
                        model.EfficiencyValue = obj.EfficiencyValue;
                        model.InternalNote = obj.InternalNote;
                        model.GeneralNote = obj.GeneralNote;
                        if (obj.InternalNote == null) { model.InternalNote = "N/A"; }
                        else { model.InternalNote = obj.InternalNote; }
                        if (obj.GeneralNote == null) { model.GeneralNote = "N/A"; }
                        else { model.GeneralNote = obj.GeneralNote; }

                        model.IsDefault = false;

                        db.SaveChanges();
                        Session["GlobalMessege"] = "Heating Schedule is Updated Successfully";
                        return RedirectToAction("Index", "MgtSchedule", new { ProjectKey = model.ProjectKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult CreateRS()
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    var model = new NewRateScheduleViewModel();
                    return View("CreateRS", model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult CreateRS(NewRateScheduleViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        NewRateSchedule model = new NewRateSchedule();
                        model.PKey = Guid.NewGuid();
                        model.ProjectKey = GlobalClass.Project.ProjectKey;
                        model.NRName = obj.NRName;
                        model.BlendElectricityRate = obj.BlendElectricityRate;
                        model.InternalNote = obj.InternalNote;
                        model.GeneralNote = obj.GeneralNote;
                        if (obj.InternalNote == null) { model.InternalNote = "N/A"; }
                        else { model.InternalNote = obj.InternalNote; }
                        if (obj.GeneralNote == null) { model.GeneralNote = "N/A"; }
                        else { model.GeneralNote = obj.GeneralNote; }

                        model.IsDefault = false;


                        db.NewRateSchedule.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Rate Schedule is Saved Successfully";
                        return RedirectToAction("Index", "MgtSchedule", new { ProjectKey = model.ProjectKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public ActionResult EditRS(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                NewRateScheduleViewModel model = new NewRateScheduleViewModel();
                model.PKey = id;
                try
                {
                    Session["GlobalMessege"] = " ";
                    if (db.NewRateSchedule.Any(o => o.PKey == id))
                    {
                        var RateSchedule = db.NewRateSchedule.FirstOrDefault(m => m.PKey == id);
                        model.NRName = RateSchedule.NRName;
                        model.BlendElectricityRate = RateSchedule.BlendElectricityRate;
                        model.InternalNote = RateSchedule.InternalNote;
                        model.GeneralNote = RateSchedule.GeneralNote;
                      
                    }
                    else
                    {
                        model.PKey = Guid.Empty;


                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult EditRS(NewRateScheduleViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        NewRateSchedule model = db.NewRateSchedule.Find(obj.PKey);
                        model.ProjectKey = GlobalClass.Project.ProjectKey;
                        model.NRName = obj.NRName;
                        model.BlendElectricityRate = obj.BlendElectricityRate;
                        model.InternalNote = obj.InternalNote;
                        model.GeneralNote = obj.GeneralNote;
                        if (obj.InternalNote == null) { model.InternalNote = "N/A"; }
                        else { model.InternalNote = obj.InternalNote; }
                        if (obj.GeneralNote == null) { model.GeneralNote = "N/A"; }
                        else { model.GeneralNote = obj.GeneralNote; }

                        model.IsDefault = false;

                        db.SaveChanges();
                        Session["GlobalMessege"] = "Rate Schedule is Updated Successfully";
                        return RedirectToAction("Index", "MgtSchedule", new { ProjectKey = model.ProjectKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public ActionResult CreateCS()
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    var model = new CoolingSystemViewModel();
                    return View("CreateCS", model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult CreateCS(CoolingSystemViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        CoolingSystem model = new CoolingSystem();
                        model.CoolingSystemKey = Guid.NewGuid();
                        model.ProjectKey = GlobalClass.Project.ProjectKey;
                        model.SystemName = obj.SystemName;
                        model.AnnualRunTime = obj.AnnualRunTime;
                        model.CoolingSystemTypeKey = obj.CoolingSystemTypeKey;
                        model.FuelTypeKey = obj.FuelTypeKey;
                        model.FuelCost = obj.FuelCost;
                        model.EfficiencyType = obj.EfficiencyType;
                        model.EfficiencyValue = obj.EfficiencyValue;
                        model.InternalNote = obj.InternalNote;
                        model.GeneralNote = obj.GeneralNote;
                        if (obj.InternalNote == null) { model.InternalNote = "N/A"; }
                        else { model.InternalNote = obj.InternalNote; }
                        if (obj.GeneralNote == null) { model.GeneralNote = "N/A"; }
                        else { model.GeneralNote = obj.GeneralNote; }

                        model.IsDefault = false;


                        db.CoolingSystem.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Cooling Schedule is Saved Successfully";
                        return RedirectToAction("Index", "MgtSchedule", new { ProjectKey = model.ProjectKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        public ActionResult EditCS(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                CoolingSystemViewModel model = new CoolingSystemViewModel();
                model.CoolingSystemKey = id;
                try
                {
                    Session["GlobalMessege"] = " ";
                    if (db.CoolingSystem.Any(o => o.CoolingSystemKey == id))
                    {
                        var CoolingSchedule = db.CoolingSystem.FirstOrDefault(m => m.CoolingSystemKey == id);
                        model.SystemName = CoolingSchedule.SystemName;
                        model.AnnualRunTime = CoolingSchedule.AnnualRunTime;
                        model.InternalNote = CoolingSchedule.InternalNote;
                        model.GeneralNote = CoolingSchedule.GeneralNote;
                        model.CoolingSystemTypeKey = CoolingSchedule.CoolingSystemTypeKey;
                        model.FuelTypeKey = CoolingSchedule.FuelTypeKey;
                        model.FuelCost = CoolingSchedule.FuelCost;
                        model.EfficiencyType = CoolingSchedule.EfficiencyType;
                        model.EfficiencyValue = CoolingSchedule.EfficiencyValue;

                    }
                    else
                    {
                        model.CoolingSystemKey = Guid.Empty;


                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult EditCS(CoolingSystemViewModel obj)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        CoolingSystem model = db.CoolingSystem.Find(obj.CoolingSystemKey);
                        model.ProjectKey = GlobalClass.Project.ProjectKey;
                        model.SystemName = obj.SystemName;
                        model.AnnualRunTime = obj.AnnualRunTime;
                        model.CoolingSystemTypeKey = obj.CoolingSystemTypeKey;
                        model.FuelTypeKey = obj.FuelTypeKey;
                        model.FuelCost = obj.FuelCost;
                        model.EfficiencyType = obj.EfficiencyType;
                        model.EfficiencyValue = obj.EfficiencyValue;
                        model.InternalNote = obj.InternalNote;
                        model.GeneralNote = obj.GeneralNote;
                        if (obj.InternalNote == null) { model.InternalNote = "N/A"; }
                        else { model.InternalNote = obj.InternalNote; }
                        if (obj.GeneralNote == null) { model.GeneralNote = "N/A"; }
                        else { model.GeneralNote = obj.GeneralNote; }

                        model.IsDefault = false;

                        db.SaveChanges();
                        Session["GlobalMessege"] = "Cooling Schedule is Updated Successfully";
                        return RedirectToAction("Index", "MgtSchedule", new { ProjectKey = model.ProjectKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtSchedule", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public JsonResult LoadSystemTypeDropDown_ToCreate()
        {
            var list = (from st in db.HeatingSystemType where st.IsDelete == false select new { st.PKey, st.TypeName }) .ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadFuelTypeDropDown_ToCreate()
        {
            var list = (from ft in db.FuelType where ft.IsDelete == false select new { ft.PKey, ft.TypeName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadEfficiencyTypeDropDown_ToCreate()
        {
            var list = (from et in db.HeatingEfficiencyType where et.IsDelete == false select new { et.PKey, et.TypeName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadCoolingSystemTypeDropDown_ToCreate()
        {
            var list = (from cs in db.CoolingSystemType where cs.IsDelete == false select new { cs.PKey, cs.TypeName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadCoolingEfficiencyTypeDropDown_ToCreate()
        {
            var list = (from ce in db.CoolingEfficientyType where ce.IsDelete == false select new { ce.PKey, ce.TypeName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadSystemTypeDropDown_ToEdit(Guid HeatingSystemTypeKey)
        {
            var list = (from hs in db.HeatingSystemType
                        select new
                        {
                            hs.PKey,
                            hs.TypeName,
                            Selected = hs.PKey == HeatingSystemTypeKey ? "selected" : ""
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadFuelTypeDropDown_ToEdit(Guid FuelTypeKey)
        {
            var list = (from ft in db.FuelType
                        select new
                        {
                            ft.PKey,
                            ft.TypeName,
                            Selected = ft.PKey == FuelTypeKey ? "selected" : ""
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadEfficiencyTypDropDown_ToEdit(Guid EfficiencyType)
        {
            var list = (from et in db.HeatingEfficiencyType
                        select new
                        {
                            et.PKey,
                            et.TypeName,
                            Selected = et.PKey == EfficiencyType ? "selected" : ""
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadCoolingSystemTypeDropDown_ToEdit(Guid CoolingSystemTypeKey)
        {
            var list = (from et in db.HeatingEfficiencyType
                        select new
                        {
                            et.PKey,
                            et.TypeName,
                            Selected = et.PKey == CoolingSystemTypeKey ? "selected" : ""
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadCoolingEfficiencyDropDown_ToEdit(Guid EfficiencyType)
        {
            var list = (from et in db.HeatingEfficiencyType
                        select new
                        {
                            et.PKey,
                            et.TypeName,
                            Selected = et.PKey == EfficiencyType ? "selected" : ""
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}