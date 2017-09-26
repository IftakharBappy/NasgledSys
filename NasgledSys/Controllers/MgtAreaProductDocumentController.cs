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
    public class MgtAreaProductDocumentController : Controller
    {
        private NasgledDBEntities db = new NasgledDBEntities();
        private ManageAreaProduct manage = new ManageAreaProduct();
        private ManageProjectArea manageArea = new ManageProjectArea();
        public ActionResult Documents(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                AreaProduct areaProduct = db.AreaProduct.Find(id);//# ambigiously AreaKey becomes ProductKey
                AreaProductDocumentViewModel model = new AreaProductDocumentViewModel();
                model.AreaProductDocumentList = new List<AreaProductDocumentViewModel>();
                model.AreaKey = areaProduct.AreaKey;
                model.ProductKey = areaProduct.ProductKey;
                model.FixtureKey = areaProduct.FixtureKey;
                try
                {

                    if (db.AreaProductDocument.Any(m => m.ProductKey == id))
                    {
                        IQueryable<AreaProductDocument> query = db.AreaProductDocument.Where(m => m.ProductKey == id);

                        var data = query.Select(asset => new AreaProductDocumentViewModel()
                        {
                            //AreaKey = asset.AreaKey,
                            DocumentKey = asset.DocumentKey,
                            Description = asset.Description,
                            //FileContent = asset.FileContent,
                            FileName = asset.FileName,
                            //FileType = asset.FileType
                        }).ToList();

                        model.AreaProductDocumentList = data;
                    }

                    else
                    {
                        model.DocumentKey = Guid.Empty;

                    }



                    return View(model);
                }
                catch (Exception e)
                {

                    return View("Error", new HandleErrorInfo(e, "Documents", "MgtAreaProductDocument"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }
        [HttpPost]
        public ActionResult Documents(AreaProductDocumentViewModel model, HttpPostedFileBase PostedLogo)
        {
            if (GlobalClass.MasterSession)
            {

                if (PostedLogo != null)
                {
                    AreaProduct areaProduct = db.AreaProduct.Find(model.ProductKey);
                    AreaProductDocument _documentobj = new AreaProductDocument();
                    _documentobj.DocumentKey = Guid.NewGuid();
                    _documentobj.AreaKey = areaProduct.AreaKey;
                    _documentobj.ProductKey = areaProduct.ProductKey;
                    _documentobj.FixtureKey = areaProduct.FixtureKey;
                    _documentobj.Description = model.Description;

                    byte[] imgBinaryData = new byte[PostedLogo.ContentLength];
                    int readresult = PostedLogo.InputStream.Read(imgBinaryData, 0, PostedLogo.ContentLength);
                    _documentobj.FileContent = imgBinaryData;
                    _documentobj.FileName = PostedLogo.FileName;
                    _documentobj.FileType = PostedLogo.ContentType;


                    db.AreaProductDocument.Add(_documentobj);
                    db.SaveChanges();
                    Session["GlobalMessege"] = "Document is ADDED successfully.";
                }
                else
                {
                    Session["GlobalMessege"] = "No document is provided";
                }
                return RedirectToAction("Documents", new { id = model.ProductKey });

            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult UpdateDocument(AreaProductDocumentViewModel model, HttpPostedFileBase UpdatePostedLogo)
        {
            if (GlobalClass.MasterSession)
            {
                AreaProduct areaProduct = db.AreaProduct.Find(model.ProductKey);
                AreaProductDocument _documentobj = db.AreaProductDocument.Where(p => p.DocumentKey == model.DocumentKey).FirstOrDefault(); //new AreaProductDocument();
                _documentobj.Description = model.Description;
                if (UpdatePostedLogo != null)
                {
                    byte[] imgBinaryData = new byte[UpdatePostedLogo.ContentLength];
                    int readresult = UpdatePostedLogo.InputStream.Read(imgBinaryData, 0, UpdatePostedLogo.ContentLength);
                    _documentobj.FileContent = imgBinaryData;
                    _documentobj.FileName = UpdatePostedLogo.FileName;
                    _documentobj.FileType = UpdatePostedLogo.ContentType;
                }
                db.SaveChanges();
                Session["GlobalMessege"] = "Document is CHANGED successfully.";

                return RedirectToAction("Documents", new { id = model.ProductKey });
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public FileContentResult DowloadFile(Guid? id)
        {
            
            var file = db.AreaProductDocument.Where(a => a.DocumentKey == id).SingleOrDefault();
            //Response.AppendHeader("content-disposition", "inline; filename=file.pdf"); //this will open in a new tab.. remove if you want to open in the same tab.
            return File(file.FileContent, file.FileType, file.FileName);
        }

        public ActionResult Delete(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    AreaProductDocument entity = db.AreaProductDocument.Find(id);
                    Guid? productKey;
                    productKey = entity.ProductKey;
                    db.AreaProductDocument.Remove(entity);
                    db.SaveChanges();

                    Session["GlobalMessege"] = "Document has been DELETED successfully.";
                    return RedirectToAction("Documents", new { @id = productKey });
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