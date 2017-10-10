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
    public class MgtUsergroupController : Controller
    {
        // GET: MgtUsergroup
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index(Guid id)
        {
            try
            {
                GlobalClass.Company = db.Company.Find(id);
               
                var temp = from x in db.Usergroup where x.CompanyKey == id select x;
                temp = temp.OrderBy(m => m.GroupName);
                return View(temp.ToList());
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "AIndex"));
            }
        }
        public ActionResult Details(Guid id)
        {
            try
            {
                Usergroup company = db.Usergroup.Find(id);
                if (company == null)
                {
                    return HttpNotFound();
                }
                return View(company);

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
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "Index"));
            }
        }
        [HttpPost]
        public ActionResult Create(Usergroup model, Guid?[] formlist)
        {
            try
            {
                Usergroup abm = new Usergroup();
                abm.GroupName = model.GroupName;
                abm.GroupID = model.GroupID;
                abm.UserGroupKey = Guid.NewGuid();
                abm.CompanyKey = GlobalClass.Company.CompanyKey;
                db.Usergroup.Add(abm);
                db.SaveChanges();
                if (formlist == null)
                {

                }
                else
                {
                    if (formlist.Count() > 0)
                    {
                        foreach (var item in formlist)
                        {
                            db = new NasgledDBEntities();
                            Forms f = db.Forms.Find(item);
                            CheckForModule(f.ModuleID, abm.UserGroupKey);
                            UserGroupForm obj = new UserGroupForm();
                           
                            obj.ModuleKey = f.ModuleID;
                            obj.UserGroupKey = abm.UserGroupKey;
                            obj.UserGroupFormKey = Guid.NewGuid();
                            obj.FormKey = item;
                            db.UserGroupForm.Add(obj);
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Index",new { id= GlobalClass.Company.CompanyKey });
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "Index"));
            }
        }

        public ActionResult Edit(Guid id)
        {
           try
            {
                Usergroup obj = db.Usergroup.Find(id);
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "Index"));
            }
        }
        [HttpPost]
        public ActionResult Edit(Usergroup model, Guid?[] formlist, Guid?[] DelList)
        {
            try
            {
                Usergroup abm = db.Usergroup.Find(model.UserGroupKey);
                abm.GroupName = model.GroupName;
                abm.GroupID = model.GroupID;
                db.SaveChanges();
                if (formlist == null)
                {

                }
                else
                {
                    if (formlist.Count() > 0)
                    {
                        foreach (var item in formlist)
                        {
                            db = new NasgledDBEntities();
                            Forms f = db.Forms.Find(item);
                            CheckForModule(f.ModuleID, model.UserGroupKey);
                            UserGroupForm obj = new UserGroupForm();
                           
                            obj.ModuleKey = f.ModuleID;
                            obj.UserGroupKey = abm.UserGroupKey;
                            obj.UserGroupFormKey = Guid.NewGuid();
                            obj.FormKey = item;
                            db.UserGroupForm.Add(obj);
                            db.SaveChanges();
                        }
                    }
                }
                if (DelList == null)
                {

                }
                else
                {
                    if (DelList.Count() > 0)
                    {
                        foreach (var item in DelList)
                        {
                            db = new NasgledDBEntities();
                            UserGroupForm f = db.UserGroupForm.Find(item);
                            CheckForModuleBeforDelete(f);
                            db.UserGroupForm.Remove(f);
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Index",new { id= GlobalClass.Company.CompanyKey });
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "Index"));
            }
        }

        private void CheckForModuleBeforDelete(UserGroupForm f)
        {
            NasgledDBEntities ac = new NasgledDBEntities();
            var temp = from x in ac.UserGroupForm where x.UserGroupKey == f.UserGroupKey && x.ModuleKey == f.ModuleKey  select x;
            if (temp.Count() > 0)
            {

            }
            else
            {
                UserGroupModule obj = db.UserGroupModule.FirstOrDefault(x => x.UserGroupKey == f.UserGroupKey && x.ModuleKey == f.ModuleKey);
                ac.UserGroupModule.Remove(obj);
                ac.SaveChanges();
            }
        }

        private void CheckForModule(Guid moduleID, Guid UserGroupKey)
        {
            NasgledDBEntities ac = new NasgledDBEntities();
            var temp = from x in ac.UserGroupModule where x.UserGroupKey == UserGroupKey && x.ModuleKey == moduleID  select x;
            if (temp.Count() > 0)
            {

            }
            else
            {
                UserGroupModule obj = new UserGroupModule();
               
                obj.ModuleKey = moduleID;
                obj.UserGroupKey = UserGroupKey;
                obj.UserGroupModuleKey = Guid.NewGuid();
                ac.UserGroupModule.Add(obj);
                ac.SaveChanges();
            }
        }

        public ActionResult Delete(Guid? id)
        {
            try
            {

                Usergroup company = db.Usergroup.Find(id);
                var temp = from x in db.UserGroupForm where x.UserGroupKey == id select x;
                var temp2 = from x in db.UserGroupModule where x.UserGroupKey == id select x;
                var temp3 = from x in db.StaffList where x.Usergr == id select x;
                if (temp.Count() > 0)
                {
                    foreach (var a in temp)
                    {
                        NasgledDBEntities ac = new NasgledDBEntities();
                        UserGroupForm form = db.UserGroupForm.Find(a.UserGroupFormKey);
                        ac.UserGroupForm.Remove(form);
                        ac.SaveChanges();
                    }
                }
                if (temp3.Count() > 0)
                {
                    foreach (var a in temp3)
                    {
                        NasgledDBEntities ac = new NasgledDBEntities();
                        StaffList form = db.StaffList.Find(a.PersonnelKey);
                        form.Usergr = null;
                        ac.SaveChanges();
                    }
                }
                if (temp2.Count() > 0)
                {
                    foreach (var a in temp2)
                    {
                        NasgledDBEntities ac = new NasgledDBEntities();
                        UserGroupModule form = db.UserGroupModule.Find(a.UserGroupModuleKey);
                        ac.UserGroupModule.Remove(form);
                        ac.SaveChanges();
                    }
                }
                db.Usergroup.Remove(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "Index"));
            }
        }


        #region For NASGLED
        public ActionResult DIndex()
        {
            try
            {              

                var temp = from x in db.Usergroup where x.CompanyKey == GlobalClass.Company.CompanyKey select x;
                temp = temp.OrderBy(m => m.GroupName);
                return View(temp.ToList());
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }
        public ActionResult DDetails(Guid id)
        {
            try
            {
                Usergroup company = db.Usergroup.Find(id);
                if (company == null)
                {
                    return HttpNotFound();
                }
                return View(company);

            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtUsergroup", "DIndex"));
            }
        }

        public ActionResult DCreate()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtUsergroup", "DIndex"));
            }
        }
        [HttpPost]
        public ActionResult DCreate(Usergroup model, Guid?[] formlist)
        {
            try
            {
                Usergroup abm = new Usergroup();
                abm.GroupName = model.GroupName;
                abm.GroupID = model.GroupID;
                abm.UserGroupKey = Guid.NewGuid();
                abm.CompanyKey = GlobalClass.Company.CompanyKey;
                db.Usergroup.Add(abm);
                db.SaveChanges();
                if (formlist == null)
                {

                }
                else
                {
                    if (formlist.Count() > 0)
                    {
                        foreach (var item in formlist)
                        {
                            db = new NasgledDBEntities();
                            Forms f = db.Forms.Find(item);
                            CheckForModule(f.ModuleID, abm.UserGroupKey);
                            UserGroupForm obj = new UserGroupForm();

                            obj.ModuleKey = f.ModuleID;
                            obj.UserGroupKey = abm.UserGroupKey;
                            obj.UserGroupFormKey = Guid.NewGuid();
                            obj.FormKey = item;
                            db.UserGroupForm.Add(obj);
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("DIndex");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtUsergroup", "DIndex"));
            }
        }

        public ActionResult DEdit(Guid id)
        {
            try
            {
                Usergroup obj = db.Usergroup.Find(id);
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtUsergroup", "DIndex"));
            }
        }
        [HttpPost]
        public ActionResult DEdit(Usergroup model, Guid?[] formlist, Guid?[] DelList)
        {
            try
            {
                Usergroup abm = db.Usergroup.Find(model.UserGroupKey);
                abm.GroupName = model.GroupName;
                abm.GroupID = model.GroupID;
                db.SaveChanges();
                if (formlist == null)
                {

                }
                else
                {
                    if (formlist.Count() > 0)
                    {
                        foreach (var item in formlist)
                        {
                            db = new NasgledDBEntities();
                            Forms f = db.Forms.Find(item);
                            CheckForModule(f.ModuleID, model.UserGroupKey);
                            UserGroupForm obj = new UserGroupForm();

                            obj.ModuleKey = f.ModuleID;
                            obj.UserGroupKey = abm.UserGroupKey;
                            obj.UserGroupFormKey = Guid.NewGuid();
                            obj.FormKey = item;
                            db.UserGroupForm.Add(obj);
                            db.SaveChanges();
                        }
                    }
                }
                if (DelList == null)
                {

                }
                else
                {
                    if (DelList.Count() > 0)
                    {
                        foreach (var item in DelList)
                        {
                            db = new NasgledDBEntities();
                            UserGroupForm f = db.UserGroupForm.Find(item);
                            CheckForModuleBeforDelete(f);
                            db.UserGroupForm.Remove(f);
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("DIndex");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtUsergroup", "DIndex"));
            }
        }

       

        public ActionResult DDelete(Guid? id)
        {
            try
            {

               
                var temp = from x in db.UserGroupForm where x.UserGroupKey == id select x;
                var temp2 = from x in db.UserGroupModule where x.UserGroupKey == id select x;
                var temp3 = from x in db.StaffList where x.Usergr == id select x;
                if (temp.Count() > 0)
                {
                    foreach (var a in temp)
                    {
                        NasgledDBEntities ac = new NasgledDBEntities();
                        UserGroupForm form = db.UserGroupForm.Find(a.UserGroupFormKey);
                        ac.UserGroupForm.Remove(form);
                        ac.SaveChanges();
                    }
                }
                if (temp3.Count() > 0)
                {
                    foreach (var a in temp3)
                    {
                        NasgledDBEntities ac = new NasgledDBEntities();
                        StaffList form = db.StaffList.Find(a.PersonnelKey);
                        form.Usergr = null;
                        ac.SaveChanges();
                    }
                }
                if (temp2.Count() > 0)
                {
                    foreach (var a in temp2)
                    {
                        NasgledDBEntities ac = new NasgledDBEntities();
                        UserGroupModule form = ac.UserGroupModule.FirstOrDefault(m=>m.UserGroupModuleKey==a.UserGroupModuleKey);
                         ac.UserGroupModule.Remove(form);
                        ac.SaveChanges();
                    }
                }
                db = new NasgledDBEntities();
                Usergroup company = db.Usergroup.Find(id);
                db.Usergroup.Remove(company);
                db.SaveChanges();
                return RedirectToAction("DIndex");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtUsergroup", "DIndex"));
            }
        }
        #endregion

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