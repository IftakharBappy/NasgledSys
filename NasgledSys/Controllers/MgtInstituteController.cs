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
    public class MgtInstituteController : Controller
    {
        // GET: MgtInstitute
        private NasgledDBEntities db = new NasgledDBEntities();
        private FormValidation val = new FormValidation();
        private MgtInstituteInfo manage = new MgtInstituteInfo();
        public ActionResult Index()//get all Institute
        {
            try
            {
                var temp = (from x in db.Company where x.IsDelete == false select new CompanyClass { CompanyID = x.CompanyID, CompanyKey = x.CompanyKey, CompanyName = x.CompanyName, CompanyAddress = x.CompanyAddress, CompanyPhone = x.CompanyPhone }).OrderBy(m => m.CompanyName).ToList();
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
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Company company = db.Company.Find(id);
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
                return View("Error", new HandleErrorInfo(e, "Home", "AIndex"));
            }
        }       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        byte[] data = null;
                        using (Stream inputStream = file.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            data = memoryStream.ToArray();
                        }
                        company.Logo = data;
                        company.LogoType = file.ContentType;
                    }
                    company = val.ValidateCompany(company);
                    company.IsDelete = false;
                    company.CompanyKey = Guid.NewGuid();
                    db.Company.Add(company);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(company);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "Create"));
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
                Company company = db.Company.Find(id);
                if (company == null)
                {
                    return HttpNotFound();
                }
                return View(company);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "Index"));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Company obj = db.Company.Find(company.CompanyKey);
                    if (file != null)
                    {
                        byte[] data = null;
                        using (Stream inputStream = file.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            data = memoryStream.ToArray();
                        }
                        obj.Logo = data;
                        obj.LogoType = file.ContentType;
                    }
                    company = val.ValidateCompany(company);
                    obj.CompanyID = company.CompanyID;
                    obj.CompanyName = company.CompanyName;
                    obj.CityKey = company.CityKey;
                    obj.StateCode = company.StateCode;
                    obj.CityKey = company.CityKey;
                    obj.CompanyAddress = company.CompanyAddress;
                    obj.CompanyPhone = company.CompanyPhone;
                    obj.CompanyMobile = company.CompanyMobile;
                    obj.CompanyEmail = company.CompanyEmail;
                    obj.CompanyWebsite = company.CompanyWebsite;
                    obj.CompanyFax = company.CompanyFax;
                    obj.ContactPersonName = company.ContactPersonName;
                    obj.ContactPersonNo = company.ContactPersonNo;
                    obj.Title = company.Title;
                    obj.ContactEmail = company.ContactEmail;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(company);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "Index"));
            }
        }
        public ActionResult UserEdit()
        {
            try
            {
                Company company = db.Company.Find(GlobalClass.Company.CompanyKey);
                CompanyClass model = manage.FillCompanyInfo(company);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "UserDetails"));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(CompanyClass company, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Company obj = db.Company.Find(company.CompanyKey);
                    if (file != null)
                    {
                        byte[] data = null;
                        using (Stream inputStream = file.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            data = memoryStream.ToArray();
                        }
                        obj.Logo = data;
                        obj.LogoType = file.ContentType;
                    }
                    company = val.ValidateCompanyClass(company);
                    obj.CompanyID = company.CompanyID;
                    obj.CompanyName = company.CompanyName;
                    obj.Zipcode = company.Zipcode;
                    obj.StateCode = company.StateCode;
                    obj.CityKey = company.CityKey;
                    obj.CompanyAddress = company.CompanyAddress;
                    obj.CompanyPhone = company.CompanyPhone;
                    obj.CompanyMobile = company.CompanyMobile;
                    obj.CompanyEmail = company.CompanyEmail;
                    obj.CompanyWebsite = company.CompanyWebsite;
                    obj.CompanyFax = company.CompanyFax;
                    obj.ContactPersonName = company.ContactPersonName;
                    obj.ContactPersonNo = company.ContactPersonNo;
                    obj.Title = company.Title;
                    obj.ContactEmail = company.ContactEmail;
                    db.SaveChanges();
                    return RedirectToAction("UserDetails");
                }
                return View(company);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtInstitute", "UserDetails"));
            }
        }
        public ActionResult UserDetails()
        {
            try
            {

                Company company = db.Company.Find(GlobalClass.Company.CompanyKey);
                if (company == null)
                {
                    return HttpNotFound();
                }
                return View(company);

            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
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
                Company company = db.Company.Find(id);
                company.IsDelete = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e,"Home","Index"));
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