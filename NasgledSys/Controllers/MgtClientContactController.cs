using Microsoft.Ajax.Utilities;
using NasgledSys.DAL;
using NasgledSys.Models;
using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // GET: MgtClientContact
        public ActionResult Index()
        {
            try
            {
                logger.Info("Mgt Client Contact Index() invoked by:"+GlobalClass.ProfileUser.FirstName+" " + GlobalClass.ProfileUser.LastName);

                ClientContactViewModel obj = new ClientContactViewModel();
                obj.ClientContactViewModelList = new List<ClientContactViewModel>();
                obj.ClientContactViewModelList = manage.ListAll();

                // Tab Data
                ThumbnailViewModel model = new ThumbnailViewModel();
                model.ThumbnailModelList = new List<ThumbnailModel>();

                // batch your List data for tab view i want batch by 2 you can set your value
               
                //var listOfBatches = obj.ClientContactViewModelList.Batch(2);
                var listOfBatches = obj.ClientContactViewModelList.Batch(6);

                int tabNo = 1;

                foreach (var batchItem in listOfBatches)
                {
                    // Generating tab
                    ThumbnailModel thumbObj = new ThumbnailModel();
                    thumbObj.ThumbnailLabel = "Lebel" + tabNo;
                    thumbObj.ThumbnailTabId = "tab" + tabNo;
                    thumbObj.ThumbnailTabNo = tabNo;
                    thumbObj.Thumbnail_Aria_Controls = "tab" + tabNo;
                    thumbObj.Thumbnail_Href = "#tab" + tabNo;

                    // batch details

                    thumbObj.ClientContactDetailsList = new List<ClientContactViewModel>();

                    foreach (var item in batchItem)
                    {
                        ClientContactViewModel detailsObj = new ClientContactViewModel();
                        detailsObj = item;
                        thumbObj.ClientContactDetailsList.Add(detailsObj);
                    }

                    model.ThumbnailModelList.Add(thumbObj);

                    tabNo++;
                }

                // Getting first tab data
                var first = model.ThumbnailModelList.FirstOrDefault();

                // Getting first tab data
                var last = model.ThumbnailModelList.LastOrDefault();

                foreach (var item in model.ThumbnailModelList)
                {
                    if (item.ThumbnailTabNo == first.ThumbnailTabNo)
                    {
                        item.Thumbnail_ItemPosition = "First";
                    }

                    if (item.ThumbnailTabNo == last.ThumbnailTabNo)
                    {
                        item.Thumbnail_ItemPosition = "Last";
                    }

                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Index");
                return View("Error", new HandleErrorInfo(ex, "Home", "UserHome"));
            }
        }

        public ActionResult Create()
        {
            logger.Info("Mgt Client Contact Create get invoked by:" + GlobalClass.ProfileUser.FirstName + " " + GlobalClass.ProfileUser.LastName);
            try {
                var model = new ClientContactViewModel();
                //ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName), "CityKey", "CityName");
                //ViewBag.StateKey = new SelectList(db.StateList.OrderBy(m => m.StateName), "Pkey", "StateName");
                return View("Create", model);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Create Get");
                return View("Error", new HandleErrorInfo(ex, "MgtClientContact", "Index"));
            }
        }

      [HttpPost]
        public ActionResult Create(ClientContactViewModel obj)
        {
            logger.Info("Mgt Client Contact Create Post invoked by:" + GlobalClass.ProfileUser.FirstName + " " + GlobalClass.ProfileUser.LastName);
            try
            {
                if (ModelState.IsValid)
                {
                    ClientContact model = new ClientContact();
                    model.ContactKey = Guid.NewGuid();
                    model.FirstName = obj.FirstName;
                    model.LastName = obj.LastName;
                    model.Email = obj.Email;
                    model.WebAddress = obj.WebAddress;
                    model.CityKey = obj.CityKey;
                    model.Address = obj.Address;
                    model.Address2 = obj.Address2;
                    model.JobTitle = obj.JobTitle;
                    model.StateKey = obj.StateKey;
                    model.ProfileKey = GlobalClass.ProfileUser.ProfileKey;
                    model.CellPhone = obj.CellPhone;
                    model.OfficePhone = obj.OfficePhone;
                    model.InternalNote = obj.InternalNote;
                    model.GeneralNote = obj.GeneralNote;
                    model.Zipcode = obj.Zipcode;
                    model.FaxPhone = obj.FaxPhone;
                    model.IsActive = true;
                    db.ClientContact.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName), "CityKey", "CityName", obj.CityKey);
                //ViewBag.StateKey = new SelectList(db.StateList.Where(m => m.IsDelete == false).OrderBy(m => m.StateName), "Pkey", "StateName", obj.StateKey);

                return View(obj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Create Post");
                return View("Error", new HandleErrorInfo(ex, "MgtClientContact", "Index"));
            }
        }
        public ActionResult Details(Guid? ContactKey)
        {
            logger.Info("Mgt Client Contact Details Post invoked by:" + GlobalClass.ProfileUser.FirstName + " " + GlobalClass.ProfileUser.LastName);
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
            catch (Exception ex)
            {
                logger.Error(ex, "Details Get");
                return View("Error", new HandleErrorInfo(ex, "MgtClientContact", "Index"));
            }
        }
        public ActionResult Edit(Guid? ContactKey)
        {
            try
            {
                logger.Info("Mgt Client Contact Edit Get invoked by:" + GlobalClass.ProfileUser.FirstName + " " + GlobalClass.ProfileUser.LastName);

                if (ContactKey == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var ClientContact = db.ClientContact.Find(ContactKey);

                ClientContactViewModel ClientContactViewModel = MgtClientContact.ConvertToModel(ClientContact);

                if (ClientContact == null)
                {
                    return HttpNotFound();
                }
                //ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName), "CityKey", "CityName", ClientContact.CityKey);
                //ViewBag.StateKey = new SelectList(db.StateList.Where(m => m.IsDelete == false).OrderBy(m => m.StateName), "Pkey", "StateName", ClientContact.StateKey);
                return View(ClientContactViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Edit Get");
                return View("Error", new HandleErrorInfo(ex, "MgtClientContact", "Index"));
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ClientContactViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    logger.Info("Mgt Client Contact Edit Post invoked by:" + GlobalClass.ProfileUser.FirstName + " " + GlobalClass.ProfileUser.LastName);

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
                //ViewBag.CityKey = new SelectList(db.CityList.Where(m => m.IsDelete == false).OrderBy(m => m.CityName), "CityKey", "CityName", obj.CityKey);
                //ViewBag.StateKey = new SelectList(db.StateList.Where(m => m.IsDelete == false).OrderBy(m => m.StateName), "Pkey", "StateName", obj.StateKey);

                return View(obj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Edit Post");
                return View("Error", new HandleErrorInfo(ex, "MgtClientContact", "Index"));
            }
        }

        public ActionResult getAjaxResult(string pName)
        {

            string searchResult = string.Empty;

            var client = (from a in db.ClientContact
                           where a.FirstName.Contains(pName)
                           orderby a.FirstName
                          select a).Take(10);

            foreach (ClientContact a in client)
            {
                searchResult += string.Format("{0}|\r\n", a.FirstName);
            }

            return Content(searchResult);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string searchTerm)
        {
            try
            {
                logger.Info("Mgt Client Contact Search() invoked by:" + GlobalClass.ProfileUser.FirstName + " " + GlobalClass.ProfileUser.LastName);

                ClientContactViewModel obj = new ClientContactViewModel();
                obj.ClientContactViewModelList = new List<ClientContactViewModel>();
                obj.ClientContactViewModelList = manage.SearchList(searchTerm);


                ThumbnailViewModel model = new ThumbnailViewModel();
                model.ThumbnailModelList = new List<ThumbnailModel>();

                List<ClientContactViewModel> batchList = new List<ClientContactViewModel>();

                batchList = obj.ClientContactViewModelList;

                var listOfBatches = batchList.Batch(1);

                int tabNo = 1;

                foreach (var batchItem in listOfBatches)
                {
                    // Generating tab
                    ThumbnailModel thumbObj = new ThumbnailModel();
                    thumbObj.ThumbnailLabel = "Lebel" + tabNo;
                    thumbObj.ThumbnailTabId = "tab" + tabNo;
                    thumbObj.ThumbnailTabNo = tabNo;
                    thumbObj.Thumbnail_Aria_Controls = "tab" + tabNo;
                    thumbObj.Thumbnail_Href = "#tab" + tabNo;

                    // batch details

                    thumbObj.ClientContactDetailsList = new List<ClientContactViewModel>();

                    foreach (var item in batchItem)
                    {
                        ClientContactViewModel detailsObj = new ClientContactViewModel();
                        detailsObj = item;
                        thumbObj.ClientContactDetailsList.Add(detailsObj);
                    }

                    model.ThumbnailModelList.Add(thumbObj);

                    tabNo++;
                }

                // Getting first tab data
                var first = model.ThumbnailModelList.FirstOrDefault();

                // Getting first tab data
                var last = model.ThumbnailModelList.LastOrDefault();

                foreach (var item in model.ThumbnailModelList)
                {
                    if (item.ThumbnailTabNo == first.ThumbnailTabNo)
                    {
                        item.Thumbnail_ItemPosition = "First";
                    }

                    if (item.ThumbnailTabNo == last.ThumbnailTabNo)
                    {
                        item.Thumbnail_ItemPosition = "Last";
                    }

                }

                return View("Index",model);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Index");
                return View("Error", new HandleErrorInfo(ex, "Home", "UserHome"));
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