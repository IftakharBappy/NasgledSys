using NasgledSys.EM;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class AreaPhotoController : BaseController
    {
        // GET: AreaPhoto
        public ActionResult Edit(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                AreaPhotoViewModel model = new AreaPhotoViewModel();
                model.AreaPhotoList = new List<AreaPhotoModel>();
                model.AreaKey = id;
                try
                {

                    if (db.AreaPhoto.Any(o => o.AreaKey == id))
                    {
                        //AreaDetail entityModel = db.AreaDetail.FirstOrDefault(m => m.AreaKey == id);
                        //model = EM_AreaDetail.ConvertToModel(entityModel);
                        //ViewBag.heading = model.Description;
                    }

                    else
                    {
                        model.AreaKey = id;
                        model.PhotoKey = Guid.Empty;
                       
                    }

                    Session["GlobalMessege"] = "";

                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Edit", "AreaDetail"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult Edit(AreaPhotoViewModel model,  HttpPostedFileBase PostedLogo)
        {
            if (GlobalClass.MasterSession)
            {
                model.AreaPhotoList = new List<AreaPhotoModel>();

                AreaPhotoModel _photoobj = new AreaPhotoModel();
                _photoobj.PhotoKey = Guid.NewGuid();
                _photoobj.AreaKey = model.AreaKey;

                byte[] imgBinaryData = new byte[PostedLogo.ContentLength];
                int readresult = PostedLogo.InputStream.Read(imgBinaryData, 0, PostedLogo.ContentLength);
                _photoobj.FileContent = imgBinaryData;
                _photoobj.FileName = PostedLogo.FileName;
                _photoobj.FileType = PostedLogo.ContentType;

                AreaPhoto entity = new AreaPhoto();
                entity = EM_AreaPhoto.ConvertToEntity(_photoobj);

                db.AreaPhoto.Add(entity);
                db.SaveChanges();

                return View(model);
                 
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
    }
}