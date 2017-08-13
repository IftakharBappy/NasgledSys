using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NasgledSys.Models;
using System.IO;
namespace NasgledSys.Controllers
{
    public class MgtFormsController : Controller
    {
        // GET: MgtForms
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()//get all Institute
        {
            try
            {
                var temp = (from x in db.Forms
                            where x.IsDelete == false
                            select x).OrderBy(m => m.FormName).ToList();
                return View(temp.ToList());
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "AIndex"));
            }
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.ModuleID = new SelectList(db.Modules.Where(m => m.IsDelete == false).OrderBy(m => m.ModuleName), "ModuleID", "ModuleName");
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Index", "MgtForms"));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Forms model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Forms obj = new Forms();
                    obj.ModuleID = model.ModuleID;
                    obj.FormName = model.FormName;
                    obj.FormLevel = model.FormLevel;
                    obj.FormController = model.FormController;
                    obj.FormView = model.FormView;
                    obj.IsDelete = false;
                    obj.FormID = Guid.NewGuid();
                    db.Forms.Add(obj);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ModuleID = new SelectList(db.Modules.Where(m => m.IsDelete == false).OrderBy(m => m.ModuleName), "ModuleID", "ModuleName",model.ModuleID);

                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Index", "MgtForms"));
            }
        }
        public ActionResult Edit(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Forms model = db.Forms.Find(id);
              
                ViewBag.ModuleID = new SelectList(db.Modules.Where(m => m.IsDelete == false).OrderBy(m => m.ModuleName), "ModuleID", "ModuleName", model.ModuleID);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Index", "MgtForms"));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Forms model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Forms obj = db.Forms.Find(model.FormID);
                    obj.ModuleID = model.ModuleID;
                    obj.FormName = model.FormName;
                    obj.FormLevel = model.FormLevel;
                    obj.FormController = model.FormController;
                    obj.FormView = model.FormView;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ModuleID = new SelectList(db.Modules.Where(m => m.IsDelete == false).OrderBy(m => m.ModuleName), "ModuleID", "ModuleName", model.ModuleID);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Index", "MgtForms"));
            }
        }


        public ActionResult Delete(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Forms obj = db.Forms.Find(id);
                obj.IsDelete = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Index", "MgtForms"));
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