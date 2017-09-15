using NasgledSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NasgledSys.Controllers
{
    public class MgtEnvironmentalImpactController : Controller
    {
        // GET: MgtEnvironmentalImpact
        private NasgledDBEntities db = new NasgledDBEntities();
        public ActionResult Index()
        {
            try
            {
                 
                var environmentalImpact = db.EnvironmentalImpact;
                EnvironmentalImpactViewModel model = new EnvironmentalImpactViewModel();

                model.EnvironmentalImpactViewModelList = environmentalImpact.Select(p => new EnvironmentalImpactViewModel()
                {
                    FactorKey = p.FactorKey,
                    FactorName = p.FactorName,
                    UnitName = p.UnitName,
                    QtyUsed = p.QtyUsed,
                    KilowattSaved = p.KilowattSaved,
                    Logo = p.Logo
            }).ToList();

                ViewBag.Unit = new SelectList(db.Unit.OrderBy(m => m.UnitName), "UnitKey", "UnitName");
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Home", "Index"));
            }
        }
        public ActionResult Create()
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    var model = new EnvironmentalImpactViewModel();
                    return View("Create", model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtEnvironmentalImpact", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        public ActionResult Create(EnvironmentalImpactViewModel obj , HttpPostedFileBase PostedLogo)
        {
            if (GlobalClass.MasterSession)
            {
                Session["GlobalMessege"] = " ";
                try
                {
                    if (ModelState.IsValid)
                    {
                        EnvironmentalImpact model = new EnvironmentalImpact();
                        model.FactorKey = Guid.NewGuid();
                        model.FactorName = obj.FactorName;
                        model.UnitName = obj.UnitName;
                        model.QtyUsed = obj.QtyUsed;
                        model.KilowattSaved = obj.KilowattSaved;
                        if (obj.QtyUsed == null) { model.QtyUsed = 0; }
                        else { model.QtyUsed = obj.QtyUsed; }
                        if (obj.KilowattSaved == null) { model.KilowattSaved = 0; }
                        else { model.KilowattSaved = obj.KilowattSaved; }

                        byte[] imgBinaryData = new byte[PostedLogo.ContentLength];
                        int readresult = PostedLogo.InputStream.Read(imgBinaryData, 0, PostedLogo.ContentLength);
                        model.Logo = imgBinaryData;
                        model.Filename = PostedLogo.FileName;
                        model.LogoType = PostedLogo.ContentType;
                        ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", model.Filename);

                        db.EnvironmentalImpact.Add(model);
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Environmental Impact is Saved Successfully";
                        return RedirectToAction("Index");
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtEnvironmentalImpact", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public ActionResult Edit(Guid? FactorKey)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    Session["GlobalMessege"] = " ";
                    if (FactorKey == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var EnvImpact = db.EnvironmentalImpact.Find(FactorKey);

                    EnvironmentalImpactViewModel model = new EnvironmentalImpactViewModel();
                    model.FactorKey = EnvImpact.FactorKey;
                    model.FactorName = EnvImpact.FactorName;
                    model.UnitName = EnvImpact.UnitName;
                    model.QtyUsed = EnvImpact.QtyUsed;
                    model.KilowattSaved = EnvImpact.KilowattSaved;
                    model.Logo = EnvImpact.Logo;

                    if (EnvImpact == null)
                    {
                        return HttpNotFound();
                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtEnvironmentalImpact", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(EnvironmentalImpactViewModel obj, HttpPostedFileBase PostedLogo)
        {
            if (GlobalClass.MasterSession)
            {
                try
                {
                    if (ModelState.IsValid)
                    {

                        EnvironmentalImpact model = db.EnvironmentalImpact.Find(obj.FactorKey);
                        model.FactorName = obj.FactorName;
                        model.UnitName = obj.UnitName;
                        model.QtyUsed = obj.QtyUsed;
                        model.KilowattSaved = obj.KilowattSaved;
                        if (obj.QtyUsed == null) { model.QtyUsed = 0; }
                        else { model.QtyUsed = obj.QtyUsed; }
                        if (obj.KilowattSaved == null) { model.KilowattSaved = 0; }
                        else { model.KilowattSaved = obj.KilowattSaved; }

                        if (PostedLogo != null && PostedLogo.ContentLength > 0)
                        {
                            byte[] imgBinaryData = new byte[PostedLogo.ContentLength];
                            int readresult = PostedLogo.InputStream.Read(imgBinaryData, 0, PostedLogo.ContentLength);
                            model.Logo = imgBinaryData;
                            model.Filename = PostedLogo.FileName;
                            model.LogoType = PostedLogo.ContentType;
                            ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", model.Filename);
                        }
                        db.SaveChanges();
                        Session["GlobalMessege"] = "Environmental Impact is Updated Successfully";
            
                        return RedirectToAction("Index", new { id = obj.FactorKey });
                    }

                    return View(obj);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MgtEnvironmentalImpact", "Index"));
                }
            }
            else
            {
                Exception e = new Exception("Session Expired");
                return View("Error", new HandleErrorInfo(e, "Home", "UserLogin"));
            }
        }

        public JsonResult LoadUnitDropDown_ToCreate()
        {
            var list = (from unit in db.Unit select new {unit.UnitName }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadUnitDropDown_ToEdit(string UnitName)
        {
            var list = (from unit in db.Unit
                        select new
                        {
                            unit.UnitName,
                            Selected = unit.UnitName == UnitName ? "selected" : ""
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}