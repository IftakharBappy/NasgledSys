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
    public class MgtAreaProductPhotoController : Controller
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        private ManageAreaProduct manage = new ManageAreaProduct();
        private ManageProjectArea manageArea = new ManageProjectArea();
        public ActionResult Photos(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                AreaProduct areaProduct = db.AreaProduct.Find(id);//# ambigiously AreaKey becomes ProductKey
                AreaProductPhotoViewModel model = new AreaProductPhotoViewModel();
                model.AreaProductPhotoList = new List<AreaProductPhotoViewModel>();
                model.AreaKey = areaProduct.AreaKey;
                model.ProductKey = areaProduct.ProductKey;
                model.FixtureKey = areaProduct.FixtureKey;
                try
                {

                    if (db.AreaProductPhoto.Any(m => m.ProductKey == id))
                    {
                        IQueryable<AreaProductPhoto> query = db.AreaProductPhoto.Where(m => m.ProductKey == id);

                        var data = query.Select(asset => new AreaProductPhotoViewModel()
                        {
                            //AreaKey = asset.AreaKey,
                            PhotoKey = asset.PhotoKey,
                            Description = asset.Description
                            //FileContent = asset.FileContent,
                            //FileName = asset.FileName,
                            //FileType = asset.FileType
                        }).ToList();

                        model.AreaProductPhotoList = data;
                    }

                    else
                    {
                       
                        model.PhotoKey = Guid.Empty;

                    }



                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Photos", "MgtAreaProductPhoto"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult Photos(AreaProductPhotoViewModel model, HttpPostedFileBase PostedLogo)
        {
            if (GlobalClass.MasterSession)
            {
                
                if (PostedLogo != null)
                {
                    AreaProduct areaProduct = db.AreaProduct.Find(model.ProductKey);
                    AreaProductPhoto _photoobj = new AreaProductPhoto();
                    _photoobj.PhotoKey = Guid.NewGuid();
                    _photoobj.AreaKey = areaProduct.AreaKey;
                    _photoobj.ProductKey = areaProduct.ProductKey;
                    _photoobj.FixtureKey = areaProduct.FixtureKey;
                    _photoobj.Description = model.Description;

                    byte[] imgBinaryData = new byte[PostedLogo.ContentLength];
                    int readresult = PostedLogo.InputStream.Read(imgBinaryData, 0, PostedLogo.ContentLength);
                    _photoobj.FileContent = imgBinaryData;
                    _photoobj.FileName = PostedLogo.FileName;
                    _photoobj.FileType = PostedLogo.ContentType;

                    
                    db.AreaProductPhoto.Add(_photoobj);
                    db.SaveChanges();
                    Session["GlobalMessege"] = "Photo is ADDED successfully.";
                    Session["counter"] = 1;
                }
                else
                {
                    Session["GlobalMessege"] = "No photo is provided";
                }
                return RedirectToAction("Photos", new { id = model.ProductKey });
                
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult UpdatePhoto(AreaProductPhotoViewModel model, HttpPostedFileBase UpdatePostedLogo)
        {
            if (GlobalClass.MasterSession)
            {
                AreaProduct areaProduct = db.AreaProduct.Find(model.ProductKey);
                AreaProductPhoto _photoobj = db.AreaProductPhoto.Where(p => p.PhotoKey == model.PhotoKey).FirstOrDefault(); //new AreaProductPhoto();
                _photoobj.Description = model.Description;
                if (UpdatePostedLogo != null)
                {
                    byte[] imgBinaryData = new byte[UpdatePostedLogo.ContentLength];
                    int readresult = UpdatePostedLogo.InputStream.Read(imgBinaryData, 0, UpdatePostedLogo.ContentLength);
                    _photoobj.FileContent = imgBinaryData;
                    _photoobj.FileName = UpdatePostedLogo.FileName;
                    _photoobj.FileType = UpdatePostedLogo.ContentType;
                }
                db.SaveChanges();
                Session["GlobalMessege"] = "Photo is CHANGED successfully.";
                Session["counter"] = 1;
                return RedirectToAction("Photos", new { id = model.ProductKey });
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }


        public ActionResult Delete(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    AreaProductPhoto entity = db.AreaProductPhoto.Find(id);
                    Guid? productKey;
                    productKey = entity.ProductKey;
                    db.AreaProductPhoto.Remove(entity);
                    db.SaveChanges();
                    Session["counter"] = 1;
                    Session["GlobalMessege"] = "Photo has been DELETED successfully.";
                    return RedirectToAction("Photos", new { id = productKey });
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