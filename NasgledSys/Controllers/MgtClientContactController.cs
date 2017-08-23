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
            ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName), "CityKey", "CityName");
            ViewBag.StateKey = new SelectList(db.StateList.OrderBy(m => m.StateName), "Pkey", "StateName");
            ViewBag.ProfileKey = new SelectList(db.UserProfile.OrderBy(m => m.Email), "ProfileKey");
            return View();
        }

        [HttpPost]
        public JsonResult Create(ClientContactClass ClientContact)
        {
            return Json(manage.Create(ClientContact), JsonRequestBehavior.AllowGet);

        }
        public ActionResult Details(Guid? ContactKey)
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
            ViewBag.ProfileKey = new SelectList(db.UserProfile.OrderBy(m => m.Email), "ProfileKey");
            return View(ClientContact);
        }
        public ActionResult Edit(Guid? ContactKey)
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
            ViewBag.ProfileKey = new SelectList(db.UserProfile.OrderBy(m => m.Email), "ProfileKey");
            return View(ClientContact);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ClientContact ClientContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ClientContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = ClientContact.ContactKey });
            }
            ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false && m.CityKey == ClientContact.CityKey).OrderBy(m => m.CityName), "CityKey", "CityName");
            ViewBag.StateKey = new SelectList(db.StateList.Where(m => m.IsDelete == false && m.PKey == ClientContact.StateKey).OrderBy(m => m.StateName), "Pkey", "StateName");
            ViewBag.ProfileKey = new SelectList(db.UserProfile.OrderBy(m => m.Email), "ProfileKey");
            return View(ClientContact);
        }
    }
}