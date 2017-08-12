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
using NasgledSys.Validation;
namespace NasgledSys.Controllers
{
    public class MgtModulesController : Controller
    {
        // GET: MgtModules
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()//get all Institute
        {
            try
            {
                var temp = (from x in db.Modules
                            where x.IsDelete == false
                            select new ModuleClass
                            {
                                ModuleID = x.ModuleID,
                                ModuleName = x.ModuleName,
                                ModuleLevel = x.ModuleLevel,
                                NoSub = x.NoSub,
                                IsDelete = x.IsDelete,
                                ParentModuleID = x.ParentModuleID,
                                MainModuleName = x.ParentModuleID == null ? "--" : db.Modules.Where(m => m.ParentModuleID ==x.ModuleID).Select(d => d.ModuleName).FirstOrDefault(),
                                   sub = x.NoSub == true ? "Yes" : "No"
                            }).OrderBy(m => m.ModuleName).ToList();
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
                ViewBag.ParentModuleID = new SelectList(db.Modules.Where(m => m.IsDelete == false).OrderBy(m => m.ModuleName), "ModuleID", "ModuleName");
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "AdminLogin", "Home"));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModuleClass model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modules obj = new Modules();
                    obj.ModuleLevel = model.ModuleLevel;
                    obj.ModuleName = model.ModuleName;
                    obj.NoSub = model.NoSub;
                    obj.ParentModuleID = model.ParentModuleID;
                    obj.IsDelete = false;
                    obj.ModuleID = Guid.NewGuid();
                    db.Modules.Add(obj);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ParentModuleID = new SelectList(db.Modules.Where(m => m.IsDelete == false).OrderBy(m => m.ModuleName), "ModuleID", "ModuleName", model.ParentModuleID);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtModules", "Create"));
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
                Modules model = db.Modules.Find(id);
                ModuleClass obj = new ModuleClass();
                obj.ModuleLevel = model.ModuleLevel;
                obj.ModuleName = model.ModuleName;
                obj.NoSub = model.NoSub;
                obj.ParentModuleID = model.ParentModuleID;
               
                obj.ModuleID = model.ModuleID;
                ViewBag.ParentModuleID = new SelectList(db.Modules.Where(m => m.IsDelete == false).OrderBy(m => m.ModuleName), "ModuleID", "ModuleName", model.ParentModuleID);

                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtModules", "Index"));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModuleClass model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modules obj = db.Modules.Find(model.ModuleID);

                    obj.ModuleLevel = model.ModuleLevel;
                    obj.ModuleName = model.ModuleName;
                    obj.NoSub = model.NoSub;
                    obj.ParentModuleID = model.ParentModuleID;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ParentModuleID = new SelectList(db.Modules.Where(m => m.IsDelete == false).OrderBy(m => m.ModuleName), "ModuleID", "ModuleName", model.ParentModuleID);

                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtModules", "Index"));
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
                Modules company = db.Modules.Find(id);
                company.IsDelete = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtModules", "Index"));
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