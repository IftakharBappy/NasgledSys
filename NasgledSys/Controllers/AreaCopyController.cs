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

namespace NasgledSys.Controllers
{
    public class AreaCopyController : Controller
    {
        // GET: AreaCopy
        private NasgledDBEntities db = new NasgledDBEntities();

        private ManageProjectArea manage = new ManageProjectArea();
        public ActionResult Index(Guid id)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Guid pKey = id;
                    Area model = db.Area.Find(id);
                    NasgledDBEntities bc = new NasgledDBEntities();
                    Area obj = new Area();
                    obj.AreaKey = Guid.NewGuid();
                    obj.AreaName = "Copy of "+model.AreaName;
                    obj.Reception = "--";
                    obj.SquareFeet = model.SquareFeet;
                    obj.OperationScheduleKey = model.OperationScheduleKey;
                    obj.NewRateScheduleKey = model.NewRateScheduleKey;
                    obj.CoolingSystemKey = model.CoolingSystemKey;
                    obj.HeatingSystemKey = model.HeatingSystemKey;
                    obj.ProjectKey = GlobalClass.Project.ProjectKey;
                    obj.IsDelete = false;
                    obj.ParentAreaKey = model.ParentAreaKey;
                    bc.Area.Add(obj);
                    bc.SaveChanges();
                    pKey = obj.AreaKey;
                    bc = new NasgledDBEntities();

                    AreaDetail dt = db.AreaDetail.FirstOrDefault(m => m.AreaKey == id);
                    if (dt == null) { }
                    else
                    {
                        AreaDetail detail = new AreaDetail();
                        detail.DetailKey = Guid.NewGuid();
                        detail.AreaKey = pKey;
                        detail.Description = dt.Description;
                        detail.AverageIlluminnce = dt.AverageIlluminnce;
                        detail.LightingSatisfactionKey = dt.LightingSatisfactionKey;
                        detail.CeilingHeight = dt.CeilingHeight;
                        detail.Reflectance = dt.Reflectance;
                        detail.AreaWidth = dt.AreaWidth;
                        detail.Length = dt.Length;
                        bc.AreaDetail.Add(detail);
                        bc.SaveChanges();
                        bc = new NasgledDBEntities();

                    }
                    var doc = from x in db.AreaDocument where x.AreaKey == id select x;
                    if (doc.Count() > 0)
                    {
                        foreach(var item in doc)
                        {
                            AreaDocument dm = new AreaDocument();
                            dm.DocumentKey = Guid.NewGuid();
                            dm.AreaKey = pKey;
                            dm.Description = item.Description;
                            dm.FileType = item.FileType;
                            dm.FileContent = item.FileContent;
                            dm.FileName =item.FileName;                                                      
                            bc.AreaDocument.Add(dm);
                            bc.SaveChanges();
                            bc = new NasgledDBEntities();
                        }
                    }
                    var photo = from x in db.AreaPhoto where x.AreaKey == id select x;
                    if (photo.Count() > 0)
                    {
                        foreach (var pitem in photo)
                        {
                            AreaPhoto dm = new AreaPhoto();
                            dm.PhotoKey = Guid.NewGuid();
                            dm.AreaKey = pKey;
                            dm.Description = pitem.Description;
                            dm.FileType = pitem.FileType;
                            dm.FileContent = pitem.FileContent;
                            dm.FileName = pitem.FileName;
                            bc.AreaPhoto.Add(dm);
                            bc.SaveChanges();
                            bc = new NasgledDBEntities();
                        }
                    }
                    AreaNote note = db.AreaNote.FirstOrDefault(m => m.AreaKey == id);
                    if (note == null) { }
                    else
                    {
                        AreaNote nt = new AreaNote();
                        nt.NoteKey = Guid.NewGuid();
                        nt.AreaKey = pKey;
                        nt.NoteContent = note.NoteContent;
                        nt.Internal = note.Internal;
                        bc.AreaNote.Add(nt);
                        bc.SaveChanges();
                        bc = new NasgledDBEntities();
                    }

                    #region Area Product
                    var product = from x in db.AreaProduct where x.AreaKey == id && x.IsDelete == false select x;
                    if (product.Count() > 0)
                    {
                        foreach(var item in product)
                        {
                            AreaProduct pd = new AreaProduct();
                            pd.ProductKey = Guid.NewGuid();
                            pd.AreaKey = pKey;
                            pd.FixtureKey = item.FixtureKey;
                            pd.Count = item.Count;
                            pd.Description = item.Description;
                            pd.ForEveryMainQty = item.ForEveryMainQty;
                            pd.ReplaceQty = item.ReplaceQty;
                            pd.SolutionName = item.SolutionName;
                            pd.IncentiveTypeKey = item.IncentiveTypeKey;
                            pd.IncentiveAmount = item.IncentiveAmount;
                            pd.IncentiveMaxAmount = item.IncentiveMaxAmount;
                            pd.IncentiveMaxTypeKey = item.IncentiveMaxTypeKey;
                            pd.ProductName = item.ProductName;
                            pd.ModelNo = item.ModelNo;
                            pd.WattPerProduct = item.WattPerProduct;
                            pd.L70 = item.L70;
                            pd.ThermalEfficency = item.ThermalEfficency;
                            pd.CustomNotes = item.CustomNotes;
                            pd.ProductCost = item.ProductCost;
                            pd.MarkupPercentage = item.MarkupPercentage;
                            pd.InstallationTime = item.InstallationTime;
                            pd.InstallationCost = item.InstallationCost;
                            pd.ShipingCost = item.ShipingCost;
                            pd.MiscCost = item.MiscCost;
                            pd.IsOn = item.IsOn;
                            pd.ProjectKey = item.ProjectKey;
                            pd.IsDelete = item.IsDelete;
                            pd.ExistingProductName = item.ExistingProductName;
                            bc.AreaProduct.Add(pd);
                            bc.SaveChanges();
                            bc = new NasgledDBEntities();

                            AreaProductDetail pdd = db.AreaProductDetail.FirstOrDefault(m=>m.ProductKey==item.ProductKey);
                            if (pdd == null) { }
                            else
                            {
                                AreaProductDetail newPdd = new AreaProductDetail();
                                newPdd.DetailKey = Guid.NewGuid();
                                newPdd.AreaKey = pKey;
                                newPdd.FixtureKey = pdd.FixtureKey;
                                newPdd.ProductKey = pd.ProductKey;
                                newPdd.ExistingControl = pdd.ExistingControl;
                                newPdd.OperationScheduleKey = pdd.OperationScheduleKey;
                                newPdd.WorkingFixtureCount = pdd.WorkingFixtureCount;
                                newPdd.ReplacementCost = pdd.ReplacementCost;
                                newPdd.AnnualMaintenance = pdd.AnnualMaintenance;
                                newPdd.InstallationInTime = pdd.InstallationInTime;
                                newPdd.Location = pdd.Location;
                                newPdd.Year = pdd.Year;
                                newPdd.LightingSatisfactionKey = pdd.LightingSatisfactionKey;
                                newPdd.MountingHeight = pdd.MountingHeight;
                                bc.AreaProductDetail.Add(newPdd);
                                bc.SaveChanges();
                                bc = new NasgledDBEntities();
                            }

                            #region AreaProductDocument
                            var pdoc = from x in db.AreaProductDocument where x.ProductKey == item.ProductKey select x;
                            if (pdoc.Count() > 0)
                            {
                                foreach(var pditem in pdoc)
                                {
                                    AreaProductDocument newpdoc = new AreaProductDocument();
                                    newpdoc.DocumentKey = Guid.NewGuid();
                                    newpdoc.AreaKey = pKey;
                                    newpdoc.FixtureKey = pditem.FixtureKey;
                                    newpdoc.ProductKey= pd.ProductKey;
                                    newpdoc.FileType = pditem.FileType;
                                    newpdoc.FileName = pditem.FileName;
                                    newpdoc.FileContent = pditem.FileContent;
                                    newpdoc.Description = pditem.Description;
                                    bc.AreaProductDocument.Add(newpdoc);
                                    bc.SaveChanges();
                                    bc = new NasgledDBEntities();
                                }
                            }
                            #endregion

                            #region AreaProductPhoto
                            var pphoto = from x in db.AreaProductPhoto where x.ProductKey == item.ProductKey select x;
                            if (pphoto.Count() > 0)
                            {
                                foreach (var phditem in pphoto)
                                {
                                    AreaProductPhoto newpdoc = new AreaProductPhoto();
                                    newpdoc.PhotoKey = Guid.NewGuid();
                                    newpdoc.AreaKey = pKey;
                                    newpdoc.FixtureKey = phditem.FixtureKey;
                                    newpdoc.ProductKey = pd.ProductKey;
                                    newpdoc.FileType = phditem.FileType;
                                    newpdoc.FileName = phditem.FileName;
                                    newpdoc.FileContent = phditem.FileContent;
                                    newpdoc.Description = phditem.Description;
                                    bc.AreaProductPhoto.Add(newpdoc);
                                    bc.SaveChanges();
                                    bc = new NasgledDBEntities();
                                }
                            }
                            #endregion

                            AreaProductNote pdnote = db.AreaProductNote.FirstOrDefault(m => m.ProductKey == item.ProductKey);
                            if (pdnote == null) { }
                            else
                            {
                                AreaProductNote newpdnote = new AreaProductNote();
                                newpdnote.NoteKey = Guid.NewGuid();
                                newpdnote.AreaKey = pKey;
                                newpdnote.FixtureKey = pdnote.FixtureKey;
                                newpdnote.ProductKey = pd.ProductKey;
                                newpdnote.Condition = pdnote.Condition;
                                newpdnote.InternalNotes = pdnote.InternalNotes;
                                newpdnote.GeneralNote = pdnote.GeneralNote;
                                newpdnote.Description = pdnote.Description;
                                bc.AreaProductNote.Add(newpdnote);
                                bc.SaveChanges();
                                bc = new NasgledDBEntities();
                            }
                        }
                    }
                    #endregion
                    return RedirectToAction("Edit", "MgtSubArea",new { id=pKey});
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