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
                        IQueryable<AreaPhoto> query = db.AreaPhoto.Where(m => m.AreaKey == model.AreaKey);

                        var data = query.Select(asset => new AreaPhotoModel()
                        {
                            AreaKey = asset.AreaKey,
                            PhotoKey = asset.PhotoKey,
                            Description = asset.Description,
                            FileContent = asset.FileContent,
                            FileName = asset.FileName,
                            FileType = asset.FileType
                        }).ToList();

                        model.AreaPhotoList = data;
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

                if (PostedLogo != null)
                {
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

                }


                IQueryable<AreaPhoto> query = db.AreaPhoto.Where(m => m.AreaKey == model.AreaKey);

                var data = query.Select(asset => new AreaPhotoModel()
                {
                    AreaKey = asset.AreaKey,
                    PhotoKey = asset.PhotoKey,
                    Description = asset.Description,
                    FileContent = asset.FileContent,
                    FileName = asset.FileName,
                    FileType = asset.FileType 
                }).ToList();

                model.AreaPhotoList = data;

                return View(model);
                 
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
                    AreaPhoto entity = db.AreaPhoto.Find(id);

                    Guid? areaId;
                    areaId = entity.AreaKey;
                    //entity.remo = true;
                    db.AreaPhoto.Remove(entity);
                    db.SaveChanges();
                  
                    Session["GlobalMessege"] = "Area Photo has been DELETED successfully.";
                    return RedirectToAction("Edit", "AreaPhoto", new { @id = areaId });
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
    }
}