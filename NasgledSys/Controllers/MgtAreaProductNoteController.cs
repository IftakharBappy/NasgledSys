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
    public class MgtAreaProductNoteController : Controller
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        private ManageAreaProduct manage = new ManageAreaProduct();
        private ManageProjectArea manageArea = new ManageProjectArea();
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

                    if (!(string.IsNullOrEmpty(model.InternalNotes) && string.IsNullOrEmpty(model.GeneralNote) && string.IsNullOrEmpty(model.Description) && string.IsNullOrEmpty(model.Condition)))
                    {
                        if (string.IsNullOrEmpty(model.InternalNotes)) model.InternalNotes = "not required";
                        if (string.IsNullOrEmpty(model.GeneralNote)) model.GeneralNote = "not required";
                        if (string.IsNullOrEmpty(model.Condition)) model.Condition = "not required";
                        if (string.IsNullOrEmpty(model.Description)) model.Description = "not required";
                        if (model.NoteKey == null || model.NoteKey == Guid.Empty)
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
                        Session["GlobalMessege"] = "Notes Saved Successfully";
                        Session["counter"] = 1;
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