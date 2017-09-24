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
    public class MgtAreaProductDetailController : Controller
    {
        // GET: MgtAreaProductDetail
        private NasgledDBEntities db = new NasgledDBEntities();
        private ManageAreaProduct manage = new ManageAreaProduct();
        private ManageProjectArea manageArea = new ManageProjectArea();

        public ActionResult Details(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                ProductDetailClass obj = new ProductDetailClass();
                Session["GlobalMessege"] = "";
                try
                {
                    AreaProduct model = db.AreaProduct.Find(id);
                    Area area = db.Area.Find(model.AreaKey);
                    AreaProductDetail detail = db.AreaProductDetail.FirstOrDefault(m=>m.ProductKey==id);
                    if (detail == null)
                    {                       
                        obj.ProductKey = model.ProductKey;
                        obj.AreaKey = model.AreaKey;
                        obj.FixtureKey = model.FixtureKey;
                        obj.DetailKey = Guid.Empty;
                        obj.ExistingControl = false;
                        obj.OperationScheduleKey = area.OperationScheduleKey;
                        obj.WorkingFixtureCount = model.Count;
                        obj.ReplacementCost = 0;
                        obj.AnnualMaintenance = 0;
                        obj.Location = "--";
                        obj.Year = System.DateTime.Now.Year;
                        obj.LightingSatisfactionKey = Guid.Empty;
                        obj.MountingHeight = 0;
                    }
                    else
                    {
                        obj.ProductKey = model.ProductKey;
                        obj.AreaKey = model.AreaKey;
                        obj.FixtureKey = model.FixtureKey;
                        obj.DetailKey = detail.DetailKey;
                        obj.ExistingControl = detail.ExistingControl;
                        obj.OperationScheduleKey = detail.OperationScheduleKey;
                        obj.WorkingFixtureCount =detail.WorkingFixtureCount;
                        obj.ReplacementCost = detail.ReplacementCost;
                        obj.AnnualMaintenance = detail.AnnualMaintenance;
                        obj.Location = detail.Location;
                        obj.Year = detail.Year;
                        obj.LightingSatisfactionKey = detail.LightingSatisfactionKey;
                        obj.MountingHeight = detail.MountingHeight;
                    }
                   
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

        public ActionResult Details(ProductDetailClass model)
        {
            if (GlobalClass.MasterSession)
            {
               
                try
                {
                    if (model.ExistingControl == null) model.ExistingControl = true;
                    if (model.WorkingFixtureCount == null) model.WorkingFixtureCount = 0;
                    if (model.ReplacementCost == null) model.ReplacementCost = 0;
                    if (model.AnnualMaintenance == null) model.AnnualMaintenance = 0;
                    if (string.IsNullOrEmpty(model.Location)) model.Location = "--";
                    if (model.MountingHeight == null) model.MountingHeight = 0;
                    if (ModelState.IsValid)
                    {
                        if (model.DetailKey == null|| model.DetailKey==Guid.Empty)
                        {
                            AreaProductDetail obj = new AreaProductDetail();
                            obj.DetailKey = Guid.NewGuid();
                            obj.ProductKey = model.ProductKey;
                            obj.AreaKey = model.AreaKey;
                            obj.FixtureKey = model.FixtureKey;

                            obj.ExistingControl = model.ExistingControl;
                            obj.OperationScheduleKey = model.OperationScheduleKey;
                            obj.WorkingFixtureCount = model.WorkingFixtureCount;
                            obj.ReplacementCost = model.ReplacementCost;
                            obj.AnnualMaintenance = model.AnnualMaintenance;
                            obj.Location = model.Location;
                            obj.Year = model.Year;
                            obj.LightingSatisfactionKey = model.LightingSatisfactionKey;
                            obj.MountingHeight = model.MountingHeight;
                            db.AreaProductDetail.Add(obj);
                            db.SaveChanges();

                            model.ProductKey = obj.ProductKey;
                            model.AreaKey = obj.AreaKey;
                            model.FixtureKey = obj.FixtureKey;
                            model.DetailKey = obj.DetailKey;
                            model.ExistingControl = obj.ExistingControl;
                            model.OperationScheduleKey = obj.OperationScheduleKey;
                            model.WorkingFixtureCount = obj.WorkingFixtureCount;
                            model.ReplacementCost = obj.ReplacementCost;
                            model.AnnualMaintenance = obj.AnnualMaintenance;
                            model.Location = obj.Location;
                            model.Year = obj.Year;
                            model.LightingSatisfactionKey = obj.LightingSatisfactionKey;
                            model.MountingHeight = obj.MountingHeight;
                        }
                        else
                        {
                            AreaProductDetail obj = db.AreaProductDetail.Find(model.DetailKey);

                            obj.ExistingControl = model.ExistingControl;
                            obj.OperationScheduleKey = model.OperationScheduleKey;
                            obj.WorkingFixtureCount = model.WorkingFixtureCount;
                            obj.ReplacementCost = model.ReplacementCost;
                            obj.AnnualMaintenance = model.AnnualMaintenance;
                            obj.Location = model.Location;
                            obj.Year = model.Year;
                            obj.LightingSatisfactionKey = model.LightingSatisfactionKey;
                            obj.MountingHeight = model.MountingHeight;

                            db.SaveChanges();

                            model.ProductKey = obj.ProductKey;
                            model.AreaKey = obj.AreaKey;
                            model.FixtureKey = obj.FixtureKey;
                            model.DetailKey = obj.DetailKey;
                            model.ExistingControl = obj.ExistingControl;
                            model.OperationScheduleKey = obj.OperationScheduleKey;
                            model.WorkingFixtureCount = obj.WorkingFixtureCount;
                            model.ReplacementCost = obj.ReplacementCost;
                            model.AnnualMaintenance = obj.AnnualMaintenance;
                            model.Location = obj.Location;
                            model.Year = obj.Year;
                            model.LightingSatisfactionKey = obj.LightingSatisfactionKey;
                            model.MountingHeight = obj.MountingHeight;
                        }
                        Session["GlobalMessege"] = "Detail Saved Successfully";
                    }
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


        public ActionResult Notes(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                ProductNoteClass obj = new ProductNoteClass();
                Session["GlobalMessege"] = "";
                try
                {
                    AreaProduct model = db.AreaProduct.Find(id);
                    Area area = db.Area.Find(model.AreaKey);
                    AreaProductNote detail = db.AreaProductNote.FirstOrDefault(m => m.ProductKey == id);
                    if (detail == null)
                    {
                        obj.ProductKey = model.ProductKey;
                        obj.AreaKey = model.AreaKey;
                        obj.FixtureKey = model.FixtureKey;
                        obj.NoteKey = Guid.Empty;
                        obj.InternalNotes = "";
                        obj.GeneralNote = "";
                        obj.Condition = "";
                        obj.Description = "";
                       
                    }
                    else
                    {
                        obj.ProductKey = model.ProductKey;
                        obj.AreaKey = model.AreaKey;
                        obj.FixtureKey = model.FixtureKey;
                        obj.NoteKey = detail.NoteKey;
                        obj.Condition = detail.Condition;
                        obj.Description = detail.Description;
                        obj.InternalNotes = detail.InternalNotes;
                        obj.GeneralNote = detail.GeneralNote;
                       
                    }

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

        public ActionResult Notes(ProductNoteClass model)
        {
            if (GlobalClass.MasterSession)
            {

                try
                {
                   
                    if (!(string.IsNullOrEmpty(model.InternalNotes)&& string.IsNullOrEmpty(model.GeneralNote)&& string.IsNullOrEmpty(model.Description)&& string.IsNullOrEmpty(model.Condition)))
                    {
                        if (string.IsNullOrEmpty(model.InternalNotes)) model.InternalNotes = "not required";
                        if (string.IsNullOrEmpty(model.GeneralNote)) model.GeneralNote = "not required";
                        if (string.IsNullOrEmpty(model.Condition)) model.Condition = "not required";
                        if (string.IsNullOrEmpty(model.Description)) model.Description = "not required";
                        if (model.NoteKey == null|| model.NoteKey==Guid.Empty)
                        {
                            AreaProductNote obj = new AreaProductNote();
                            obj.NoteKey = Guid.NewGuid();
                            obj.ProductKey = model.ProductKey;
                            obj.AreaKey = model.AreaKey;
                            obj.FixtureKey = model.FixtureKey;

                            obj.InternalNotes = model.InternalNotes;
                            obj.GeneralNote = model.GeneralNote;
                            obj.Description = model.Description;
                            obj.Condition = model.Condition;
                         
                            db.AreaProductNote.Add(obj);
                            db.SaveChanges();

                            model.ProductKey = obj.ProductKey;
                            model.AreaKey = obj.AreaKey;
                            model.FixtureKey = obj.FixtureKey;
                            model.NoteKey = obj.NoteKey;
                            model.InternalNotes = obj.InternalNotes;
                            model.GeneralNote = obj.GeneralNote;
                            model.Description = obj.Description;
                            model.Condition = obj.Condition;                         
                        }
                        else
                        {
                            AreaProductNote obj = db.AreaProductNote.Find(model.NoteKey);

                            obj.InternalNotes = model.InternalNotes;
                            obj.GeneralNote = model.GeneralNote;
                            obj.Description = model.Description;
                            obj.Condition = model.Condition;

                            db.SaveChanges();

                            model.ProductKey = obj.ProductKey;
                            model.AreaKey = obj.AreaKey;
                            model.FixtureKey = obj.FixtureKey;
                            model.NoteKey = obj.NoteKey;
                            model.InternalNotes = obj.InternalNotes;
                            model.GeneralNote = obj.GeneralNote;
                            model.Description = obj.Description;
                            model.Condition = obj.Condition;
                        }
                        Session["GlobalMessege"] = "Detail Saved Successfully";
                    }
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