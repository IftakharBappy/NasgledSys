using NasgledSys.DAL;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtClientContactController : Controller
    {
        private MgtClientContact manage = new MgtClientContact();
        private NasgledDBEntities db = new NasgledDBEntities();
        // GET: MgtClientContact
        public ActionResult Index()
        {
            try
            {
                ClientContactClass obj = new ClientContactClass();
                obj.ClientContactList = new List<ClientContactClass>();
                obj.ClientContactList = manage.ListAll();
                
                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName), "CityKey", "CityName");
                ViewBag.StateKey = new SelectList(db.StateList.OrderBy(m => m.StateName), "Pkey", "StateName");

                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtClientContact", "Index"));
            }
        }

        [HttpPost]
        public JsonResult Create(ClientContactClass ClientContact)
        {
            
                return Json(manage.Create(ClientContact), JsonRequestBehavior.AllowGet);

        }
        public ActionResult Details(Guid? ContactKey)
        {
            try
            {
                if (ContactKey == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClientContact ClientContact = db.ClientContact.Find(ContactKey);
                if (ClientContact == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false && m.CityKey == ClientContact.CityKey).OrderBy(m => m.CityName), "CityKey", "CityName");
                ViewBag.StateKey = new SelectList(db.StateList.Where(m => m.IsDelete == false && m.PKey == ClientContact.StateKey).OrderBy(m => m.StateName), "Pkey", "StateName");

                return View(ClientContact);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtClientContact", "Index"));
            }
        }
        public ActionResult Edit(Guid? ContactKey)
        {
            try
            {
                if (ContactKey == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClientContact ClientContact = db.ClientContact.Find(ContactKey);
                if (ClientContact == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName), "CityKey", "CityName", ClientContact.CityKey);
                ViewBag.StateKey = new SelectList(db.StateList.Where(m => m.IsDelete == false).OrderBy(m => m.StateName), "Pkey", "StateName", ClientContact.StateKey);
                return View(ClientContact);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtClientContact", "Index"));
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ClientContact obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClientContact model = db.ClientContact.Find(obj.ContactKey);
                    model.FirstName = obj.FirstName;
                    model.LastName = obj.LastName;
                    model.Email = obj.Email;
                    model.WebAddress = obj.WebAddress;
                    model.CityKey = obj.CityKey;
                    model.Address = obj.Address;
                    model.Address2 = obj.Address2;
                    model.JobTitle = obj.JobTitle;
                    model.StateKey = obj.StateKey;

                    model.CellPhone = obj.CellPhone;
                    model.OfficePhone = obj.OfficePhone;
                    model.InternalNote = obj.InternalNote;
                    model.GeneralNote = obj.GeneralNote;
                    model.Zipcode = obj.Zipcode;
                    model.FaxPhone = obj.FaxPhone;
                    db.SaveChanges();
                    Session["GlobalMessege"] = "Contact Info is Updated";
                    return RedirectToAction("Index", new { id = obj.ContactKey });
                }
                ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName), "CityKey", "CityName", obj.CityKey);
                ViewBag.StateKey = new SelectList(db.StateList.Where(m => m.IsDelete == false).OrderBy(m => m.StateName), "Pkey", "StateName", obj.StateKey);

                return View(obj);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "MgtClientContact", "Index"));
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