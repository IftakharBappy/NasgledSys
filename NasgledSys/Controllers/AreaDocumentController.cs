using NasgledSys.EM;
using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class AreaDocumentController : BaseController
    {
        // GET: AreaDocument
        public ActionResult Edit(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                AreaDocumentViewModel model = new AreaDocumentViewModel();
                model.AreaDocumentList = new List<AreaDocumentModel>();
                model.AreaKey = id;
                try
                {

                    if (db.AreaDocument.Any(o => o.AreaKey == id))
                    {
                        IQueryable<AreaDocument> query = db.AreaDocument.Where(m => m.AreaKey == model.AreaKey);

                        var data = query.Select(asset => new AreaDocumentModel()
                        {
                            AreaKey = asset.AreaKey,
                            DocumentKey = asset.DocumentKey,
                            Description = asset.Description,
                            FileContent = asset.FileContent,
                            FileName = asset.FileName,
                            FileType = asset.FileType
                        }).ToList();

                        model.AreaDocumentList = data;
                    }

                    else
                    {
                        model.AreaKey = id;
                        model.DocumentKey = Guid.Empty;

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
        public ActionResult Edit(AreaDocumentViewModel model, HttpPostedFileBase PostedLogo)
        {
            if (GlobalClass.MasterSession)
            {


                if (PostedLogo != null)
                {
                    AreaDocumentModel _photoobj = new AreaDocumentModel();
                    _photoobj.DocumentKey = Guid.NewGuid();
                    _photoobj.AreaKey = model.AreaKey;

                    byte[] imgBinaryData = new byte[PostedLogo.ContentLength];
                    int readresult = PostedLogo.InputStream.Read(imgBinaryData, 0, PostedLogo.ContentLength);
                    _photoobj.FileContent = imgBinaryData;
                    _photoobj.FileName = PostedLogo.FileName;
                    _photoobj.FileType = PostedLogo.ContentType;

                    AreaDocument entity = new AreaDocument();
                    entity = EM_DocumentPhoto.ConvertToEntity(_photoobj);

                    db.AreaDocument.Add(entity);
                    db.SaveChanges();

                }

                else
                {
                    // Remove Photo
                    foreach (var item in model.AreaDocumentList)
                    {
                        AreaDocument entityrem = db.AreaDocument.Find(item.DocumentKey);
                        db.AreaDocument.Remove(entityrem);
                        db.SaveChanges();
                    }

                    // Add Photo
                    foreach (var item in model.AreaDocumentList)
                    {
                        AreaDocument entity = new AreaDocument();
                        entity = EM_DocumentPhoto.ConvertToEntity(item);
                        db.AreaDocument.Add(entity);
                        db.SaveChanges();
                    }

                   
                }

                model.AreaDocumentList = new List<AreaDocumentModel>();

                IQueryable<AreaDocument> query = db.AreaDocument.Where(m => m.AreaKey == model.AreaKey);

                var data = query.Select(asset => new AreaDocumentModel()
                {
                    AreaKey = asset.AreaKey,
                    DocumentKey = asset.DocumentKey,
                    Description = asset.Description,
                    FileContent = asset.FileContent,
                    FileName = asset.FileName,
                    FileType = asset.FileType
                }).ToList();

                model.AreaDocumentList = data;

                return View(model);

            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult UpdatePhoto(AreaDocumentViewModel model, HttpPostedFileBase UpdatePostedLogo)
        {
            if (GlobalClass.MasterSession)
            {


                model.AreaDocumentList = new List<AreaDocumentModel>();

                if (UpdatePostedLogo != null)
                {

                    AreaDocument entity = db.AreaDocument.Find(model.DocumentKey);
                    model.AreaKey = entity.AreaKey;

                    AreaDocumentModel _photoobj = new AreaDocumentModel();
                    _photoobj.DocumentKey = model.DocumentKey;
                    _photoobj.AreaKey = model.AreaKey;

                    byte[] imgBinaryData = new byte[UpdatePostedLogo.ContentLength];
                    int readresult = UpdatePostedLogo.InputStream.Read(imgBinaryData, 0, UpdatePostedLogo.ContentLength);
                    _photoobj.FileContent = imgBinaryData;
                    _photoobj.FileName = UpdatePostedLogo.FileName;
                    _photoobj.FileType = UpdatePostedLogo.ContentType;

                    // AreaDocument entity = new AreaDocument();
                    entity = EM_DocumentPhoto.ConvertToEntity(_photoobj);

                    #region Remove Data

                    AreaDocument entityrem = db.AreaDocument.Find(model.DocumentKey);
                    db.AreaDocument.Remove(entityrem);
                    db.SaveChanges();

                    #endregion

                    db.AreaDocument.Add(entity);
                    db.SaveChanges();

                }


                IQueryable<AreaDocument> query = db.AreaDocument.Where(m => m.AreaKey == model.AreaKey);

                var data = query.Select(asset => new AreaDocumentModel()
                {
                    AreaKey = asset.AreaKey,
                    DocumentKey = asset.DocumentKey,
                    Description = asset.Description,
                    FileContent = asset.FileContent,
                    FileName = asset.FileName,
                    FileType = asset.FileType
                }).ToList();

                model.AreaDocumentList = data;
                return RedirectToAction("Edit", "AreaDocument", new { @id = model.AreaKey });

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
                    AreaDocument entity = db.AreaDocument.Find(id);
                    Guid? areaId;
                    areaId = entity.AreaKey;
                    db.AreaDocument.Remove(entity);
                    db.SaveChanges();

                    Session["GlobalMessege"] = "Area Photo has been DELETED successfully.";
                    return RedirectToAction("Edit", "AreaDocument", new { @id = areaId });
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